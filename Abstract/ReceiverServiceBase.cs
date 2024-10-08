﻿using Telegram.Bot;
using Telegram.Bot.Polling;

namespace TgBotBorderCross.Abstract
{
    public abstract class ReceiverServiceBase<TUpdateHandler> : IReceiverService
        where TUpdateHandler: IUpdateHandler
    {
        private readonly ITelegramBotClient _botClient;
        private readonly TUpdateHandler _updateHandler;
        private readonly ILogger<ReceiverServiceBase<TUpdateHandler>> _logger;

        internal ReceiverServiceBase(
            ITelegramBotClient botClient,
            TUpdateHandler updateHandler,
            ILogger<ReceiverServiceBase<TUpdateHandler>> logger)
        {
            _botClient = botClient;
            _updateHandler = updateHandler;
            _logger = logger;
        }

        public async Task ReceiveAsync(CancellationToken stoppingToken)
        {
            var receiveOptions = new ReceiverOptions()
            {
                AllowedUpdates = [],
                DropPendingUpdates = true,
            };

            var me = await _botClient.GetMeAsync(stoppingToken);
            _logger.LogInformation("Start receiving updates for {BotName}", me.Username);

            await _botClient.ReceiveAsync(
                updateHandler: _updateHandler,
                receiverOptions: receiveOptions,
                cancellationToken: stoppingToken);
        }
    }
}
