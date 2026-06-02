using TMPro;
using UnityEngine;

public class HorrorSequenceManager : MonoBehaviour
{
    [Header("UI")]
    public TMP_Text objectiveText;

    [Header("Entity")]
    public HorrorEntityChase horrorEntity;
    public Transform secondPieceSpawnPoint;
    public Transform thirdPieceScarePoint;

    [Header("Audio")]
    public AudioSource globalAudioSource;
    public AudioClip secondPieceHorrorClip;
    public AudioClip thirdPieceTensionClip;
    public float volume = 0.8f;

    private bool secondEventTriggered;
    private bool thirdEventTriggered;

    public void OnPartsCollected(int collectedParts)
    {
        if (collectedParts == 2 && !secondEventTriggered)
        {
            TriggerSecondPieceEvent();
        }

        if (collectedParts == 3 && !thirdEventTriggered)
        {
            TriggerThirdPieceEvent();
        }
    }

    private void TriggerSecondPieceEvent()
    {
        secondEventTriggered = true;

        if (horrorEntity != null)
        {
            if (secondPieceSpawnPoint != null)
                horrorEntity.TeleportTo(secondPieceSpawnPoint);

            horrorEntity.StartChase();
        }

        PlayClip(secondPieceHorrorClip);

        if (objectiveText != null)
            objectiveText.text = "Objetivo: Algo apareció en el campus... encuentra la última pieza.";
    }

    private void TriggerThirdPieceEvent()
    {
        thirdEventTriggered = true;

        PlayClip(thirdPieceTensionClip);

        if (horrorEntity != null && thirdPieceScarePoint != null)
            horrorEntity.ShowForSeconds(thirdPieceScarePoint, 5f);

        if (objectiveText != null)
            objectiveText.text = "Objetivo: La última pieza hizo demasiado ruido. Regresa al auto.";
    }

    private void PlayClip(AudioClip clip)
    {
        if (clip == null)
            return;

        if (globalAudioSource != null)
            globalAudioSource.PlayOneShot(clip, volume);
        else
            AudioSource.PlayClipAtPoint(clip, transform.position, volume);
    }
}