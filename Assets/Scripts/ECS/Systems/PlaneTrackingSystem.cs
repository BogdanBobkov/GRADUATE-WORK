using System;
using System.Collections.Generic;
using Morpeh;
using Unity.IL2CPP.CompilerServices;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using Object = UnityEngine.Object;

namespace Scripts.ECS.Systems
{
    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
    public class PlaneTrackingSystem : ISystem
    {
        public World World { get; set; }
        private ARRaycastManager _RaycastManager;
        private List<ARRaycastHit> s_Hits = new();
        private Transform _PlacementObject;
        private GameObject _gameObject;
        
        public void OnAwake()
        {
            AddressablesUtils.Load<GameObject>("ar_prefabs", "indicator", handle =>
            {
                _PlacementObject = Object.Instantiate(handle.Result).transform;
            });
            
            _RaycastManager = Object.FindObjectOfType<ARRaycastManager>();
            GuiMainHudSystem.OnLoadGameObject += OnInstantiateGO;
        }

        public void OnUpdate(float deltaTime)
        {
            if (_PlacementObject != null)
            {
                List<ARRaycastHit> hits = new List<ARRaycastHit>();
                _RaycastManager.Raycast(new Vector2(Screen.width / 2, Screen.height / 2), hits, TrackableType.Planes);

                if (hits.Count > 0)
                {
                    _PlacementObject.position = hits[0].pose.position;
                    _PlacementObject.rotation = hits[0].pose.rotation;
                }
            }

            if (!TryGetTouchPosition(out Vector2 touchPosition))
                return;

            if (_RaycastManager.Raycast(touchPosition, s_Hits, TrackableType.PlaneWithinPolygon) && _gameObject != null)
            {
                var hitPose = s_Hits[0].pose;

                _gameObject.transform.position = hitPose.position;
                
                _gameObject.SetActive(true);
                _PlacementObject.gameObject.SetActive(false);
            }
        }

        private bool TryGetTouchPosition(out Vector2 touchPosition)
        {
            if (Input.touchCount > 0)
            {
                touchPosition = Input.GetTouch(0).position;
                return true;
            }

            touchPosition = default;
            return false;
        }

        private void OnInstantiateGO(GameObject gameObject)
        {
            if(_gameObject != null)
                Object.Destroy(_gameObject);
            
            _gameObject = gameObject;
            _gameObject.SetActive(false);
        }
        
        public void Dispose()
        {
            
        }
    }
}