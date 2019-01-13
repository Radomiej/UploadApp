using System;
using EventBus;
using UploadClient.Events;
using WpfPluginBase;

namespace PluginMusicPlayer
{
    public class PluginApp : IBasePlugin
    {
        public string GetPluginName()
        {
            return "Music Player";
        }

        public void Dispose()
        {
        }

        public string Initialize(SimpleEventBus eventBus)
        {
            MusicPlayerWindow musicPlayerWindow = new MusicPlayerWindow();
            eventBus.Post(new ShowWindowEvent(musicPlayerWindow), TimeSpan.Zero);
            
            WpfConsole.WriteLine("Hello from Music Plugin!");

            return "OK";
        }
    }
}