using System;
using System.Collections.Generic;
using System.Resources;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using EventBus;
using UploadClient.Events;

namespace WpfPluginBase
{
    public class I18NEngine
    {
        private readonly Visual root;
        private readonly ResourceManager _resourceManager;
        private readonly Dictionary<Visual, string> _elements;
        private readonly Dictionary<MenuItem, string> _menuItems;
        
        public I18NEngine(Visual root, ResourceManager resourceManager, bool registerInDefaultEventListener = true)
        {
            this.root = root;
            _resourceManager = resourceManager;
            _elements = new Dictionary<Visual, string>(100);
            _menuItems = new Dictionary<MenuItem, string>(100);
            
            GetControlsToTranslateKeyDictionary(root);
            if(registerInDefaultEventListener) SimpleEventBus.GetDefaultEventBus().Register(this);
        }
        
        private void GetControlsToTranslateKeyDictionary(Visual root)
        {
            int ChildNumber = VisualTreeHelper.GetChildrenCount(root);

            for (int i = 0; i <= ChildNumber - 1; i++)
            {
                Visual v = (Visual)VisualTreeHelper.GetChild(root, i);
                AddControlsToTranslateKeyDictionary(v);
            }
        }

        private void AddControlsToTranslateKeyDictionary(Visual control)
        {
            const int indent = 4;
            int ChildNumber = VisualTreeHelper.GetChildrenCount(control);

            for (int i = 0; i <= ChildNumber - 1; i++)
            {
                Visual v = (Visual)VisualTreeHelper.GetChild(control, i);
                if (v is ComboBox)
                {
                    ProcessComboBox(v as ComboBox);
                }else if (v is ContentControl)
                {
                    ProcessContentControl(v as ContentControl);
                }else if (v is MenuItem)
                {
                    ProcessMenuItem(v as MenuItem);
                }
                

                if (VisualTreeHelper.GetChildrenCount(v) > 0)
                {
                    AddControlsToTranslateKeyDictionary(v);
                }
            }
        }

        private void ProcessContentControl(ContentControl contentControl)
        {
            if(contentControl?.Content != null)
            {
                _elements.Add(contentControl, contentControl.Content.ToString());
            }
        }

        private void ProcessComboBox(ComboBox comboBox)
        {
            foreach (var item in comboBox.Items)
            {
                if(item is ContentControl)
                {
                    ProcessContentControl(item as ContentControl);
                }
            }
        }
        
        private void ProcessMenuItem(MenuItem menuItem)
        {
            if(menuItem?.Header != null)
            {
                string translateKey = menuItem.Header.ToString();
                _menuItems.Add(menuItem, translateKey);
            }

            foreach (var childMenuItem in menuItem.Items)
            {
                if(childMenuItem is MenuItem) ProcessMenuItem(childMenuItem as MenuItem);
                else if(childMenuItem is Visual) AddControlsToTranslateKeyDictionary(childMenuItem as Visual);
            }
        }

        [EventSubscriber]
        public void HandleOutputEvent(ChangeLanguageEvent changeLanguageEvent)
        {
            UpdateLanguage();
        }
        
        public void UpdateLanguage()
        {
            foreach(KeyValuePair<Visual, string> entry in _elements)
            {
                var childContentVisual = entry.Key as ContentControl;
                if(childContentVisual != null)
                {
                    string translateKey = _resourceManager.GetString(entry.Value);
                    if(translateKey == null) continue;
                    
                    Application.Current.Dispatcher.Invoke(
                        () => { childContentVisual.Content = translateKey; });
                }
            }
            
            foreach(KeyValuePair<MenuItem, string> entry in _menuItems)
            {
                var childContentVisual = entry.Key as MenuItem;
                if(childContentVisual != null)
                {
                    string translateKey = _resourceManager.GetString(entry.Value);
                    if(translateKey == null) continue;
                    
                    Application.Current.Dispatcher.Invoke(
                        () => { childContentVisual.Header = translateKey; });
                }
            }
        }
    }
}