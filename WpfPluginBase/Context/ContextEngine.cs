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

        private Dictionary<string, object> contextData = new Dictionary<string, object>(100);
        
        private ContextEngine()
        {
            // Initialize.
        }

        public void setContextObject(string key, object value)
        {
            object oldValue = getContextObject(key);
            ContextPropertiesChangedEvent contextPropertiesChangedEvent = new ContextPropertiesChangedEvent(key, oldValue, value);
            contextData.Add(key, value);
            SimpleEventBus.GetDefaultEventBus().Post(contextPropertiesChangedEvent, TimeSpan.Zero);
        }
        
        public string getStringContextObject(string key)
        {
            if (!contextData.ContainsKey(key)) return "";
            return contextData[key].ToString();
        }
        
        public object getContextObject(string key)
        {
            return contextData.ContainsKey(key) ? contextData[key] : null;
        }
    }
}