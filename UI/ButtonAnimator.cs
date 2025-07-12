using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonAnimator : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    private Animator animator;
    private AudioManager audioManager;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        animator.SetTrigger("Press");
        audioManager.PlaySFX(audioManager.buttonClickClip);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        animator.SetTrigger("Release");
        audioManager.PlaySFX(audioManager.buttonClickClip);
    }
}
