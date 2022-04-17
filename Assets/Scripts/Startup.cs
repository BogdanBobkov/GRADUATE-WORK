using System.Collections.Generic;
using GUI;
using Morpeh;
using Scripts.ECS.Entities;
using Scripts.ECS.Systems;
using Scripts.GUI;
using UnityEngine;

namespace Scripts
{
    public class Startup : MonoBehaviour
    {
        void Start()
        {
            GlobalContextCore.Camera = Camera.main;
            GlobalContextCore.CanvasLayerLocator = new CanvasLayerLocator(CanvasLayer.GetAllCanvasLayers());
            GlobalContextCore.AddressablesLocator = new AddressablesLocator();
            
            var addressableLabels = new List<string>
            {
                "gui_prefabs"
            };

            var enumerator = GlobalContextCore.AddressablesLocator.Load(addressableLabels, () =>
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
