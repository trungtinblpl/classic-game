using UnityEngine;
using System.Collections;

public class Spikehead : TrapDame
{
    [Header("SpikeHead Attributes")]
    [SerializeField] private float speed;
    [SerializeField] private float range;
    [SerializeField] private float checkDelay;
    [SerializeField] private LayerMask playerLayer;
    [SerializeField] private float shakeDuration = 0.5f;
    [SerializeField] private float shakeMagnitude = 0.1f;

    private Vector3[] directions = new Vector3[4];
    private Vector3 destination;
    private float checkTimer;
    private bool attacking;
    private bool isShaking;
    private bool hasFallen;

    private Vector3 originalPosition;

    private void OnEnable()
    {
        Stop();
        originalPosition = transform.position;
    }

    private void Update()
    {
        if (attacking)
        {
            transform.Translate(destination * Time.deltaTime * speed);
        }
        else if (!isShaking && !hasFallen)
        {
            checkTimer += Time.deltaTime;
            if (checkTimer > checkDelay)
                CheckForPlayer();
        }
    }

    private void CheckForPlayer()
    {
        CalculateDirections();

        for (int i = 0; i < directions.Length; i++)
        {
            Debug.DrawRay(transform.position, directions[i], Color.red);
            RaycastHit2D hit = Physics2D.Raycast(transform.position, directions[i], range, playerLayer);

            if (hit.collider != null)
            {
                checkTimer = 0;
                StartCoroutine(ShakeBeforeAttack(directions[i]));
                break;
            }
        }
    }

    private IEnumerator ShakeBeforeAttack(Vector3 attackDirection)
    {
        isShaking = true;
        originalPosition = transform.position;
        float elapsed = 0f;

        while (elapsed < shakeDuration)
        {
            float offsetX = Random.Range(-1f, 1f) * shakeMagnitude;
            float offsetY = Random.Range(-1f, 1f) * shakeMagnitude;
            transform.position = originalPosition + new Vector3(offsetX, offsetY, 0f);

            elapsed += Time.deltaTime;
            yield return null;
        }

        transform.position = originalPosition;
        isShaking = false;

        // Bắt đầu tấn công
        attacking = true;
        destination = attackDirection.normalized;
    }

    private void CalculateDirections()
    {
        directions[3] = -transform.up;       // Down
    }

    private void Stop()
    {
        destination = Vector3.zero;
        attacking = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Player") && attacking)
        {
            base.OnTriggerEnter2D(collision);

        }

        if (!hasFallen && collision.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
            Stop();
            hasFallen = true; // Không cho rơi lại
            GetComponent<Collider2D>().enabled = false;
        }
    }
}
