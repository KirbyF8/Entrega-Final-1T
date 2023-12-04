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

    private bool Check = false;


    private AudioSource player_Audio;

    [SerializeField] private AudioClip Coin_SE;
    [SerializeField] private AudioClip BadCoin_SE;
    [SerializeField] private AudioClip GameOver;
    [SerializeField] private AudioClip GameComplete;

   

    // Update is called once per frame
    void Update()
    {
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
        if (Coin.gameObject.CompareTag("Good Coin"))
            {
            Destroy(Coin.gameObject);
            Points += 5;
            Debug.Log(Points + "Points");
            player_Audio.PlayOneShot(Coin_SE, 1f);

        }

        if (Coin.gameObject.CompareTag("Bad Coin"))
            {
            Destroy(Coin.gameObject);
            Lives--;
            Debug.Log(Lives + "Lives");
            player_Audio.PlayOneShot(BadCoin_SE, 1f);

        }

    }

    private void Checker()
    {
        if (Points == 40 && Check != true)
        {
            Debug.Log("You Win");
            player_Audio.PlayOneShot(GameComplete, 1f);
            Check = true;
            Time.timeScale = 0;
        }

        if (Lives < 0 && Check != true)
        {
            Debug.Log("You Lose");
            Check = true;
            player_Audio.PlayOneShot(GameOver, 1f);
            Time.timeScale = 0;

        }
    }
}
