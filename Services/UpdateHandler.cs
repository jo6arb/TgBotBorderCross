using Telegram.Bot;
using Telegram.Bot.Exceptions;
using Telegram.Bot.Polling;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using TgBotBorderCross.Abstract;

namespace TgBotBorderCross.Services
{
    public class UpdateHandler(ITelegramBotClient bot, ILogger<UpdateHandler> logger) : IUpdateHandler
    {
        public async Task HandleErrorAsync(ITelegramBotClient bot, Exception error, HandleErrorSource source, CancellationToken cancellationToken)
        {
            logger.LogInformation("HanleError: {error}", error);

            if (error is RequestException)
            {
                await Task.Delay(TimeSpan.FromSeconds(2), cancellationToken);
            }
        }

        public async Task HandleUpdateAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            await (update switch
            {
                { Message: { } message } => OnMessage(message),
                _ => UnknownUpdateHandlerAsync(update)
            });
        }

        private Task UnknownUpdateHandlerAsync(Update update)
        {
            logger.LogInformation($"Unknown update handler: {update.Type}");
            return Task.CompletedTask;
        }

        private async Task OnMessage(Message message)
        {
            if (message.Text is not { } messageText)
                return;

            Message sendMessage = await (messageText switch 
            {
                "/start" => HelloMessage(message),
                "Общее по вопросам пропуска через ГГ РФ" => PropuskMessage(message),
                "Разъяснения вопросам пограничного режима" => WorkLaterMessage(message),
                "Актуальные вакансии" => WorkLaterMessage(message),
                "Разъяснения по вопросам режима в пункте пропуска через ГГ РФ" => RegimeAnswerMessage(message),
                "Получение пропуска в пункт пропуска через ГГ РФ" => GetPropuskAnswerMessage(message),
                "Отказ в выдаче пропуска через ГГ РФ" => GetNonePropuskAnswerMessage(message),
                "Сход на берег экипажа заграничного следования" => GetEkipageAnswerMessage(message),
                "Разъяснения вопросам пропуска через ГГ РФ" => GetAnswerPropuskAnswerMessage(message),
                "Миграционная карта" => GetMigrationCardAnswerMessage(message),
                "Получение миграционной карты при следовании иностранного гражданина из Республики Беларусь в Российскую Федерацию" => GetMigrationCardRBRFAnswerMessage(message),
                "Выезд из Российской Федерации без миграционной карты" => ExitWithotMigrationCardAnswerMessage(message),
                "Дети" => ChildrenAnswerMessage(message),
                "Подверждение гражданства Российской Федерации" => ChildrenGragdanstvAnswerMessage(message),
                "Выезд из Российской Федерации" => ChildrenViezdAnswerMessage(message),
                "Заявление о несогласии на выезд из Российской Федерации" => ChildrenNesoglasAnswerMessage(message),
                "Экипаж морского суда заграничного следования" => WorkLaterMessage(message),
                "Функционирование пунктов пропуска" => WorkLaterMessage(message),

            });

            logger.LogInformation($"Message: {message}");
        }

        private async Task<Message> ChildrenNesoglasAnswerMessage(Message message)
        {
            return await bot.SendTextMessageAsync(message.Chat, text: AnswerText.MessageDetiNesogl(), parseMode: ParseMode.Html);
        }

        private async Task<Message> ChildrenViezdAnswerMessage(Message message)
        {
            return await bot.SendTextMessageAsync(message.Chat, text: AnswerText.MessageDetiViezd(), parseMode: ParseMode.Html);
        }

        private async Task<Message> ChildrenGragdanstvAnswerMessage(Message message)
        {
            return await bot.SendTextMessageAsync(message.Chat, text: AnswerText.MessageDetiGragdanst(), parseMode: ParseMode.Html);
        }

        private async Task<Message> ChildrenAnswerMessage(Message message)
        {
            return await bot.SendTextMessageAsync(message.Chat, text: "Раздел Дети", replyMarkup: MenuCreator.MenuChildren());
        }

        private async Task<Message> ExitWithotMigrationCardAnswerMessage(Message message)
        {
            return await bot.SendTextMessageAsync(message.Chat, text: AnswerText.MessageMKExitRF(), parseMode: ParseMode.Html);
        }

        private async Task<Message> GetMigrationCardRBRFAnswerMessage(Message message)
        {
            return await bot.SendTextMessageAsync(message.Chat, text: AnswerText.MessageMKRBRF(), parseMode: ParseMode.Html);
        }

        private async Task<Message> GetMigrationCardAnswerMessage(Message message)
        {
            return await bot.SendTextMessageAsync(message.Chat, text: "Пункты меню:", replyMarkup: MenuCreator.MenuMigrationCard());
        }

        private async Task<Message> GetAnswerPropuskAnswerMessage(Message message)
        {
            return await bot.SendTextMessageAsync(message.Chat, text: "Выберите пункты меню", replyMarkup: MenuCreator.MenuPropuskPP());
        }

        private async Task<Message> GetEkipageAnswerMessage(Message message)
        {
            return await bot.SendTextMessageAsync(message.Chat, text: AnswerText.MessageRegimeThree(), parseMode: ParseMode.Html);
        }

        private async Task<Message> GetNonePropuskAnswerMessage(Message message)
        {
            return await bot.SendTextMessageAsync(message.Chat, text: AnswerText.MessageRegimeTwo(), parseMode: ParseMode.Html);
        }

        private async Task<Message> GetPropuskAnswerMessage(Message message)
        {
            return await bot.SendTextMessageAsync(message.Chat, text: AnswerText.MessageRegimeOne(), parseMode: ParseMode.Html);
        }

        private async Task<Message> RegimeAnswerMessage(Message message)
        {
            return await bot.SendTextMessageAsync(message.Chat, text: "Выберите пункты меню", replyMarkup: MenuCreator.MenuRegime());
        }

        private async Task<Message> WorkLaterMessage(Message message)
        {
            return await bot.SendTextMessageAsync(message.Chat, text:"Данный радел находится в разработке!");
        }

        private async Task<Message> PropuskMessage(Message message)
        {
            return await bot.SendTextMessageAsync(message.Chat, text: "Выберите пункты меню", replyMarkup: MenuCreator.MenuPropusk());
        }

        private async Task<Message> HelloMessage(Message message)
        {
            string helloMsg = $"👋Здравствуйте, {message.From.FirstName}{Environment.NewLine}" +
                     $"Данный бот разработан для разъяснений по{Environment.NewLine}"
                     + $"пересечению государственной границы на участке{Environment.NewLine}"
                     + $"ПУ ФСБ России по городу Санкт-Петербургу{Environment.NewLine}";

            return await bot.SendTextMessageAsync(message.Chat, helloMsg, parseMode:ParseMode.Html,
                replyMarkup: MenuCreator.MainMenu());
        }
    }
}