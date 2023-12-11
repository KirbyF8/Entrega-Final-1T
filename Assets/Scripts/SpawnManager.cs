using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] private GameObject[] CoinsArray;
    private int coinIndex;
    // private int lastCoin = 2;
    private int CoinLimiter;
    private int CoinUnlimitier;
    public int BadCoinCount;

    [SerializeField] private GameObject BadCoin;

    private float cameraLimitx = 9f;
    private float cameraLimity = 6f;
    private float CoinPlacementX;
    private float CoinPlacementY;
    

    [SerializeField] private float startDelay = 2f;
    private float spawnInterval = 2.4f;
    private int SpeedUp = 10;

    private Player_S Player;
   
    void Start()
    {
        InvokeRepeating("SpawnCoin", startDelay, spawnInterval);
        InvokeRepeating("SpawnBadCoin", startDelay + 1f, spawnInterval);
        if (SpeedUp < 0)
        {
            Debug.Log("Más Rápido !!!");
            InvokeRepeating("SpawnCoin", startDelay, spawnInterval + 2f);
            InvokeRepeating("SpawnBadCoin", startDelay + 2f, spawnInterval + 1f);
        }
        Player = FindObjectOfType<Player_S>();
    }

    private void Update()
    {
        if (Player.Check)
        {
            CancelInvoke();
        }
    }

    private void SpawnCoin()
    {
        CoinPlacementX = Random.Range(-cameraLimitx, cameraLimitx);
        CoinPlacementY = Random.Range(-cameraLimity, cameraLimity);

        CoinLimiter = (CoinsArray.Length - 1);
        CoinLimiter = CoinLimiter - CoinUnlimitier;

        coinIndex = Random.Range(0, CoinsArray.Length - CoinLimiter);
        Instantiate(CoinsArray[coinIndex], new Vector3(CoinPlacementX, 0, CoinPlacementY), Quaternion.Euler(0, 0, 0));
        // lastCoin = coinIndex;
        if (BadCoinCount > 5)
        {
            if (CoinUnlimitier <= CoinLimiter)
            {
                CoinUnlimitier++;
            }
        }
        

    }

    [SerializeField] private float radio;
    
    private void SpawnBadCoin()
    {
        CoinPlacementX = Random.Range(-cameraLimitx, cameraLimitx);
        CoinPlacementY = Random.Range(-cameraLimity, cameraLimity);
        while ((CoinPlacementY * CoinPlacementY) + (CoinPlacementX * CoinPlacementX) >= radio * radio)
        {
            CoinPlacementX = Random.Range(-cameraLimitx, cameraLimitx);
            CoinPlacementY = Random.Range(-cameraLimity, cameraLimity);
        }
        Instantiate(BadCoin, new Vector3(CoinPlacementX, 0, CoinPlacementY), Quaternion.Euler(0, 0, 0));
        BadCoinCount++;


        // SpeedUP, al crear 10 BadCoins Crea otro generador de ambos Coins
        if (SpeedUp > 0)
        {
            SpeedUp--;
        }


    }
}
