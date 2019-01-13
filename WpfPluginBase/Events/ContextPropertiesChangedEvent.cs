namespace UploadClient.Events
{
    public class ContextPropertiesChangedEvent
    {
        public string Key { get; }
        public object OldValue { get; }
        public object NewValue { get; }

        public ContextPropertiesChangedEvent(string key, object oldValue, object newValue)
        {
            Key = key;
            OldValue = oldValue;
            NewValue = newValue;
        }
    }
}