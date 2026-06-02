using UnityEngine;

public class CarRepairStation : MonoBehaviour
{
    public RepairManager repairManager;

    [Header("Audio")]
    public AudioClip engineFailClip;
    public AudioClip engineStartClip;
    public float volume = 0.8f;

    [Header("Final Lights")]
    public GameObject[] finalLights;

    private bool repaired = false;

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;
        if (repairManager == null) return;

        if (!repairManager.HasAllParts())
        {
            if (engineFailClip != null)
                AudioSource.PlayClipAtPoint(engineFailClip, transform.position, volume);

            repairManager.RepairCar();
            return;
        }

        if (repaired) return;

        repaired = true;

        repairManager.RepairCar();

        if (engineStartClip != null)
            AudioSource.PlayClipAtPoint(engineStartClip, transform.position, volume);

        foreach (GameObject lightObj in finalLights)
        {
            if (lightObj != null)
                lightObj.SetActive(true);
        }
    }
}