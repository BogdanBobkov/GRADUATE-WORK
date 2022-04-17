using System.Collections.Generic;
using Morpeh;
using UnityEngine;

namespace Scripts
{
    public class Startup : MonoBehaviour
    {
        void Start()
        {
            var addressableLabels = new List<string>
            {
                "gui_prefabs"
            };

            var enumerator = AddressablesLocator.Load(addressableLabels, () =>
            {
                var systemsGroup = World.Default.CreateSystemsGroup();
            
                World.Default.AddSystemsGroup(order: 0, systemsGroup);
            });
            StartCoroutine(enumerator);
        }
    }
}
