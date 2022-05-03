using System;
using System.IO;
using System.Linq;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace Scripts
{
    public static class AddressablesUtils
    {
        public static void Load<T>(string label, string name, Action<AsyncOperationHandle<T>> callback)
        {
            Addressables.LoadResourceLocationsAsync(label, typeof(object)).Completed += handleLocation =>
            {
                var location = handleLocation.Result.FirstOrDefault(t => Path.GetFileNameWithoutExtension(t.ToString()) == name);
            
                Addressables.LoadAssetAsync<T>(location).Completed += handle =>
                {
                    callback?.Invoke(handle);
                };
            };
        }
    }
}