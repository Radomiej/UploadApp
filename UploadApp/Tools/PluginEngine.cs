using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Windows;
using EventBus;
using UploadClient.Events;
using WpfPluginBase;

namespace UploadClient.Tools
{
    public class PluginEngine
    {
        
        private List<string> pluginsPath = new List<string>();
        
        
        [EventSubscriber]
        public void HandleOutputEvent(ConsoleInputEvent inputEvent)
        {
            string outputText = inputEvent.ConsoleInputText;
            if (outputText.StartsWith("plugin"))
            {
                outputText = outputText.Replace("plugin", "");
                FindPluginInFolder(outputText);
                Application.Current.Dispatcher.Invoke(
                    InitializePlugins);
               

            }else if (outputText.StartsWith("plugins"))
            {
                outputText = outputText.Replace("plugins", "");
                FindPlugins(outputText);
                Application.Current.Dispatcher.Invoke(
                    InitializePlugins);
            }
        }
        
        public void FindPlugins(string path)
        {
            if (!Directory.Exists(path))
            {
                WpfConsole.WriteLine("Plugin path: " + path + " NOT FOUND!");
                return;
            }
            
            //First empty the collection, we're reloading them all
//            pluginsPath.Clear();
            foreach (string directoryOn in Directory.GetDirectories(path))
            {
                FindPluginInFolder(directoryOn);
            }


            WpfConsole.WriteLine("Finds " + pluginsPath.Count + " plugins");
        }

        public void FindPluginInFolder(string path)
        {
            //Go through all the files in the plugin directory
            foreach (string fileOn in Directory.GetFiles(path))
            {
                FileInfo file = new FileInfo(fileOn);

                //Preliminary check, must be .dll
                if (file.Extension.Equals(".exe") && file.Name.ToLower().Contains("plugin"))
                {
                    //Add the 'plugin'
                    pluginsPath.Add(fileOn);
                    WpfConsole.WriteLine("Find plugin: " + file.Name);
                }
            }
        }
        
        public void InitializePlugins()
        {
          
            foreach (string fileOn in pluginsPath)
            {
                Assembly assembly = Assembly.LoadFrom(fileOn);
                
                Type objectType = (from type in assembly.GetTypes()
                    where type.IsClass && type.Name == "PluginApp"
                    select type).Single();
                IBasePlugin basePlugin = (IBasePlugin) Activator.CreateInstance(objectType);
                WpfConsole.WriteLine("Initialize: " + basePlugin.GetPluginName());
                var result = basePlugin.Initialize(SimpleEventBus.GetDefaultEventBus());
                WpfConsole.WriteLine("Initialize result: " + result);
            }
        }
    }
}