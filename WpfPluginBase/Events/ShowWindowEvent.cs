using System.Windows;

namespace UploadClient.Events
{
    public class ShowWindowEvent
    {
        public Window windowToShow;

        public ShowWindowEvent(Window windowToShow)
        {
            this.windowToShow = windowToShow;
        }
    }
}