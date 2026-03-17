 using UnityEngine;

public class TimeSwap : MonoBehaviour
{
    GameObject PastGround;
    bool isPresent = true;
    void Start()
    {
        
    }

    void Update()
    {
        Swap();
    }

    public void Swap()
    {
        if (Input.GetKey(KeyCode.LeftShift) && isPresent == true)
        {
            this.gameObject.SetActive(false);
            isPresent = false;
        }
        else if (Input.GetKey(KeyCode.LeftShift) && isPresent == false){
            this.gameObject.SetActive(true);
            isPresent = true;
        }
    }
}
