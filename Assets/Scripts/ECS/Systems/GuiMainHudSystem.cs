using Morpeh;
using Unity.IL2CPP.CompilerServices;

namespace Scripts.ECS.Systems
{
    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
    public class GuiMainHudSystem : ISystem
    {
        public World World { get; set; }
        private Filter _filter;

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

                entity.RemoveComponent<GuiMainHudComponent>();
            }
        }
        
        public void Dispose()
        {
            _filter = null;
        }
    }
}