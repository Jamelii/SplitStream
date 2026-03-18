using UnityEngine;

public class PastToPresent : MonoBehaviour
{
    bool isPresent = false;  
    void Start()
    {
        
    }

    
    void Update()
    {
        
    }

    public void Swap()
    {
        if(Input.GetKey(KeyCode.LeftShift) && isPresent == false)
        {
            this.gameObject.SetActive(false);
            isPresent = true;
        }
        else if(Input.GetKey(KeyCode.RightShift) && isPresent == true)
        {
            this.gameObject.SetActive(true);
            isPresent = false;
        }
    }
}
