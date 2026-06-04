using UnityEditor;
using UnityEngine;

public static class TecVRLightAudit
{
    [MenuItem("Tools/TecVR/Report Scene Lights")]
    public static void ReportSceneLights()
    {
        Light[] lights = Object.FindObjectsByType<Light>(FindObjectsSortMode.None);

        int total = 0;
        int active = 0;
        int realtime = 0;
        int baked = 0;
        int mixed = 0;
        int shadows = 0;

        foreach (Light light in lights)
        {
            total++;

            if (light.gameObject.activeInHierarchy && light.enabled)
                active++;

            if (light.lightmapBakeType == LightmapBakeType.Realtime)
                realtime++;

            if (light.lightmapBakeType == LightmapBakeType.Baked)
                baked++;

            if (light.lightmapBakeType == LightmapBakeType.Mixed)
                mixed++;

            if (light.shadows != LightShadows.None)
                shadows++;

            Debug.Log(
                $"LIGHT: {light.name} | Type: {light.type} | Active: {light.gameObject.activeInHierarchy} | " +
                $"Mode: {light.lightmapBakeType} | Shadows: {light.shadows} | Intensity: {light.intensity} | Range: {light.range}"
            );
        }

        Debug.Log(
            $"LIGHT REPORT → Total: {total}, Active: {active}, Realtime: {realtime}, Baked: {baked}, Mixed: {mixed}, With Shadows: {shadows}"
        );
    }
}