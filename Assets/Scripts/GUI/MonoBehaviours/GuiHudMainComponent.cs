using System;
using UnityEngine;
using UnityEngine.UI;

namespace Scripts.GUI.MonoBehaviours
{
    public class GuiHudMainComponent : MonoBehaviour
    {
        [Header("References")] 
        [SerializeField] private Button _LoadModelButton;

        public Action OnLoadModel;

        private void Awake()
        {
            _LoadModelButton.onClick.AddListener(LoadModelHandler);
        }

        public void SetStateButton(bool state) => _LoadModelButton.interactable = state;
        
        #region HANLERS

        private void LoadModelHandler()
        {
            OnLoadModel?.Invoke();
        }
        
        #endregion
    }
}