namespace UploadClient.Events
{
    public class ConsoleOutputEvent
    {
        public readonly string ConsoleOutputText;

        public ConsoleOutputEvent(string consoleOutputText)
        {
            ConsoleOutputText = consoleOutputText;
        }
    }
}