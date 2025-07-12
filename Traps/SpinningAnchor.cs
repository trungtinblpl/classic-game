using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpinningAnchor : TrapDame
{
    [SerializeField] public float maxAngle = 30f;

    // Tốc độ dao động (đơn vị: vòng/s)
    public float speed = 1f;

    private float time;

    void Update()
    {
        time += Time.deltaTime;

        float signY = Mathf.Sign(transform.localScale.y);
        float angle = maxAngle * Mathf.Sin(time * speed * 2 * Mathf.PI);

        // Áp dụng xoay quanh trục Z (2D)
        transform.localRotation = Quaternion.Euler(0f, 0f, angle * signY);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            base.OnTriggerEnter2D(collision);
        }
    }
}
