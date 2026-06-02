using System.Collections;
using UnityEngine;

public class HorrorEntityChase : MonoBehaviour
{
    [Header("Target")]
    public Transform target;

    [Header("Movement")]
    public float moveSpeed = 1.4f;
    public float stopDistance = 1.2f;
    public float maxChaseTime = 35f;
    public bool keepSameHeight = true;

    [Header("Animation")]
    public Animator animator;
    public string chaseBoolParameter = "";

    private bool isChasing;
    private float chaseTimer;

    private void Update()
    {
        if (!isChasing || target == null)
            return;

        chaseTimer += Time.deltaTime;

        if (chaseTimer >= maxChaseTime)
        {
            StopChase();
            return;
        }

        Vector3 targetPosition = target.position;

        if (keepSameHeight)
            targetPosition.y = transform.position.y;

        Vector3 direction = targetPosition - transform.position;

        if (direction.sqrMagnitude > 0.01f)
        {
            Quaternion lookRotation = Quaternion.LookRotation(direction.normalized);
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 4f);
        }

        float distance = Vector3.Distance(transform.position, targetPosition);

        if (distance > stopDistance)
        {
            transform.position = Vector3.MoveTowards(
                transform.position,
                targetPosition,
                moveSpeed * Time.deltaTime
            );
        }
    }

    public void StartChase()
    {
        gameObject.SetActive(true);
        isChasing = true;
        chaseTimer = 0f;

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

        transform.position = point.position;
        transform.rotation = point.rotation;
    }

    public void ShowForSeconds(Transform point, float duration)
    {
        gameObject.SetActive(true);

        if (point != null)
        {
            transform.position = point.position;
            transform.rotation = point.rotation;
        }

        StartCoroutine(HideAfterSeconds(duration));
    }

    private IEnumerator HideAfterSeconds(float duration)
    {
        yield return new WaitForSeconds(duration);
        gameObject.SetActive(false);
    }
}