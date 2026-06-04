using UnityEditor;
using UnityEngine;

public static class TecVROptimizeLightsSelected
{
    [MenuItem("Tools/TecVR/Optimize Selected Lights For Quest")]
    public static void OptimizeSelectedLights()
    {
        GameObject[] selected = Selection.gameObjects;

        if (selected == null || selected.Length == 0)
        {
            Debug.LogWarning("Selecciona un grupo de luces o un objeto raíz primero.");
            return;
        }

        int optimized = 0;

        foreach (GameObject root in selected)
        {
            Light[] lights = root.GetComponentsInChildren<Light>(true);

            foreach (Light light in lights)
            {
                Undo.RecordObject(light, "Optimize Light For Quest");

                light.shadows = LightShadows.None;
                light.lightmapBakeType = LightmapBakeType.Realtime;
                light.renderMode = LightRenderMode.Auto;

                if (light.type == LightType.Point)
                {
                    light.range = Mathf.Min(light.range, 10f);
                    light.intensity = Mathf.Min(light.intensity, 2f);
                }

                if (light.type == LightType.Spot)
                {
                    light.range = Mathf.Min(light.range, 15f);
                    light.intensity = Mathf.Min(light.intensity, 3f);
                    light.spotAngle = Mathf.Clamp(light.spotAngle, 35f, 65f);
                }

                EditorUtility.SetDirty(light);
                optimized++;
            }
        }

        Debug.Log($"Luces optimizadas para Quest: {optimized}");
    }
}