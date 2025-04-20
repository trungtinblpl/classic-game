using System.Collections.Generic;
using UnityEngine;

public class ProjectilePool : MonoBehaviour
{
    public static ProjectilePool Instance;
    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private int poolSize = 10;

    private Queue<Projectile> projectilePool = new Queue<Projectile>();

    private void Awake()
    {
        Instance = this;
        InitializePool();
    }

    private void InitializePool()
    {
        for (int i = 0; i < poolSize; i++)
        {
            Projectile proj = Instantiate(projectilePrefab).GetComponent<Projectile>();
            proj.gameObject.SetActive(false);
            projectilePool.Enqueue(proj);
        }
    }

    public Projectile GetProjectile()
    {
        if (projectilePool.Count > 0)
        {
            Projectile proj = projectilePool.Dequeue();
            proj.gameObject.SetActive(true);
            return proj;
        }
        else
        {
            return Instantiate(projectilePrefab).GetComponent<Projectile>();
        }
    }

    public void ReturnToPool(Projectile proj)
    {
        proj.gameObject.SetActive(false);
        projectilePool.Enqueue(proj);
    }
}
