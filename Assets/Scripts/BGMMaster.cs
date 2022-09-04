using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGMMaster : MonoBehaviour
{
    AudioSource audioSource;

    public int musicState;

    public AudioClip BGM1;
    [SerializeField] public float BGM1Time = 90;

    public AudioClip BGM2;
    [SerializeField] public float BGM2Time = 210;

    public AudioClip BGM3;
    [SerializeField] public float BGM3Time = 80;

    public float musicTime;
    public bool musicOn = false;
    // Start is called before the first frame update

    void Start()
    {
        musicState = 0;
        audioSource = GetComponent<AudioSource>();
        musicTime = 0;
        musicOn = true;
    }

    // Update is called once per frame
    void Update()
    {
        Music();
    }

    public void Music()
    {
        //Debug.Log(musicTime);
        musicTime += Time.deltaTime;

        if (musicState == 0 && musicOn == true)
        {
            audioSource.PlayOneShot(BGM1);
            musicOn = false;
        }
        if (musicState == 0 && musicTime >= BGM1Time && musicOn == false)
        {
            Debug.Log("!!!!");
            musicTime = 0;
            musicState = 1;
            musicOn = true;
        }

        if (musicState == 1 && musicOn == true)
        {
            audioSource.PlayOneShot(BGM2);
            musicOn = false;
        }
        if (musicState == 1 && musicTime >= BGM2Time && musicOn == false)
        {
            musicTime = 0;
            musicState = 2;
            musicOn = true;
        }

        if (musicState == 2 && musicOn == true)
        {
            audioSource.PlayOneShot(BGM3);
            musicOn = false;
        }
        if (musicState == 2 && musicTime >= BGM3Time && musicOn == false)
        {
            musicTime = 0;
            musicState = 0;
            musicOn = true;
        }
    }
}
