using System;
using System.Collections.Generic;
using EventBus;
using UploadClient.Events;

namespace WpfPluginBase.Context
{
    public class ContextEngine
    {
        private static ContextEngine _instance;
        public static ContextEngine Instance => _instance ?? (_instance = new ContextEngine());

        private readonly Dictionary<string, object> _contextData = new Dictionary<string, object>(100);

        private ContextEngine()
        {
            // Initialize.
        }

        public void SetContextObject(string key, object value)
        {
            object oldValue = GetContextObject(key);
            ContextPropertiesChangedEvent contextPropertiesChangedEvent =
                new ContextPropertiesChangedEvent(key, oldValue, value);
            
            if (_contextData.ContainsKey(key)) _contextData[key] = value;
            else _contextData.Add(key, value);
            
            SimpleEventBus.GetDefaultEventBus().Post(contextPropertiesChangedEvent, TimeSpan.Zero);
        }

        public string GetStringContextObject(string key)
        {
            if (!_contextData.ContainsKey(key)) return "";
            return _contextData[key].ToString();
        }

        public object GetContextObject(string key)
        {
            return _contextData.ContainsKey(key) ? _contextData[key] : null;
        }
    }
}