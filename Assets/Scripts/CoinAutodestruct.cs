using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinAutodestruct : MonoBehaviour
{

    [SerializeField] private float startDelay = 3f;
    
    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("Destroy", startDelay, 0);
    }

    // Update is called once per frame
    private void Destroy()
    {

        Destroy(gameObject);
    }
}
