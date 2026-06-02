using UnityEngine;

public class RepairPart : MonoBehaviour
{
    public RepairManager repairManager;

    [Header("Audio")]
    public AudioClip pickupClip;
    public float pickupVolume = 0.7f;

    private bool collected = false;

    private void OnTriggerEnter(Collider other)
    {
        if (collected) return;

        if (other.CompareTag("Player"))
        {
            collected = true;

            if (repairManager != null)
                repairManager.CollectPart();

            if (pickupClip != null)
                AudioSource.PlayClipAtPoint(pickupClip, transform.position, pickupVolume);

            gameObject.SetActive(false);
        }
    }
}