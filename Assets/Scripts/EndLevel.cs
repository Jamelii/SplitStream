using UnityEngine;
using UnityEngine.SceneManagement;

public class EndLevel : MonoBehaviour
{
    BoxCollider2D bc2D;
    private Animator anim;

    [Header("Level Transition Timer")]
    [SerializeField] public float delayTime = 1.0f; // Amount to Delay before swapping to next level.

    void Start()
    {
        bc2D = GetComponent<BoxCollider2D>();
        anim = GetComponent<Animator>();
    }

    void Update()
    {

    }
    // When the player touches the trigger box, change to end game scene after a set time.
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            anim.Play("Open");
            Invoke("DelayedTransition", delayTime);
        }
    }

    // Change to end game scene.
    public void DelayedTransition()
    {
        SceneManager.LoadScene("EndGame");
    }
}
