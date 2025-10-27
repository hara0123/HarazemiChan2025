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
    BlockDetector blockDetectorScript;

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
        blockDetectorScript = harazemichanObject.gameObject.GetComponent<BlockDetector>();

        // 信号受信時に呼ばれる関数としてOnDataReceived関数を登録
        serialHandler.OnDataReceived += OnDataReceived;
    }

    void Update()
    {
        // UnityからArduinoに送る場合はココに記述
        if (targetScript.jklPress[0])
        {
            targetScript.jklPress[0] = false;
            if (targetScript.jklToggle[0])
            {
                // LED ON
                serialHandler.Write("a");
            }
            else
            {
                // LED OFF
                serialHandler.Write("b");
            }
        }

        if (targetScript.jklPress[1])
        {
            targetScript.jklPress[1] = false;
            if (targetScript.jklToggle[1])
            {
                // LED ON
                serialHandler.Write("c");
            }
            else
            {
                // LED OFF
                serialHandler.Write("d");
            }
        }

        if (targetScript.jklPress[2])
        {
            targetScript.jklPress[2] = false;
            if (targetScript.jklToggle[2])
            {
                // LED ON
                serialHandler.Write("e");
            }
            else
            {
                // LED OFF
                serialHandler.Write("f");
            }
        }

        if (blockDetectorScript.led1on)
        {
            blockDetectorScript.led1on = false;
            serialHandler.Write("a");
        }

        if (blockDetectorScript.led1off)
        {
            blockDetectorScript.led1off = false;
            serialHandler.Write("b");
        }

        if (blockDetectorScript.led2on)
        {
            blockDetectorScript.led2on = false;
            serialHandler.Write("c");
        }

        if (blockDetectorScript.led2off)
        {
            blockDetectorScript.led2off = false;
            serialHandler.Write("d");
        }

        if (blockDetectorScript.led3on)
        {
            blockDetectorScript.led3on = false;
            serialHandler.Write("e");
        }

        if (blockDetectorScript.led3off)
        {
            blockDetectorScript.led3off = false;
            serialHandler.Write("f");
        }

        if (blockDetectorScript.soudnPlay)
        {
            blockDetectorScript.soudnPlay = false;
            serialHandler.Write("g");
        }

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
