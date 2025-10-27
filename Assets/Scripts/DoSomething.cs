// Unityでシリアル通信で送られてくるデータをデコードする雛形
// 2025_8月Ver.


using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StarterAssets;

public class DoSomething : MonoBehaviour
{
    // 制御対象のオブジェクト用に宣言しておいて、Start関数内で名前で検索
    GameObject targetObject;
    Player targetScript; // UnityプロジェクトにMainスクリプトが必要

    GameObject harazemichanObject;
    StarterAssetsInputs harazemichanSAIScript;

    // シリアル通信のクラス、クラス名は正しく書くこと
    public SerialHandler serialHandler;

  void Start()
    {
        // 制御対象のオブジェクトを取得
        targetObject = GameObject.Find("PlayerObject"); // UnityのヒエラルキーにGameMasterオブジェクトがいること。このオブジェクトにMain.csが関連付けられている
        // 制御対象に関連付けられたスクリプトを取得。
        // 大文字、小文字を区別するので、player.csを作ったのなら「p」layer。
        targetScript = targetObject.GetComponent<Player>(); // こちらはスクリプトの名前

        harazemichanObject = GameObject.Find("harazemi_chan025_chara");
        harazemichanSAIScript = harazemichanObject.GetComponent<StarterAssetsInputs>();

        // 信号受信時に呼ばれる関数としてOnDataReceived関数を登録
        serialHandler.OnDataReceived += OnDataReceived;
    }

    void Update()
    {
        // UnityからArduinoに送る場合はココに記述
        string command = "hogehoge";
        //serialHandler.Write(command);
    }

    //受信した信号(message)に対する処理
    void OnDataReceived(string message)
    {
        if (message == null)
            return;

        // ここでデコード処理等を記述
        int t;
        string receivedData;

        receivedData = message.Substring(1, 1);
        int.TryParse(receivedData, out t);
        targetScript.sw[0] = t;

        receivedData = message.Substring(2, 1);
        int.TryParse(receivedData, out t);
        targetScript.sw[1] = t;

        receivedData = message.Substring(3, 1);
        int.TryParse(receivedData, out t);
        targetScript.sw[2] = t;

        receivedData = message.Substring(4, 1);
        int.TryParse(receivedData, out t);
        targetScript.sw[3] = t;

        receivedData = message.Substring(5, 1);
        int.TryParse(receivedData, out t);
        targetScript.sw[4] = t;

        float x = 0f, y = 0f;
        bool isMoveChange = false;

        if (targetScript.sw[0] == 0 && targetScript.swPre[0] == 1)
        {
            x = 1f;
            isMoveChange = true;
        }
        else if (targetScript.sw[0] == 1 && targetScript.swPre[0] == 0)
        {
            x = 0f;
            isMoveChange = true;
        }

        if (targetScript.sw[1] == 0 && targetScript.swPre[1] == 1)
        {
            x = -1f;
            isMoveChange = true;
        }
        else if (targetScript.sw[1] == 1 && targetScript.swPre[1] == 0)
        {
            x = 0f;
            isMoveChange = true;
        }

        if (targetScript.sw[2] == 0 && targetScript.swPre[2] == 1)
            harazemichanSAIScript.JumpInput(true);

        if (isMoveChange)
        {
            harazemichanSAIScript.MoveInput(new Vector2(x, y));
        }
    }
}
