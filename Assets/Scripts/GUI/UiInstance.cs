using System;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using Object = UnityEngine.Object;

namespace Scripts.GUI
{
    public class UiInstance<T> : IUiInstance where T : MonoBehaviour
    {
        public GameObject Instance;
        public T Component;
        
        private AsyncOperationHandle<Object> _asyncOperation;

        public UiInstance(string name, GameObject parent, Action<T> onLoad = null, Action<T> onDestroy = null)
        {
            _asyncOperation = Addressables.LoadAssetAsync<Object>(GlobalContextCore.AddressablesLocator.GetResourceLocation("gui_prefabs", name));
            _asyncOperation.Completed += handle =>
            {
                Instance = Object.Instantiate((GameObject)handle.Result, parent.transform);
                Component = Instance.GetComponent<T>();
                onLoad?.Invoke(Component);
                Addressables.Release(handle);
            };
        }

        public void Destroy()
        {
            if(!_asyncOperation.IsDone) Addressables.Release(_asyncOperation);
            Object.Destroy(Instance);
        }

        public void Show()
        {
            Instance.SetActive(true);
        }

        public void Hide()
        {
            Instance.SetActive(false);
        }
    }
}