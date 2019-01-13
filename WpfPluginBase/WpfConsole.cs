using System;
using EventBus;
using UploadClient.Events;

namespace WpfPluginBase
{
    public class WpfConsole
    {
        public static void WriteLine(string message)
        {
            SimpleEventBus.GetDefaultEventBus().Post(new ConsoleOutputEvent(message), TimeSpan.Zero);
        }
    }
}