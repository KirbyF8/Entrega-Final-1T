using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_S : MonoBehaviour
{

    [SerializeField] private float playerspeed = 15f;

    private float HorizontalInput;
    private float VerticalInput;


    private float cameraLimitx = 9.5f;
    private float cameraLimity = 7f;
    // Start is called before the first frame update

    private int Points = 0;
    private int Lives = 3;
    private int SumP;
    public bool Check = false;


    private AudioSource player_Audio;
    
    private Animator player_Ani;
    private const string C_Coin_ANI = "Coin_T";

    [SerializeField] private AudioClip Coin_SE;
    [SerializeField] private AudioClip BadCoin_SE;
    [SerializeField] private AudioClip GameOver;
    [SerializeField] private AudioClip GameComplete;

    [SerializeField] private ParticleSystem Coin_FX;
    [SerializeField] private ParticleSystem BadCoin_FX;
    [SerializeField] private ParticleSystem GameOver_FX;
    [SerializeField] private ParticleSystem GameComplete_FX;

    private SpawnManager spawnManager;
    private CoinAutodestruct coinP;
    // Update is called once per frame
    void Update()
    {
        if (Check)
        {
            playerspeed = 0;
        }
        HorizontalInput = Input.GetAxis("Horizontal");

        transform.Translate(Vector3.right * playerspeed * Time.deltaTime * HorizontalInput);

        VerticalInput = Input.GetAxis("Vertical");

        transform.Translate(Vector3.forward * playerspeed * Time.deltaTime * VerticalInput);

        PlayerBounds();
        Checker();


    }

    private void Awake()
    {
        player_Audio = GetComponent<AudioSource>();
        player_Ani = GetComponent<Animator>();


    }

    private void Start()
    {
        spawnManager = FindObjectOfType<SpawnManager>();
       
    }

    private void PlayerBounds()
    {
        Vector3 position = transform.position;

        if (position.x >= cameraLimitx)
        {
            transform.position = new Vector3(-cameraLimitx, position.y, position.z);
        }
        if (position.x <= -cameraLimitx)
        {
            transform.position = new Vector3(cameraLimitx, position.y, position.z);
        }

        if (position.z >= cameraLimity)
        {
            transform.position = new Vector3(position.x, position.y, -cameraLimity);
        }
        if (position.z <= -cameraLimity)
        {
            transform.position = new Vector3(position.x, position.y, cameraLimity);
        }

    }


    private void OnTriggerEnter(Collider Coin)
    {
        if (Coin.gameObject.CompareTag("Good Coin") || Coin.gameObject.CompareTag("Bronze Coin") || Coin.gameObject.CompareTag("Silver Coin"))
        {
            Instantiate(Coin_FX, transform.position, Quaternion.identity);
            coinP = FindObjectOfType<CoinAutodestruct>();
            
            Points += coinP.points;
            Debug.Log(Points + "Points");
            player_Audio.PlayOneShot(Coin_SE, 1f);
            Destroy(Coin.gameObject);
        }

        

        if (Coin.gameObject.CompareTag("Bad Coin"))
        {

            player_Ani.SetTrigger(C_Coin_ANI);
            Instantiate(BadCoin_FX, transform.position, Quaternion.identity);
            Destroy(Coin.gameObject);
            Lives--;
            Debug.Log(Lives + "Lives");
            player_Audio.PlayOneShot(BadCoin_SE, 1f);
            spawnManager.BadCoinCount--;
        }

    }

    private void Checker()
    {
        if (Points >= 50 && Check != true)
        {
            Instantiate(GameComplete_FX, transform.position, Quaternion.identity);
            Debug.Log("You Win");
            GameComplete_FX.Play();
            player_Audio.PlayOneShot(GameComplete, 1f);
            Check = true;
            
        }

        if (Lives < 0 && Check != true)
        {
            Instantiate(GameOver_FX, transform.position, Quaternion.identity);
            Debug.Log("You Lose");
            Check = true;
            player_Audio.PlayOneShot(GameOver, 1f);
            

        }
    }

   
}
