using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class FootstepAudio : MonoBehaviour
{
    [Header("References")]
    public Transform playerRoot;

    [Header("Footstep Audio")]
    public AudioClip[] footstepClips;
    public float stepInterval = 0.55f;
    public float minMoveDistance = 0.02f;
    public float volume = 0.35f;

    private AudioSource audioSource;
    private Vector3 lastPosition;
    private float stepTimer;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();

        if (playerRoot == null)
            playerRoot = transform;

        lastPosition = playerRoot.position;
    }

    private void Update()
    {
        Vector3 currentPosition = playerRoot.position;
        float distanceMoved = Vector3.Distance(currentPosition, lastPosition);

        bool isMoving = distanceMoved > minMoveDistance;

        if (isMoving)
        {
            stepTimer += Time.deltaTime;

            if (stepTimer >= stepInterval)
            {
                PlayFootstep();
                stepTimer = 0f;
            }
        }
        else
        {
            stepTimer = stepInterval;
        }

        lastPosition = currentPosition;
    }

    private void PlayFootstep()
    {
        if (footstepClips == null || footstepClips.Length == 0)
            return;

        int randomIndex = Random.Range(0, footstepClips.Length);
        AudioClip clip = footstepClips[randomIndex];

        if (clip != null)
            audioSource.PlayOneShot(clip, volume);
    }
}