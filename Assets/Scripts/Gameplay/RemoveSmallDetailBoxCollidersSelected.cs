using UnityEditor;
using UnityEngine;

public static class RemoveBoxCollidersSelected
{
    [MenuItem("Tools/TecVR/Remove Box Colliders From Selected")]
    public static void RemoveBoxColliders()
    {
        GameObject[] selectedObjects = Selection.gameObjects;

        if (selectedObjects == null || selectedObjects.Length == 0)
        {
            Debug.LogWarning("Selecciona uno o varios objetos primero.");
            return;
        }

        int totalRemoved = 0;

        foreach (GameObject root in selectedObjects)
        {
            BoxCollider[] boxColliders = root.GetComponentsInChildren<BoxCollider>(true);

            foreach (BoxCollider box in boxColliders)
            {
                Undo.DestroyObjectImmediate(box);
                totalRemoved++;
            }

            Debug.Log($"BoxColliders eliminados en {root.name}: {boxColliders.Length}");
        }

        Debug.Log($"Total de BoxColliders eliminados: {totalRemoved}");
    }
}