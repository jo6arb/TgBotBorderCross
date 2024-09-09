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
                "Разъяснения по вопросам пропуска через ГГ РФ" => PropuskMessage(message),
                "Разъяснения вопросам пограничного режима" => WorkLaterMessage(message),
                "Актуальные вакансии" => WorkLaterMessage(message),
                 
            });

            logger.LogInformation($"Message: {message}");
        }

        private async Task<Message> WorkLaterMessage(Message message)
        {
            return await bot.SendTextMessageAsync(message.Chat, text:"Данный радел находится в разработке!");
        }

        private async Task<Message> PropuskMessage(Message message)
        {
            throw new NotImplementedException();
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