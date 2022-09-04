using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMaster : MonoBehaviour
{
    // ゲームマスターオブジェクト
    public static GameObject gameMaster;
    // プレイヤーオブジェクト

    // Start is called before the first frame update
    void Start()
    {
        // ゲームマスターを取得する
        gameMaster = GameObject.Find("SceneSwitcher");
        // プレイヤーを取得する
    }

    // Update is called once per frame
    void Update()
    {

    }
}

