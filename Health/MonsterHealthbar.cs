using UnityEngine;
using UnityEngine.UI;

public class MonsterHealthbar : MonoBehaviour
{
    [SerializeField] private Health targetHealth;
    [SerializeField] private Image totalhealthBar;
    [SerializeField] private Image currenthealthBar;
    [SerializeField] private Vector3 offset;

    // Update is called once per frame
    private void Update()
    {
        if(targetHealth != null){
            currenthealthBar.fillAmount = (float)targetHealth.currentHealth / targetHealth.maxHealth;
            transform.position = Camera.main.WorldToScreenPoint(targetHealth.transform.position + offset);
        }
    }

    public void SetTarget(Health healthTaget){
        targetHealth = healthTaget;
    }
}
