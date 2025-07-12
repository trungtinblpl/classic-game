using System.Collections;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public static PlayerStats Instance;

    private Health health;

    [Header("Chỉ số gốc")]
    public float damage;
    public float critChance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            // Debug.Log("[PlayerStats] Instance gán thành công");
        }
        else
        {
            // Debug.LogWarning("[PlayerStats] Đã có Instance khác tồn tại");
            Destroy(gameObject);
        }

        health = GetComponent<Health>();

    }

    public (float damage, bool isCrit) CaculateDame()
    {
        bool isCrit = Random.value < critChance / 100f;
        float finalDamage = isCrit ? damage * 2 : damage;
        return (finalDamage, isCrit);
    }

    public void AttackEnemy(Health enemyHealth)
    {
        var (damageToDeal, _) = CaculateDame();
        enemyHealth.TakeDamage(damageToDeal);
    }

    public void Heal(float _value)
    {
        health.AddHealth(_value);
    }

    public void TakeDamage(float _value)
    {
        health.TakeDamage(_value);
    }

    public void BuffDamage(float _value)
    {
        damage += _value;
        // Debug.Log($"Damage buffed to: {damage}");
    }

    public void BuffCritChance(float _value)
    {
        critChance += _value;
        // Debug.Log($"Crit chance buffed to: {critChance}");
    }

    public void TemporaryBuff(System.Action<float> buffMethod, float value, float duration)
    {
        buffMethod(value);
        // Debug.Log($"<color=cyan>Temporary buff started: +{value} for {duration}s at {Time.time}</color>");
        StartCoroutine(RemoveBuffAfter(buffMethod, -value, duration));
    }

    private IEnumerator RemoveBuffAfter(System.Action<float> buffMethod, float value, float duration)
    {
        yield return new WaitForSeconds(duration);
        buffMethod(value);
        // Debug.Log($"<color=gray>Buff removed: {value} at {Time.time}</color>");
    }
}
