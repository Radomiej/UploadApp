using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Resources;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using EventBus;
using PluginMusicPlayer.Actions;
using UploadClient.Events;
using WpfPluginBase;
using WpfPluginBase.Context;

namespace PluginMusicPlayer
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MusicPlayerWindow
    {
        private I18NEngine _i18NEngine;
        private MenuItem menuItem;
        
        public MusicPlayerWindow()
        {
            InitializeComponent();
            SimpleEventBus.GetDefaultEventBus().Register(this);

        }

        private void AddMenuPlayButton()
        {
            menuItem = new MenuItem();
            menuItem.Header = "Text_Play";
            menuItem.IsEnabled = false;
            menuItem.Click += ButtonPlay_OnClick;
            
            AddMenuItemEvent addMenuItemEvent = new AddMenuItemEvent("file", menuItem, Properties.Resources.ResourceManager);
            SimpleEventBus.GetDefaultEventBus().Post(addMenuItemEvent, TimeSpan.Zero);

            _i18NEngine.AddTranslateElement(menuItem);
        }

        private void MusicPlayerWindow_OnLoaded(object sender, RoutedEventArgs e)
        {
            _i18NEngine = new I18NEngine(this, Properties.Resources.ResourceManager);
            AddMenuPlayButton();
            _i18NEngine.UpdateLanguage();
        }
        
        [EventSubscriber]
        public void HandlePropertiesChangedEvent(ContextPropertiesChangedEvent propertiesChangedEvent)
        {
            if (!propertiesChangedEvent.Key.Equals("selectedFile")) return;

            string filePath = propertiesChangedEvent.NewValue.ToString();
            string fileExtansion = Path.GetExtension(filePath).ToUpper();

            bool playButtonEnabled = fileExtansion.Equals(".MP3");
            Application.Current.Dispatcher.Invoke(
                () => { ButtonPlay.IsEnabled = playButtonEnabled; menuItem.IsEnabled = playButtonEnabled;});
            
        }

        private void ButtonPlay_OnClick(object sender, RoutedEventArgs e)
        {
            string filePath = ContextEngine.Instance.GetStringContextObject("selectedFile");
            if (!File.Exists(filePath)) return;
            
            PlayMusicAction playMusicAction = new PlayMusicAction(filePath);
            SimpleEventBus.GetDefaultEventBus().Post(new DoActionEvent(playMusicAction), TimeSpan.Zero);
        }
    }
}