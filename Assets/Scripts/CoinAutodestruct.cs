using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinAutodestruct : MonoBehaviour
{
    public int points;

    // [SerializeField] private float startDelay = 3f;
    
    
    void Start()
    {
        // InvokeRepeating("Destroy", startDelay, 0);
        Destroy(gameObject, 3);

       

    }

    private void Awake()
    {
        if (gameObject.CompareTag("Good Coin"))
        {
            points = 10;
            
        }
        if (gameObject.CompareTag("Silver Coin"))
        {
            
            points = 5;
            
        }
        if (gameObject.CompareTag("Bronze Coin"))
        {
            
            points = 1;
            
        }
    }
    /* 
     private void Destroy()
    {

        Destroy(gameObject);
    }
    */
}
