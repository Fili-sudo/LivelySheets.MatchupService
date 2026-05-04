namespace LivelySheets.MatchupService.API.Background_Services;

public static class LoggingMessages
{
    public static readonly string BackgroundServiceStarted = "{0} started at {1}";
    public static readonly string BackgroundServiceUnhandledErrorMessage = "{0} crashed unexpectedly at {1}. Retrying...";
    public static readonly string PeriodicTaskTicked = "{0}: Looking for messages to process...";
    public static readonly string DequeueErrorMessage = "Dequeue couldn't be performed";
    public static readonly string StartProcessMessage = "OutboxMessage:{0} started processing at {1}";
    public static readonly string FinishProcessMessage = "OutboxMessage:{0} processed at {1}";
}
