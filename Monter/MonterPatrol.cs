
using UnityEngine;

public class MonterPatrol : MonoBehaviour
{
    [Header("Patrol Point")]
    [SerializeField] private Transform leftEdge;
    [SerializeField] private Transform rightEdge;

    [Header("Monter")]
    [SerializeField] private Transform monter;

    [Header("Movement parameters")]
    [SerializeField] private float speed;
    private Vector3 initScale;
    private bool movingLeft;

    [Header("Idle Behaviour")]
    [SerializeField] private float idleDuration;
    private float idleTime;

    [Header("Monter Animation")]
    [SerializeField] private Animator amin;

    private void Awake()
    {
        initScale = monter.localScale;
    }

    private void OnDisable()
    {
        amin.SetBool("moving", false);
    }

    private void Update()
    {

        if (movingLeft)
        {
            if (monter.position.x >= leftEdge.position.x)
            {
                MoveInDirection(-1);
            }
            else
            {
                DirectionChange();
            }
        }
        else
        {
            if (monter.position.x <= rightEdge.position.x)
            {
                MoveInDirection(1);
            }
            else
            {
                DirectionChange();
            }
        }
    }


    private void DirectionChange()
    {
        amin.SetBool("moving", false);
        idleTime += Time.deltaTime;

        if (idleTime > idleDuration)
        {
            movingLeft = !movingLeft;
        }
    }

    private void MoveInDirection(int _direction)
    {
        idleTime = 0;
        amin.SetBool("moving", true);

        //make monter face direction
        monter.localScale = new Vector3(Mathf.Abs(initScale.x) * _direction,
        initScale.y, initScale.z);

        //move in that directon
        monter.position = new Vector3(monter.position.x + Time.deltaTime * _direction * speed,
        monter.position.y, monter.position.z);
    }
}

