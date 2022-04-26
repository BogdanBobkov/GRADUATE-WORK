using System.Collections.Generic;
using Morpeh;
using Scripts.ECS.Systems;
using Scripts.GUI;
using Scripts.LoadingModel;
using UnityEngine;

namespace Scripts
{
    public class Startup : MonoBehaviour
    {
        private void Start()
        {
            GlobalContextCore.Camera = Camera.main;
            GlobalContextCore.CanvasLayerLocator = new CanvasLayerLocator(CanvasLayer.GetAllCanvasLayers());
            GlobalContextCore.AddressablesLocator = new AddressablesLocator();
            GlobalContextCore.LoaderPickerManager = new LoaderPickerManager();
            
            var addressableLabels = new List<string>
            {
                "gui_prefabs"
            };

            var enumerator = GlobalContextCore.AddressablesLocator.Load(addressableLabels, () =>
            {
                var systemsGroup = World.Default.CreateSystemsGroup();
                
                systemsGroup.AddSystem(new GuiMainHudSystem());
                systemsGroup.AddSystem(new GuiTutorialSystem());
            
                World.Default.AddSystemsGroup(order: 0, systemsGroup);

                StartGame();
            });
            StartCoroutine(enumerator);
        }

        private void StartGame()
        {
            var tutorialEntity = World.Default.CreateEntity();
            tutorialEntity.AddComponent<GuiTutorialComponent>();
            tutorialEntity.AddComponent<DirtyComponent<GuiTutorialComponent>>();
        }
    }
}
