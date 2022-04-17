using Morpeh;
using Scripts.GUI;
using Scripts.GUI.MonoBehaviours;

namespace Scripts.ECS.Systems
{
    public class MainMenuMechanicsSystem : ISystem
    {
        public World World { get; set; }
        private Filter _filter;
        private UiInstance<MainMenuComponent> _instance;

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
                _instance = new UiInstance<MainMenuComponent>("main_menu",
                    onLoad: component =>
                    {
                        component.OnCloseClick += Close;
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

        }
    }
}