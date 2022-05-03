using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceLocations;
using Object = UnityEngine.Object;

namespace Scripts.GUI
{
    public class UiInstance<T> : IUiInstance where T : MonoBehaviour
    {
        public GameObject Instance;
        public T Component;
        
        private AsyncOperationHandle<IList<IResourceLocation>> _resourceOperation;
        private AsyncOperationHandle<Object> _asyncOperation;

        public UiInstance(string name, GameObject parent, Action<T> onLoad = null, Action<T> onDestroy = null)
        {
            AddressablesUtils.Load<GameObject>("gui_prefabs", name, (handle) =>
            {                        
                Instance = Object.Instantiate(handle.Result, parent.transform);
                Component = Instance.GetComponent<T>();
                onLoad?.Invoke(Component);
                
            });
        }

        public void Destroy()
        {
            if (!_resourceOperation.IsDone) Addressables.Release(_resourceOperation);
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