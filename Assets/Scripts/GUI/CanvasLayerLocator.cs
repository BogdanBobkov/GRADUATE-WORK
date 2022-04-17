using System.Collections.Generic;
using UnityEngine;

namespace GUI
{
    public class CanvasLayerLocator
    {
        private readonly Dictionary<string, GameObject> _tagToCanvasLayer = new Dictionary<string, GameObject>();
        
        public CanvasLayerLocator(IEnumerable<string> canvasTags)
        {
            foreach (var canvasTag in canvasTags)
            {
                var canvasObject = GameObject.FindGameObjectWithTag(canvasTag);
                if (canvasObject)
                {
                    _tagToCanvasLayer.Add(canvasTag, canvasObject);
                }
            }
        }
        
        public GameObject GetCanvasObject(string canvasName)
        {
            return _tagToCanvasLayer[canvasName];
        }
    }
}