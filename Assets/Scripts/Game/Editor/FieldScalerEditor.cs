#if UNITY_EDITOR
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace Digg.Game
{
    [CanEditMultipleObjects]
    [CustomEditor(typeof(FieldScaler))]
    public class FieldScalerEditor : Editor 
    {
        public override void OnInspectorGUI()
        {
            var scaler = ((FieldScaler)target);
            DrawDefaultInspector();
            
            if (GUILayout.Button("Rescale")) scaler.Rescale();
        }
    }
}
#endif