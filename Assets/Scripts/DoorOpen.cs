using UnityEngine;

public class DoorOpen : MonoBehaviour
{
    BoxCollider2D bc2D;
    private Animation anim;
    void Start()
    {
        bc2D = GetComponent<BoxCollider2D>();
        anim = GetComponent<Animation>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            anim.enabled = true;
        }
    }
}
