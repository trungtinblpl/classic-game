using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private float speed;
    private float direction;
    private bool hit;
    private float lifetime;

    private Animator amin;
    private BoxCollider2D boxCollider;

    private void Awake(){
        amin = GetComponent<Animator>();
        boxCollider = GetComponent<BoxCollider2D>();
    }

    private void Update(){
        if(hit) return;
        float moveSpeed = speed * Time.deltaTime * direction;
        transform.Translate(moveSpeed, 0, 0);

        lifetime += Time.deltaTime;
        if(lifetime > 5) gameObject.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision){
        hit = true;
        boxCollider.enabled = false;
        amin.SetTrigger("mooLight_Explode");

        if(collision.tag == "Monter")
            collision.GetComponent<Health>()?.TakeDamage(1);
    }

    public void SetDirection(float _direction){
        direction = _direction;
        gameObject.SetActive(true);
        hit = false;
        boxCollider.enabled = true;

        float localScaleX = transform.localScale.x;
        if(Mathf.Sign(localScaleX) != _direction)
            localScaleX = -localScaleX;

        transform.localScale = new Vector3(localScaleX, transform.localScale.y, transform.localScale.z);
    }

    //vo hiu hoa
    private void Deactive(){
        gameObject.SetActive(false);
    }
}

