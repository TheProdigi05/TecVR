using UnityEngine;

public class CarRepairStation : MonoBehaviour
{
    public RepairManager repairManager;
    public AudioSource repairSound;

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;

        if (repairManager != null)
        {
            repairManager.RepairCar();

            if (repairManager.HasAllParts() && repairSound != null)
                repairSound.Play();
        }
    }
}