using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;

public class ScheduledJobService : IHostedService, IDisposable
{
    private readonly Timer _timer;

    public ScheduledJobService()
    {
        // Get the current date and time
        var now = DateTime.Now;

        // Set the desired time for the job to run
        var scheduledTime = new DateTime(now.Year, now.Month, now.Day, 4, 28, 0);

        // If the scheduled time has already passed, add one day to the scheduled time
        if (now > scheduledTime)
        {
            scheduledTime = scheduledTime.AddDays(1);
        }

        // Calculate the interval between now and the scheduled time
        var interval = (scheduledTime - now).TotalMilliseconds;

        // Create a timer with the calculated interval
        _timer = new Timer(ExecuteJob, null, (int)interval, Timeout.Infinite);
    }

    public Task StartAsync(CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        _timer?.Dispose();
        return Task.CompletedTask;
    }

    public void Dispose()
    {
        _timer?.Dispose();
    }

    private void ExecuteJob(object state)
    {
        // This method will be executed at the specified time
        Console.WriteLine("Doing work...");

        // Calculate the interval until the next scheduled time
        var interval = TimeSpan.FromDays(1).TotalMilliseconds;

        // Reset the timer with the new interval
        _timer.Change((int)interval, Timeout.Infinite);
    }
}