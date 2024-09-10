using Microsoft.Extensions.Options;
using Telegram.Bot;
using TgBotBorderCross.Services;

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((context, services) =>
    {
        services.Configure<BotConfig>(context.Configuration.GetSection("BotConfig"));

        services.AddHttpClient("telegram_bot").RemoveAllLoggers()
            .AddTypedClient<ITelegramBotClient>((httpClient, sp) =>
            {
                BotConfig? botConfig = sp.GetService<IOptions<BotConfig>>()?.Value;
                ArgumentNullException.ThrowIfNull(nameof(botConfig));
                TelegramBotClientOptions options = new(botConfig.BotToken);
                return new TelegramBotClient(options, httpClient);
            });

        services.AddScoped<UpdateHandler>();
        services.AddScoped<ReceiverService>();
        services.AddHostedService<PollingService>();
    })
    .UseSystemd()
    .Build();

await host.RunAsync();
