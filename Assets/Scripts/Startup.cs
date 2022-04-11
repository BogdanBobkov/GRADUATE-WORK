using Morpeh;
using UnityEngine;

namespace Scripts
{
    public class Startup : MonoBehaviour
    {
        void Start()
        {
            var systemsGroup = World.Default.CreateSystemsGroup();
            
            World.Default.AddSystemsGroup(order: 0, systemsGroup);
        }
    }
}
