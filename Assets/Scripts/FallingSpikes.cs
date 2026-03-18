using Unity.VisualScripting;
using UnityEngine;

public class FallingSpikes : MonoBehaviour
{
    Rigidbody2D rb;
    BoxCollider2D bc2D;

    [SerializeField] public float distance;
    bool isFalling = false;
    [field: SerializeField] public GameObject EffectOnDestroyPrefab {  get; private set; }
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        bc2D = GetComponent<BoxCollider2D>();
    }
    void Update()
    {
        Fall();
    }

    public void Fall()
    {
        Physics2D.queriesStartInColliders = false;

        if (isFalling == false)
        {
            RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, distance);

            Debug.DrawRay(transform.position, Vector2.down * distance, Color.red);

            if (hit.transform != null)
            {
                if (hit.transform.tag == "Player")
                {
                    rb.gravityScale = 5;
                    isFalling = true;
                }
            }
        }
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Ground")
        {
            if (EffectOnDestroyPrefab)
            {
                Instantiate(EffectOnDestroyPrefab, transform.position, Quaternion.identity);
            }
            Destroy(this.gameObject);

        }
    }
}
