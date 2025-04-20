using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour
{
    private bool isOnPlatform = false;
    private Transform playerTransform;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            isOnPlatform = true;
            playerTransform = collision.transform;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            isOnPlatform = false;
            playerTransform = null;
        }
    }

    private void Update()
    {
        if (isOnPlatform && playerTransform != null)
        {
            playerTransform.position += transform.position - lastPlatformPosition;
        }
        lastPlatformPosition = transform.position;
    }

    private Vector3 lastPlatformPosition;

    private void Start()
    {
        lastPlatformPosition = transform.position;
    }
}
