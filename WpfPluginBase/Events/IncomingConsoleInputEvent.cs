namespace UploadClient.Events
{
    public class IncomingConsoleInputEvent
    {
        public readonly string ConsoleInputText;

        public IncomingConsoleInputEvent(string consoleInputText)
        {
            ConsoleInputText = consoleInputText;
        }
    }
}