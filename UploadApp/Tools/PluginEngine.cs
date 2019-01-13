using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using EventBus;
using WpfPluginBase;

namespace UploadClient.Tools
{
    public class PluginEngine
    {
        
        private List<string> pluginsPath = new List<string>();
        
        public void FindPlugins(string path)
        {
            if (!Directory.Exists(path))
            {
                WpfConsole.WriteLine("Plugin path: " + path + " NOT FOUND!");
                return;
            }
            
            //First empty the collection, we're reloading them all
            pluginsPath.Clear();

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
            
            WpfConsole.WriteLine("Finds " + pluginsPath.Count + " plugins");
        }

        public void InitializePlugins(string pluginPath)
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