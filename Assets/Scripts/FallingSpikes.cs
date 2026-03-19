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
        Physics2D.queriesStartInColliders = false; // Stops the Raycast detecting its own object's collision box.

        if (isFalling == false)
        {
            RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, distance); // Cast a 2DRaycast downwards and multiply it by distance.

            Debug.DrawRay(transform.position, Vector2.down * distance, Color.red); // Allows to see 2DRaycast during playtime in editor.

            if (hit.transform != null) // Checks if 2DRaycast has seen anything.
            {
                if (hit.transform.tag == "Player") // Checks if what the 2DRaycast saw had the "Player" tag.
                {
                    rb.gravityScale = 5;
                    isFalling = true;
                }
            }
        }
    }
    // When the spike touches anything with a "Ground" Tag, destroy the object and play a particle effect for 0.5 seconds.
    public void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Ground")
        {
            if (EffectOnDestroyPrefab) 
            {
                Instantiate(EffectOnDestroyPrefab, new Vector2(transform.position.x, transform.position.y - 0.5f), Quaternion.identity); // Spawns the particle effect.
            }
            Destroy(this.gameObject);

        }
    }
}
