using WMPLib;
using WpfPluginBase;

namespace PluginMusicPlayer.Actions
{
    public class PlayMusicAction : IBaseAction
    {
        public string FilePath { get; }

        private WindowsMediaPlayer oMediaPlayer;

        public PlayMusicAction(string filePath)
        {
            FilePath = filePath;

            oMediaPlayer = new WindowsMediaPlayer();
            oMediaPlayer.URL = FilePath;
            
           
        }

        public void DoAction()
        {
            oMediaPlayer.controls.stop();
            oMediaPlayer.controls.play();
        }

        public void UndoAction()
        {
            oMediaPlayer.controls.stop();
        }

        public void RedoAction()
        {
            throw new System.NotImplementedException();
        }
    }
}