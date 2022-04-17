using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.ResourceLocations;

namespace Scripts
{
    public class AddressablesLocator
    {
        private Dictionary<string, Dictionary<string, IResourceLocation>> resourceLocationsByLabels =
            new Dictionary<string, Dictionary<string, IResourceLocation>>();

        public IEnumerator Load(List<string> labels, Action callback)
        {
            foreach (var label in labels)
            {
                var data = new Dictionary<string, IResourceLocation>();
                resourceLocationsByLabels.Add(label, data);

                var handle = Addressables.LoadResourceLocationsAsync(label, typeof(object));
                yield return handle;
                
                foreach (var resourceLocation in handle.Result)
                {
                    var fileName = Path.GetFileNameWithoutExtension(resourceLocation.ToString());
                    if (data.ContainsKey(fileName))
                    {
                        //Debug.LogError($"ArgumentException: An item with the same key has already been added. Key: {fileName}");
                        continue;
                    }

                    data.Add(fileName, resourceLocation);

                    //Debug.Log($"Loading {label}/{fileName}");
                }

                Addressables.Release(handle);
            }
            
            callback?.Invoke();
        }

        public IResourceLocation GetResourceLocation(string label, string name)
        {
            if (!resourceLocationsByLabels.ContainsKey(label))
            {
                Debug.LogError($"Не найден лейбл {label} в адресаблах");
            }
            
            if (!resourceLocationsByLabels[label].ContainsKey(name))
            {
                Debug.LogError($"Не найден путь {name} в лейбле {label}");
            }
            
            return resourceLocationsByLabels[label][name];
        }
    }
}