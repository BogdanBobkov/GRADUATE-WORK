using Morpeh;
using Scripts.GUI;
using Scripts.GUI.MonoBehaviours;
using Unity.IL2CPP.CompilerServices;

namespace Scripts.ECS.Systems
{
    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
    public class GuiTutorialSystem : ISystem
    {
        public World World { get; set; }
        private Filter _filter;
        private UiInstance<GuiTutorialPanelComponent> _instance;

        public void OnAwake()
        {
            _filter = World.Filter
                .With<GuiTutorialComponent>()
                .With<DirtyComponent<GuiTutorialComponent>>();
        }

        public void OnUpdate(float deltaTime)
        {
            foreach (var entity in _filter)
            {
                _instance = new UiInstance<GuiTutorialPanelComponent>("tutorial_panel", 
                    GlobalContextCore.CanvasLayerLocator.GetCanvasObject(CanvasLayer.UI),
                    onLoad: component =>
                    {
                        component.OnCloseClick += Close;
                    },
                    onDestroy: component =>
                    {
                        entity.Dispose();
                    });

                entity.RemoveComponent<GuiTutorialComponent>();
            }
        }

        private void Close()
        {
            _instance?.Destroy();
            _instance = null;
        }

        public void Dispose()
        {
            _filter = null;
        }
    }
}