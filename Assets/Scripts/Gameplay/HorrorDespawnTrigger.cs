using UnityEngine;

public class HorrorDespawnTrigger : MonoBehaviour
{
    public HorrorEntityChase entity;

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player"))
            return;

        if (entity != null)
            entity.StopChase();
    }
}