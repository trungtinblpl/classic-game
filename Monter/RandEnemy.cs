
using UnityEngine;

public class RandEnemy : MonoBehaviour
{
    [Header ("Attack Parameters")]
    [SerializeField] private float attackCooldown;
    [SerializeField] private float range;
    [SerializeField] private float damage;

    [Header ("Ranged Attack")]
    [SerializeField] private Transform bonePoint;
    [SerializeField] private GameObject[] bones;

    [Header ("Collider Parameters")]
    [SerializeField] private float colliderDistance;
    [SerializeField] private BoxCollider2D boxCollider;

    [Header ("Player Layer")]
    [SerializeField] private LayerMask playerLayer;
    private float cooldownTimer = Mathf.Infinity;

    private Animator amin;
    private MonterPatrol monterPatrol;

    private void Awake(){
        amin = GetComponent<Animator>();
        monterPatrol = GetComponentInParent<MonterPatrol>();
    }

    private void Update(){
        cooldownTimer += Time.deltaTime;

         //attack only when player in sight
        if(PlayerInsight()){
            if(cooldownTimer >= attackCooldown){
                //attack
                cooldownTimer = 0;
                amin.SetTrigger("rangAttack");
            }
        }

        if(monterPatrol != null){
            monterPatrol.enabled = !PlayerInsight();
        }
            
    }

    private void RangedAttack(){
        cooldownTimer = 0;
        //shoot
        bones[FindBone()].transform.position = bonePoint.position;
        bones[FindBone()].GetComponent<EnemyProjectile>().ActivateProjectile();
    }

    private int FindBone(){
        for(int i = 0; i < bones.Length; i++){
            if(!bones[i].activeInHierarchy){
                return i;
            }
        }
        return 0;
    }

    private bool PlayerInsight(){
        RaycastHit2D hit = Physics2D.BoxCast(
            boxCollider.bounds.center + transform.right * 
            range * transform.localScale.x * colliderDistance, 
        new Vector3(boxCollider.bounds.size.x 
        * range, boxCollider.bounds.size.y, boxCollider.bounds.size.z), 
        0, Vector2.left, 0, playerLayer);


        return hit.collider != null;
    }

    private void OnDrawGizmos(){
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(boxCollider.bounds.center + transform.right 
        * range * transform.localScale.x * colliderDistance, 
        new Vector3(boxCollider.bounds.size.x * range, boxCollider.bounds.size.y, boxCollider.bounds.size.z));
    }
}
