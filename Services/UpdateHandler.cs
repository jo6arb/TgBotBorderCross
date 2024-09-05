using Telegram.Bot;
using Telegram.Bot.Exceptions;
using Telegram.Bot.Polling;
using Telegram.Bot.Types;

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
            logger.LogInformation($"Message: {message}");
        }
    }
}