
using UnityEngine;
using UnityEngine.UI;

public class Healthbar : MonoBehaviour
{
    [SerializeField] private Health targetHealth;
    [SerializeField] private Image totalhealthBar;
    [SerializeField] private Image currenthealthBar;

    private void Start(){
        totalhealthBar.fillAmount = 1f;
    }

    private void Update(){
        currenthealthBar.fillAmount = targetHealth.currentHealth / targetHealth.maxHealth;
    }
}
