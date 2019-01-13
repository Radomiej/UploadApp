namespace UploadClient.Events
{
    public class FileSelectedEvent
    {
        public readonly string FilePath;

        public FileSelectedEvent(string filePath)
        {
            FilePath = filePath;
        }
    }
}