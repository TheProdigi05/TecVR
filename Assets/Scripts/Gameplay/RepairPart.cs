using UnityEngine;

public class RepairPart : MonoBehaviour
{
    public RepairManager repairManager;
    public AudioSource pickupSound;

    private bool collected = false;

    private void OnTriggerEnter(Collider other)
    {
        if (collected) return;

        if (other.CompareTag("Player"))
        {
            collected = true;

            if (repairManager != null)
                repairManager.CollectPart();

            if (pickupSound != null)
                pickupSound.Play();

            gameObject.SetActive(false);
        }
    }
}