using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwitcher : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
       
    }

    public void ToMain()
    {
        //Mainシーンに移動する
        SceneManager.LoadScene("Endless");
    }

    public void ToOpening()
    {
        SceneManager.LoadScene("Start");
    }

    public void Retry()
    {
        SceneManager.LoadScene("Endless");
    }

    public void Tutorial()
    {
        SceneManager.LoadScene("Tutorial");
    }

    public void Story()
    {
        SceneManager.LoadScene("Story");
    }
}
