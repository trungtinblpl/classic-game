using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBarFollow : MonoBehaviour
{
    public Transform target;
    public Vector3 offset = new Vector3(0, 1.5f, 0); // chỉnh để nằm trên đầu quái

    void LateUpdate()
    {
        if (target != null)
            transform.position = target.position + offset;
    }
}
