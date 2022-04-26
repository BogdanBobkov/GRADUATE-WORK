using System;
using TriLibCore;
using UnityEngine;

namespace Scripts.LoadingModel
{
    public class LoaderPickerManager
    {
        public Action<AssetLoaderContext, float> OnProgress;
        public Action<bool> OnBeginLoad;
        public Action<AssetLoaderContext> OnLoad;

        private GameObject _loadedGameObject;
        
        public void LoadModel()
        {
            var assetLoaderOptions = AssetLoader.CreateDefaultLoaderOptions();
            var assetLoaderFilePicker = AssetLoaderFilePicker.Create();
            assetLoaderFilePicker.LoadModelFromFilePickerAsync("Select a Model file", OnLoad, OnMaterialsLoad, OnProgress, OnBeginLoad, OnError, null, assetLoaderOptions);
        }
        
        private void OnError(IContextualizedError obj)
        {
            Debug.LogError($"An error occurred while loading your Model: {obj.GetInnerException()}");
        }
        
        private void OnMaterialsLoad(AssetLoaderContext assetLoaderContext)
        {
            if (assetLoaderContext.RootGameObject != null)
            {
                Debug.Log("Model fully loaded.");
            }
            else
            {
                Debug.Log("Model could not be loaded.");
            }
        }
    }
}
