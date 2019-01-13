using EventBus;

namespace WpfPluginBase
{
    public interface IBasePlugin
    {
        string GetPluginName();
        string Initialize(SimpleEventBus eventBus);
        void Dispose();
    }
}