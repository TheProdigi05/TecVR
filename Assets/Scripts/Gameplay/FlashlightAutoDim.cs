using UnityEngine;

public class FlashlightAutoDim : MonoBehaviour
{
    [Header("References")]
    public Transform headTarget;
    public Light flashlight;

    [Header("Follow Head")]
    public bool followHead = true;
    public Vector3 localPositionOffset = new Vector3(0f, 0f, 0.05f);
    public Vector3 localRotationOffset = Vector3.zero;

    [Header("Light Intensity")]
    public float normalIntensity = 5f;
    public float closeWallIntensity = 1.2f;
    public float smoothSpeed = 8f;

    [Header("Wall Detection")]
    public float checkDistance = 2.5f;
    public float startDimmingDistance = 1.5f;
    public LayerMask collisionMask = ~0;

    private void Reset()
    {
        flashlight = GetComponentInChildren<Light>();
    }

    private void Awake()
    {
        if (flashlight == null)
            flashlight = GetComponentInChildren<Light>();
    }

    private void LateUpdate()
    {
        if (headTarget == null || flashlight == null)
            return;

        if (followHead)
        {
            transform.position = headTarget.TransformPoint(localPositionOffset);
            transform.rotation = headTarget.rotation * Quaternion.Euler(localRotationOffset);
        }

        float targetIntensity = normalIntensity;

        Ray ray = new Ray(headTarget.position, headTarget.forward);

        if (Physics.Raycast(ray, out RaycastHit hit, checkDistance, collisionMask, QueryTriggerInteraction.Ignore))
        {
            if (hit.distance <= startDimmingDistance)
            {
                float t = Mathf.InverseLerp(startDimmingDistance, 0.2f, hit.distance);
                targetIntensity = Mathf.Lerp(normalIntensity, closeWallIntensity, t);
            }
        }

        flashlight.intensity = Mathf.Lerp(
            flashlight.intensity,
            targetIntensity,
            Time.deltaTime * smoothSpeed
        );
    }
}