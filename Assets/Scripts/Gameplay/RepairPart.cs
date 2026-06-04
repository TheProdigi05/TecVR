using UnityEngine;

public class RepairPart : MonoBehaviour
{
    public RepairManager repairManager;

    [Header("Audio")]
    public AudioClip pickupClip;
    public float pickupVolume = 0.7f;

    [Header("Pickup Settings")]
    public bool canBePickedByPlayerBody = true;
    public bool canBePickedByHands = true;

    private bool collected = false;

    private void OnTriggerEnter(Collider other)
    {
        TryCollect(other);
    }

    private void OnTriggerStay(Collider other)
    {
        // Ayuda por si el mando entra muy rápido o el trigger no se detecta al primer frame.
        TryCollect(other);
    }

    private void TryCollect(Collider other)
    {
        if (collected) return;

        if (!IsValidCollector(other)) return;

        collected = true;

        if (repairManager != null)
            repairManager.CollectPart();

        if (pickupClip != null)
            AudioSource.PlayClipAtPoint(pickupClip, transform.position, pickupVolume);

        gameObject.SetActive(false);
    }

    private bool IsValidCollector(Collider other)
    {
        if (other == null) return false;

        if (canBePickedByPlayerBody && other.CompareTag("Player"))
            return true;

        if (canBePickedByHands && other.CompareTag("PlayerHand"))
            return true;

        // Por si el collider está en un hijo del PlayerController o del mando.
        Transform current = other.transform;

        while (current != null)
        {
            if (canBePickedByPlayerBody && current.CompareTag("Player"))
                return true;

            if (canBePickedByHands && current.CompareTag("PlayerHand"))
                return true;

            current = current.parent;
        }

        return false;
    }
}