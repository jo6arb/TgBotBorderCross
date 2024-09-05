namespace TgBotBorderCross.Abstract
{
    public interface IReceiverService
    {
        Task ReceiveAsync(CancellationToken cancellationToken);
    }
}
