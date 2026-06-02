using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class HorrorRandomSound : MonoBehaviour
{
    [Header("Clips")]
    public AudioClip[] horrorClips;

    [Header("Timing")]
    public float minDelay = 4f;
    public float maxDelay = 9f;

    [Header("Audio")]
    public float volume = 0.7f;
    public bool onlyWhenActive = true;

    private AudioSource audioSource;
    private float timer;
    private float nextDelay;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.playOnAwake = false;
        audioSource.loop = false;

        SetNextDelay();
    }

    private void OnEnable()
    {
        SetNextDelay();
        timer = 0f;
    }

    private void Update()
    {
        if (onlyWhenActive && !gameObject.activeInHierarchy)
            return;

        if (horrorClips == null || horrorClips.Length == 0)
            return;

        timer += Time.deltaTime;

        if (timer >= nextDelay)
        {
            PlayRandomClip();
            timer = 0f;
            SetNextDelay();
        }
    }

    private void PlayRandomClip()
    {
        int index = Random.Range(0, horrorClips.Length);
        AudioClip clip = horrorClips[index];

        if (clip != null)
            audioSource.PlayOneShot(clip, volume);
    }

    private void SetNextDelay()
    {
        nextDelay = Random.Range(minDelay, maxDelay);
    }
}