using UnityEngine;

public class HorrorDespawnTrigger : MonoBehaviour
{
    public HorrorEntityChase entity;

    [Header("Debug")]
    public bool showDebugLogs = true;

    private void OnTriggerEnter(Collider other)
    {
        TryDespawnEntity(other);
    }

    private void OnTriggerStay(Collider other)
    {
        TryDespawnEntity(other);
    }

    private void TryDespawnEntity(Collider other)
    {
        if (entity == null)
        {
            if (showDebugLogs)
                Debug.LogWarning("HorrorDespawnTrigger: No hay entidad asignada.");

            return;
        }

        if (!IsEntityOrEntityChild(other))
            return;

        if (showDebugLogs)
            Debug.Log("HorrorDespawnTrigger: la entidad cruzó el trigger. Desapareciendo.");

        entity.StopChase();
    }

    private bool IsEntityOrEntityChild(Collider other)
    {
        if (other == null)
            return false;

        Transform current = other.transform;

        while (current != null)
        {
            if (current == entity.transform)
                return true;

            current = current.parent;
        }

        return false;
    }
}