using System.Collections.Generic;
using Morpeh;
using Scripts.ECS.Entities;
using Scripts.ECS.Systems;
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
                
                systemsGroup.AddInitializer(new MainMenuMechanicsSystem());
            
                World.Default.AddSystemsGroup(order: 0, systemsGroup);

                StartGame();
            });
            StartCoroutine(enumerator);
        }
        
        private void StartGame()
        {
            GuiEntity.AddComponent<GuiTutorialComponent>();
        }
    }
}
