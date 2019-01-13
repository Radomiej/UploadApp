namespace UploadClient.Events
{
    public class FileDeselectedEvent
    {
        public readonly string FilePath;

        public FileDeselectedEvent(string filePath)
        {
            FilePath = filePath;
        }
    }
}