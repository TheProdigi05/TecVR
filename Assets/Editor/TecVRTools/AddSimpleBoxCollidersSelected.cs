using UnityEditor;
using UnityEngine;

public static class AddSimpleBoxCollidersSelected
{
    [MenuItem("Tools/TecVR/Add Simple Box Colliders To Selected")]
    public static void AddBoxColliders()
    {
        GameObject root = Selection.activeGameObject;

        if (root == null)
        {
            Debug.LogWarning("Selecciona el objeto raíz del edificio primero.");
            return;
        }

        MeshFilter[] meshFilters = root.GetComponentsInChildren<MeshFilter>(true);
        int added = 0;
        int skipped = 0;

        foreach (MeshFilter mf in meshFilters)
        {
            if (mf == null || mf.sharedMesh == null)
                continue;

            GameObject obj = mf.gameObject;

            // Evitar meter collider a cosas que probablemente no deberían bloquear.
            string n = obj.name.ToLower();

            if (n.Contains("glass") || n.Contains("vidrio") || n.Contains("window") || n.Contains("ventana") ||
                n.Contains("light") || n.Contains("lamp") || n.Contains("texto") || n.Contains("sign") ||
                n.Contains("cartel"))
            {
                skipped++;
                continue;
            }

            // Si ya tiene collider, no duplicar.
            Collider existing = obj.GetComponent<Collider>();
            if (existing != null)
            {
                skipped++;
                continue;
            }

            Undo.AddComponent<BoxCollider>(obj);
            BoxCollider box = obj.GetComponent<BoxCollider>();

            Bounds bounds = mf.sharedMesh.bounds;
            box.center = bounds.center;
            box.size = bounds.size;
            box.isTrigger = false;

            added++;
        }

        Debug.Log($"BoxColliders agregados en {root.name}: {added}. Saltados: {skipped}");
    }
}