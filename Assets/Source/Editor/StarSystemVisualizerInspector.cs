using UnityEngine;
using UnityEditor;

namespace NB.SpaceGame
{
    [CustomEditor(typeof(StarSystemVisualizer))]
    public class StarSystemVisualizerInspector : Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            var visualizer = target as StarSystemVisualizer;

            if (!visualizer.simulateOnUpdate && GUILayout.Button("Calculate"))
            {
                visualizer.CalculateTrajectory();
            }
        }
    }
}
