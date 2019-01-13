namespace UploadClient.Events
{
    public class ConsoleInputEvent
    {
        public readonly string ConsoleInputText;

        public ConsoleInputEvent(string consoleInputText)
        {
            ConsoleInputText = consoleInputText;
        }
    }
}