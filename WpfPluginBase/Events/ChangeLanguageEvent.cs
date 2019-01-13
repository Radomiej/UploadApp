namespace UploadClient.Events
{
    public class ChangeLanguageEvent
    {
        public readonly string oldLanguage;
        public readonly string newLanguage;

        public ChangeLanguageEvent(string newLanguage, string oldLanguage)
        {
            this.newLanguage = newLanguage;
            this.oldLanguage = oldLanguage;
        }
    }
}