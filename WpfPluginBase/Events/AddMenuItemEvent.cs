using System.Resources;
using System.Windows.Media;

namespace UploadClient.Events
{
    public class AddMenuItemEvent
    {
        public string Category { get; }
        public Visual MenuItem { get; }
        public ResourceManager ResourceManager { get; }

        public AddMenuItemEvent(string category, Visual menuItem, ResourceManager resourceManager)
        {
            Category = category;
            MenuItem = menuItem;
            ResourceManager = resourceManager;
        }
    }
}