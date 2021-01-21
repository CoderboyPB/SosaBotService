using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Serilog;

namespace SportsfreundSosaService
{
    public class Worker : BackgroundService
    {
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                Log.Information("Execute Async aufgerufen");
                await Bot.Retweet();
                await Task.Delay(1000 * 60 *2, stoppingToken);
            }
        }

        public override async Task StartAsync(CancellationToken cancellationToken)
        {
            await Bot.Say($"Es ist {DateTime.Now:HH:mm:ss}, und ich bin habe ausgeschlafen und bin wieder zurück.");
            Log.Information("Return from Sleep");
            await base.StartAsync(cancellationToken);
        }

        public override async Task StopAsync(CancellationToken cancellationToken)
        {
            await Bot.Say($"Es ist {DateTime.Now:now:HH:mm:ss}, und ich lege mich jetzt hin für ein Schläfchen.");
            Log.Information("Going to Sleep");
            await base.StartAsync(cancellationToken);
        }
    }
}
