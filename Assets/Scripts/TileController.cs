using UnityEngine;
using UnityEngine.Tilemaps;

public class TileController : MonoBehaviour
{
    bool isPresent = true;

    [Header ("Objects")]
    [SerializeField] GameObject PresentGround;
    [SerializeField] GameObject PresentWalls;
    [SerializeField] GameObject PastGround;
    [SerializeField] GameObject PastWalls;
    
    void Start()
    {
        // PresentGround.SetActive(true);
        // PresentWalls.SetActive(true);
        RenderPresent();
        UnrenderPast();
    }

    void Update()
    {
        Swap();
    }

    public void Swap()
    {
        if (isPresent == true) 
        {
            // PresentGround.SetActive(true);
            // PresentWalls.SetActive(true);
            RenderPresent();
        } 
        if (Input.GetKeyDown(KeyCode.LeftShift) && isPresent == true)
        {
            UnrenderPresent();
            RenderPast();
            isPresent = false;
            Debug.Log(isPresent);
        }
        else if (Input.GetKeyDown(KeyCode.LeftShift) && !isPresent == true)
        {
            RenderPresent();
            UnrenderPast();
            isPresent = true;
            Debug.Log(isPresent);
        }                
    }

    public void RenderPresent()
    {
        PresentGround.GetComponent<TilemapRenderer>().enabled = true;
        PresentGround.GetComponent<TilemapCollider2D>().enabled = true;
        PresentWalls.GetComponent<TilemapRenderer>().enabled = true;
        PresentWalls.GetComponent<TilemapCollider2D>().enabled = true;
    }

    public void RenderPast()
    {
        PastGround.GetComponent<TilemapRenderer>().enabled = true;
        PastGround.GetComponent<TilemapCollider2D>().enabled = true;
        PastWalls.GetComponent<TilemapRenderer>().enabled = true;
        PastWalls.GetComponent<TilemapCollider2D>().enabled = true;
    }

    public void UnrenderPresent()
    {
        PresentGround.GetComponent<TilemapRenderer>().enabled = false;
        PresentGround.GetComponent<TilemapCollider2D>().enabled = false;
        PresentWalls.GetComponent<TilemapRenderer>().enabled = false;
        PresentWalls.GetComponent<TilemapCollider2D>().enabled = false;
    }

    public void UnrenderPast()
    {
        PastGround.GetComponent<TilemapRenderer>().enabled = false;
        PastGround.GetComponent<TilemapCollider2D>().enabled = false;
        PastWalls.GetComponent<TilemapRenderer>().enabled = false;
        PastWalls.GetComponent<TilemapCollider2D>().enabled = false;
    }
}
