using System;
using System.Security.Cryptography.X509Certificates;
using Morpeh;
using Scripts.GUI;
using Scripts.GUI.MonoBehaviours;
using TriLibCore;
using TriLibCore.Extensions;
using Unity.IL2CPP.CompilerServices;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Scripts.ECS.Systems
{
    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
    public class GuiMainHudSystem : ISystem
    {
        public World World { get; set; }
        private Filter _filter;
        private UiInstance<GuiHudMainComponent> _instance;
        private GameObject _gameObject;

        public static event Action<GameObject> OnLoadGameObject;

        public void OnAwake()
        {
            _filter = World.Filter
                .With<GuiMainHudComponent>()
                .With<DirtyComponent<GuiMainHudComponent>>();
        }

        public void OnUpdate(float deltaTime)
        {
            foreach (var entity in _filter)
            {
                _instance = new UiInstance<GuiHudMainComponent>("main_hud", 
                    GlobalContextCore.CanvasLayerLocator.GetCanvasObject(CanvasLayer.UI),
                    onLoad: component =>
                    {
                        component.OnLoadModel += OnLoadClickHandler;
                        GlobalContextCore.LoaderPickerManager.OnLoad += OnLoadHandler;
                    },
                    onDestroy: component =>
                    {
                        GlobalContextCore.LoaderPickerManager.OnLoad -= OnLoadHandler;
                        entity.Dispose();
                    });
                entity.RemoveComponent<DirtyComponent<GuiMainHudComponent>>();
            }
        }
        
        public void Dispose()
        {
            _filter = null;
        }
        
        #region HANDLERS
        
        private void OnLoadClickHandler()
        {
            GlobalContextCore.LoaderPickerManager.LoadModel();
        }

        private void OnLoadHandler(AssetLoaderContext context)
        {
            GlobalContextCore.Camera.FitToBounds(context.RootGameObject, 2f);
            _instance.Component.SetStateButton(true);
            
            OnLoadGameObject?.Invoke(context.RootGameObject);
        }
        
        #endregion
    }
}