namespace UploadClient.Events
{
    public class IncomingConsoleOutputEvent
    {
        public readonly string ConsoleOutputText;

        public IncomingConsoleOutputEvent(string consoleOutputText)
        {
            ConsoleOutputText = consoleOutputText;
        }
    }
}