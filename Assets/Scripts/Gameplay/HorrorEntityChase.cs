using System.Collections;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class HorrorEntityChase : MonoBehaviour
{
    [Header("Target")]
    public Transform target;

    [Header("Movement")]
    public float moveSpeed = 1.3f;
    public float stopDistance = 1.3f;
    public float rotationSpeed = 6f;
    public float maxChaseTime = 35f;

    [Header("Gravity")]
    public float gravity = -18f;
    public float groundedStickForce = -2f;
    public bool chaseWhileFalling = false;

    [Header("Life / Horror Feel")]
    public bool lookAtPlayer = true;
    public float idleSwayAmount = 3f;
    public float idleSwaySpeed = 3f;

    [Header("Animation")]
    public Animator animator;
    public string chaseBoolParameter = "";

    private CharacterController controller;
    private bool isChasing;
    private float chaseTimer;
    private float verticalVelocity;
    private Quaternion baseRotation;

    private void Awake()
    {
        controller = GetComponent<CharacterController>();
        baseRotation = transform.rotation;
    }

    private void Update()
    {
        ApplyGravity();

        if (!isChasing || target == null)
        {
            ApplyIdleSway();
            return;
        }

        chaseTimer += Time.deltaTime;

        if (chaseTimer >= maxChaseTime)
        {
            StopChase();
            return;
        }

        Vector3 toTarget = target.position - transform.position;
        toTarget.y = 0f;

        float distance = toTarget.magnitude;

        if (lookAtPlayer && toTarget.sqrMagnitude > 0.01f)
        {
            Quaternion targetRotation = Quaternion.LookRotation(toTarget.normalized);
            transform.rotation = Quaternion.Slerp(
                transform.rotation,
                targetRotation,
                rotationSpeed * Time.deltaTime
            );
        }

        Vector3 horizontalMove = Vector3.zero;

        bool canChase = controller.isGrounded || chaseWhileFalling;

        if (canChase && distance > stopDistance)
        {
            horizontalMove = toTarget.normalized * moveSpeed;
        }

        Vector3 finalMove = horizontalMove;
        finalMove.y = verticalVelocity;

        controller.Move(finalMove * Time.deltaTime);
    }

    private void ApplyGravity()
    {
        if (controller.isGrounded && verticalVelocity < 0)
        {
            verticalVelocity = groundedStickForce;
        }
        else
        {
            verticalVelocity += gravity * Time.deltaTime;
        }
    }

    private void ApplyIdleSway()
    {
        if (idleSwayAmount <= 0) return;

        float sway = Mathf.Sin(Time.time * idleSwaySpeed) * idleSwayAmount;
        transform.rotation = baseRotation * Quaternion.Euler(0f, sway, 0f);
    }

    public void StartChase()
    {
        gameObject.SetActive(true);

        isChasing = true;
        chaseTimer = 0f;
        verticalVelocity = 0f;
        baseRotation = transform.rotation;

        if (animator != null && !string.IsNullOrEmpty(chaseBoolParameter))
            animator.SetBool(chaseBoolParameter, true);
    }

    public void StopChase()
    {
        isChasing = false;

        if (animator != null && !string.IsNullOrEmpty(chaseBoolParameter))
            animator.SetBool(chaseBoolParameter, false);

        gameObject.SetActive(false);
    }

    public void TeleportTo(Transform point)
    {
        if (point == null) return;

        if (controller == null)
            controller = GetComponent<CharacterController>();

        controller.enabled = false;

        transform.position = point.position;
        transform.rotation = point.rotation;
        baseRotation = transform.rotation;
        verticalVelocity = 0f;

        controller.enabled = true;
    }

    public void ShowForSeconds(Transform point, float duration)
    {
        gameObject.SetActive(true);

        if (point != null)
            TeleportTo(point);

        StartCoroutine(HideAfterSeconds(duration));
    }

    private IEnumerator HideAfterSeconds(float duration)
    {
        yield return new WaitForSeconds(duration);
        gameObject.SetActive(false);
    }
}