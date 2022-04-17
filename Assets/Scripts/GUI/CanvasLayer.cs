using System;
using System.Collections.Generic;
using Morpeh;

namespace Scripts.GUI
{
    public static class CanvasLayer
    {
        public const string UI = "CanvasLayerUI";

        private static List<string> _allLayers;

        static CanvasLayer()
        {
            _allLayers = new List<string>
            {
                UI
            };
        }
        
        public static List<string> GetAllCanvasLayers()
        {
            return _allLayers;
        }
    }
}
