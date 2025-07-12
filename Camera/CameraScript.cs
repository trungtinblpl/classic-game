using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraC : MonoBehaviour
{

    private Transform player;

    public float minX, maxX;
    public float minY, maxY;

    // Start is called before the first frame update
    void Start()
    {
        GameObject playerObj = GameObject.FindWithTag("Player");
        if (playerObj != null)
        {
            player = playerObj.transform;
        }
        // else
        // {
        //     // Debug.LogWarning("Không tìm thấy GameObject có tag 'Player' trong Start(). Sẽ thử lại trong Update().");
        // }
    }

    // Update is called once per frame
    void Update()
    {
        if (player == null)
        {
            GameObject playerObj = GameObject.FindWithTag("Player");
            if (playerObj != null)
            {
                player = playerObj.transform;
                // Debug.Log("Camera đã tìm thấy player.");
            }
        }

        if (player != null)
        {
            Vector3 temp = transform.position;
            temp.x = player.position.x;

            if (temp.x < minX)
                temp.x = minX;
            if (temp.x > maxX)
                temp.x = maxX;

            transform.position = temp;

            Vector3 tmps = transform.position;
            tmps.y = player.position.y;

            if (tmps.y > minY)
                tmps.y = minY;

            transform.position = tmps;
        }
    }
}
