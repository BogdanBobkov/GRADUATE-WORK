using Morpeh;

namespace Scripts.ECS.Entities
{
    public static class GuiEntity
    {
        private static Entity _instance = World.Default.CreateEntity();

        public static void AddComponent<T>() where T : struct, IComponent
        {
            _instance.AddComponent<T>();
            _instance.AddComponent<DirtyComponent<T>>();
        }
    }
}