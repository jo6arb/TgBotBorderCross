﻿using TgBotBorderCross.Abstract;

namespace TgBotBorderCross.Services
{
    public class PollingService(IServiceProvider serviceProvider, ILogger<PollingService> logger)
        : PollingServiceBase<ReceiverService>(serviceProvider, logger);
}
