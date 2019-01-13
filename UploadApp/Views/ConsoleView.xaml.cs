using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using EventBus;
using UploadClient.Events;

namespace UploadApp.Views
{
    public partial class ConsoleView : UserControl
    {
        ConsoleContent dc = new ConsoleContent();

        public ConsoleView()
        {
            InitializeComponent();
            SimpleEventBus.GetDefaultEventBus().Register(this);
            DataContext = dc;
            Loaded += MainWindow_Loaded;
        }

        void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            InputBlock.KeyDown += InputBlock_KeyDown;
            InputBlock.Focus();
        }

        void InputBlock_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                dc.ConsoleInput = InputBlock.Text;
                dc.RunCommand();
                InputBlock.Focus();
                Scroller.ScrollToBottom();
            }
        }

        [EventSubscriber]
        public void HandleOutputEvent(ConsoleOutputEvent outputEvent)
        {
            Application.Current.Dispatcher.Invoke(
                () => { dc.ConsoleOutput.Add(outputEvent.ConsoleOutputText); });
        }
    }

    public class ConsoleContent : INotifyPropertyChanged
    {
        string consoleInput = string.Empty;
        ObservableCollection<string> consoleOutput = new ObservableCollection<string>() {"Application start...", "Type: plugin <FilePath> to load plugin"};

        public string ConsoleInput
        {
            get { return consoleInput; }
            set
            {
                consoleInput = value;
                OnPropertyChanged("ConsoleInput");
            }
        }

        public ObservableCollection<string> ConsoleOutput
        {
            get { return consoleOutput; }
            set
            {
                consoleOutput = value;
                OnPropertyChanged("ConsoleOutput");
            }
        }

        public void RunCommand()
        {
            ConsoleOutput.Add(ConsoleInput);
            // do your stuff here.
            SimpleEventBus.GetDefaultEventBus().Post(new ConsoleInputEvent(ConsoleInput), TimeSpan.Zero);
            ConsoleInput = String.Empty;
        }


        public event PropertyChangedEventHandler PropertyChanged;

        void OnPropertyChanged(string propertyName)
        {
            if (null != PropertyChanged)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}