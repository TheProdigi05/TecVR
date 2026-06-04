using UnityEditor;
using UnityEngine;

public static class FixSelectedMaterialsNoGlow
{
    [MenuItem("Tools/TecVR/Fix Selected Materials No Glow")]
    public static void FixMaterials()
    {
        GameObject root = Selection.activeGameObject;

        if (root == null)
        {
            Debug.LogWarning("Selecciona el objeto raíz del edificio primero.");
            return;
        }

        Renderer[] renderers = root.GetComponentsInChildren<Renderer>(true);
        int fixedMaterials = 0;

        foreach (Renderer renderer in renderers)
        {
            foreach (Material mat in renderer.sharedMaterials)
            {
                if (mat == null) continue;

                Undo.RecordObject(mat, "Fix Material No Glow");

                // Apagar emisión común en URP/Lit, Standard y varios shaders importados.
                if (mat.HasProperty("_EmissionColor"))
                {
                    mat.SetColor("_EmissionColor", Color.black);
                }

                if (mat.HasProperty("_EmissionMap"))
                {
                    mat.SetTexture("_EmissionMap", null);
                }

                mat.DisableKeyword("_EMISSION");
                mat.globalIlluminationFlags = MaterialGlobalIlluminationFlags.EmissiveIsBlack;

                // Bajar brillo/reflejo.
                if (mat.HasProperty("_Metallic"))
                {
                    mat.SetFloat("_Metallic", 0f);
                }

                if (mat.HasProperty("_Smoothness"))
                {
                    mat.SetFloat("_Smoothness", 0.05f);
                }

                if (mat.HasProperty("_Glossiness"))
                {
                    mat.SetFloat("_Glossiness", 0.05f);
                }

                // Evitar blancos quemados en materiales demasiado claros.
                if (mat.HasProperty("_BaseColor"))
                {
                    Color c = mat.GetColor("_BaseColor");
                    float max = Mathf.Max(c.r, c.g, c.b);

                    if (max > 0.85f)
                    {
                        c.r *= 0.65f;
                        c.g *= 0.65f;
                        c.b *= 0.65f;
                        mat.SetColor("_BaseColor", c);
                    }
                }

                if (mat.HasProperty("_Color"))
                {
                    Color c = mat.GetColor("_Color");
                    float max = Mathf.Max(c.r, c.g, c.b);

                    if (max > 0.85f)
                    {
                        c.r *= 0.65f;
                        c.g *= 0.65f;
                        c.b *= 0.65f;
                        mat.SetColor("_Color", c);
                    }
                }

                EditorUtility.SetDirty(mat);
                fixedMaterials++;
            }
        }

        AssetDatabase.SaveAssets();

        Debug.Log($"Materiales corregidos en {root.name}: {fixedMaterials}");
    }
}