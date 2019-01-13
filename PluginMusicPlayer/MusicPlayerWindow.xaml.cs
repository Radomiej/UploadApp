using System;
using System.Collections.Generic;
using System.Reflection;
using System.Resources;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using EventBus;
using UploadClient.Events;
using WpfPluginBase;

namespace PluginMusicPlayer
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MusicPlayerWindow
    {
        private I18NEngine _i18NEngine;
        public MusicPlayerWindow()
        {
            InitializeComponent();
            SimpleEventBus.GetDefaultEventBus().Register(this);

            AddMenuPlayButton();
        }

        private void AddMenuPlayButton()
        {
            MenuItem menuItem = new MenuItem();
            menuItem.Header = "Text_Play";
            
            AddMenuItemEvent addMenuItemEvent = new AddMenuItemEvent("file", menuItem, Properties.Resources.ResourceManager);
            SimpleEventBus.GetDefaultEventBus().Post(addMenuItemEvent, TimeSpan.Zero);
            
        }

        private void MusicPlayerWindow_OnLoaded(object sender, RoutedEventArgs e)
        {
            _i18NEngine = new I18NEngine(this, Properties.Resources.ResourceManager);
            _i18NEngine.UpdateLanguage();
        }
    }
}