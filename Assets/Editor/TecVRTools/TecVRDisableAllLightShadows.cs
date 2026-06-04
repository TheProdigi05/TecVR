using UnityEditor;
using UnityEngine;

public static class TecVRDisableAllLightShadows
{
    [MenuItem("Tools/TecVR/Disable Shadows On All Scene Lights")]
    public static void DisableAllLightShadows()
    {
        Light[] lights = Object.FindObjectsByType<Light>(FindObjectsSortMode.None);

        int count = 0;

        foreach (Light light in lights)
        {
            Undo.RecordObject(light, "Disable Light Shadows");

            light.shadows = LightShadows.None;

            EditorUtility.SetDirty(light);
            count++;
        }

        Debug.Log($"Sombras desactivadas en {count} luces.");
    }
}