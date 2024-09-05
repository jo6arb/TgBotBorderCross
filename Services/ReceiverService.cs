using Telegram.Bot;
using TgBotBorderCross.Abstract;

namespace TgBotBorderCross.Services
{
    public class ReceiverService(ITelegramBotClient botClient, UpdateHandler updateHandler, ILogger<ReceiverServiceBase<UpdateHandler>> logger)
        : ReceiverServiceBase<UpdateHandler>(botClient, updateHandler, logger);
}