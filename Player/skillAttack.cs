using UnityEngine;

public class skillAttack : MonoBehaviour
{
    [SerializeField] private float attackCooldown;
    [SerializeField] private Transform moonPoint;

    private Animator amin;
    private PlayerMove playerMove;
    private float cooldownTimer = Mathf.Infinity;

    private void Awake()
    {
        amin = GetComponent<Animator>();
        playerMove = GetComponent<PlayerMove>();
    }

    private void Update()
    {
        if (Input.GetMouseButton(0) && cooldownTimer > attackCooldown && playerMove.canAttack() && Time.timeScale > 0)
            Attack();

        cooldownTimer += Time.deltaTime;
    }

    private void Attack()
    {
        amin.SetTrigger("SkillAttack");
        cooldownTimer = 0;

        Projectile newProjectile = ProjectilePool.Instance.GetProjectile();
        newProjectile.transform.position = moonPoint.position;
        newProjectile.SetDirection(Mathf.Sign(transform.localScale.x));
    }
}
