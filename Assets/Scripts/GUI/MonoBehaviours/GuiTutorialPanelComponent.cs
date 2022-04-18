using System;
using UnityEngine;
using UnityEngine.UI;

namespace Scripts.GUI.MonoBehaviours
{
    public class GuiTutorialPanelComponent : MonoBehaviour
    {
        [Header("References")] 
        [SerializeField] private Button _CloseButton;

        public Action OnCloseClick;

        private void Awake()
        {
            _CloseButton.onClick.AddListener(OnCloseHandler);
        }
        
        #region HANDLERS

        private void OnCloseHandler()
        {
            OnCloseClick?.Invoke();
        }
        
        #endregion
    }
}