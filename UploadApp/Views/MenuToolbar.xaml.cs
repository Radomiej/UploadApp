using System;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using EventBus;
using UploadClient.Events;
using WpfPluginBase;

namespace UploadApp.Views
{
    public partial class MenuToolbar : UserControl
    {
        
        public MenuToolbar()
        {
            InitializeComponent();
            SimpleEventBus.GetDefaultEventBus().Register(this);
        }

        private void MenuToolbar_OnLoaded(object sender, RoutedEventArgs e)
        {
        }
        
        private void AppExit_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.Application.Current.Shutdown();
        }

        [EventSubscriber]
        public void AddMenuItem(AddMenuItemEvent addMenuItemEvent)
        {
            
        }

        private void Lang_English_OnClick(object sender, RoutedEventArgs e)
        {
           ChangeLanguage("en-US");
        }
        
        private void Lang_Poland_OnClick(object sender, RoutedEventArgs e)
        {
            ChangeLanguage("pl-PL");
        }

        private void ChangeLanguage(string language)
        {
            var culture = CultureInfo.GetCultureInfo(language);
            var oldCulture = Dispatcher.Thread.CurrentCulture;

            Dispatcher.Thread.CurrentCulture = culture;
            Dispatcher.Thread.CurrentUICulture = culture;

            ChangeLanguageEvent changeLanguageEvent = new ChangeLanguageEvent(culture.Name, oldCulture.Name);
            SimpleEventBus.GetDefaultEventBus().Post(changeLanguageEvent, TimeSpan.Zero);
            
            WpfConsole.WriteLine("Change localization on: " + culture.Name + " from: " + oldCulture.Name);
        }

       
    }
}
