using System;
using System.Windows;
using System.Windows.Controls;
using EventBus;
using UploadApp.Views;
using UploadClient.Events;
using UploadClient.Tools;
using WpfPluginBase;

namespace UploadApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
      
        private I18NEngine _i18NEngine;
        
        public MainWindow()
        {
            InitializeComponent();
            SimpleEventBus.GetDefaultEventBus().Register(this);
            LoadPlugins();
        }


        private void LoadPlugins()
        {
            string pluginPath = @"H:\Dokumenty\Kod\C#\UploadApp\UploadApp\PluginMusicPlayer\bin\Debug\";
            WpfConsole.WriteLine("Load Plugins from: " + pluginPath);
            PluginEngine pluginEngine = new PluginEngine();
            pluginEngine.FindPlugins(pluginPath);
            pluginEngine.InitializePlugins(pluginPath);
        }


        [EventSubscriber]
        public void HandleOutputEvent(ShowWindowEvent showWindowEvent)
        {
            Application.Current.Dispatcher.Invoke(
                () => { showWindowEvent.windowToShow.Show(); });
        }



        

        private void MainWindow_OnLoaded(object sender, RoutedEventArgs e)
        {
            _i18NEngine = new I18NEngine(this, Properties.Resources.ResourceManager);
            _i18NEngine.UpdateLanguage();
        }

    }
}