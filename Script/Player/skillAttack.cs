using UnityEngine;

public class skillAttack : MonoBehaviour
{
    [SerializeField] private float attackCooldown;
    [SerializeField] private Transform moonPoint;
    [SerializeField] private GameObject[] moonlights;

    private Animator amin;
    private PlayerMove playerMove;
    private float cooldownTimer = Mathf.Infinity;

    private void Awake()
    {
        amin = GetComponent<Animator>();
        playerMove = GetComponent<PlayerMove>();
    }

    // Update is called once per frame
    private void Update()
    {
        if(Input.GetMouseButton(0) && cooldownTimer > attackCooldown && playerMove.canAttack() && Time.timeScale > 0)
            Attack();
    
        cooldownTimer += Time.deltaTime;

        // Debug.Log("Update called");
    }

    private void Attack(){
        amin.SetTrigger("SkillAttack");
        cooldownTimer = 0;

        moonlights[FindMoonLight()].transform.position = moonPoint.position;
        moonlights[FindMoonLight()].GetComponent<Projectile>().SetDirection(Mathf.Sign(transform.localScale.x));
    }

    private int FindMoonLight(){
        for(int i = 0; i < moonlights.Length; i++){
            if(!moonlights[i].activeInHierarchy)
                return i;
        }
        return 0;
    }
}
