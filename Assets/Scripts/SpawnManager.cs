using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpawnManager : MonoBehaviour
{

    [SerializeField] private GameObject Coin;
    [SerializeField] private GameObject BadCoin;

    private float cameraLimitx = 9f;
    private float cameraLimity = 6f;
    private float CoinPlacementX;
    private float CoinPlacementY;

    [SerializeField] private float startDelay = 2f;
    private float spawnInterval = 2.4f;
    private int SpeedUp = 30;
    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("SpawnCoin", startDelay, spawnInterval);
        InvokeRepeating("SpawnBadCoin", startDelay + 1f, spawnInterval);
        if (SpeedUp < 0)
        {
            InvokeRepeating("SpawnCoin", startDelay, spawnInterval + 2f);
            InvokeRepeating("SpawnBadCoin", startDelay + 2f, spawnInterval + 1f);
        }
    }

  

    private void SpawnCoin()
    {
        CoinPlacementX = Random.Range(-cameraLimitx, cameraLimitx);
        CoinPlacementY = Random.Range(-cameraLimity, cameraLimity);
        Instantiate(Coin, new Vector3(CoinPlacementX, 0, CoinPlacementY), Quaternion.Euler(0, 0, 0));
        

    }

    private void SpawnBadCoin()
    {
        CoinPlacementX = Random.Range(-cameraLimitx, cameraLimitx);
        CoinPlacementY = Random.Range(-cameraLimity, cameraLimity);
        Instantiate(BadCoin, new Vector3(CoinPlacementX, 0, CoinPlacementY), Quaternion.Euler(0, 0, 0));
    }
}
