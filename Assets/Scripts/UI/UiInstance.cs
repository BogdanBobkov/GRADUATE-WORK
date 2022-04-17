using System;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using Object = UnityEngine.Object;

namespace Scripts.UI
{
    public class UiInstance<T> : IUiInstance where T : MonoBehaviour
    {
        public GameObject Instance;
        public T Component;
        
        private AsyncOperationHandle<Object> _asyncOperation;

        public UiInstance(string name, Action onLoad, Action onDestroy)
        {
            _asyncOperation = Addressables.LoadAssetAsync<Object>(AddressablesLocator.GetResourceLocation("gui_prefabs", name));
            _asyncOperation.Completed += handle =>
            {
                Instance = (GameObject) handle.Result;
                Component = Instance.GetComponent<T>();
                Addressables.Release(handle);
            };
        }

        public void Destroy()
        {
            throw new NotImplementedException();
        }

        public void Show()
        {
            throw new NotImplementedException();
        }

        public void Hide()
        {
            throw new NotImplementedException();
        }
    }
}