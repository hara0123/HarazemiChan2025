using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    float moveValue_;
    float jumpValue_;
    public TextMeshProUGUI debugText_;

    public int[] swPre;
    public int[] sw;
    public int accX;

    public bool[] jklPress;
    public bool[] jklToggle;

    public float startTime;


    void Awake()
    {
        swPre = new int[5];
        sw = new int[5];

        jklPress = new bool[3];
        jklToggle = new bool[3];
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        moveValue_ = 0.01f;
        jumpValue_ = 5f;
        debugText_.text = "debug";

        swPre[0] = 1;
        swPre[1] = 1;
        swPre[2] = 1;
        swPre[3] = 1;
        swPre[4] = 1;

        sw[0] = 1;
        sw[1] = 1;
        sw[2] = 1;
        sw[3] = 1;
        sw[4] = 1;
        accX = 0;

        jklPress[0] = false;
        jklPress[1] = false;
        jklPress[2] = false;
        jklToggle[0] = false;
        jklToggle[1] = false;
        jklToggle[2] = false;

        startTime = -1;
    }

    // Update is called once per frame
    void Update()
    {
        var current = Keyboard.current;

        if (current == null)
            return;

        string str=string.Format("sw:{0}{1}{2}{3}{4}", sw[0], sw[1], sw[2], sw[3], sw[4]);
        debugText_.text = str;

        if (current.rightArrowKey.isPressed || sw[0] == 0)
            this.transform.position += new Vector3(moveValue_, 0f, 0f);

        if (current.leftArrowKey.isPressed || sw[1] == 0)
            this.transform.position -= new Vector3(moveValue_, 0f, 0f);

        if (current.zKey.wasPressedThisFrame || (sw[2] == 0 && swPre[2] == 1))
            GetComponent<Rigidbody>().linearVelocity = Vector3.up * jumpValue_;

        if (current.jKey.wasPressedThisFrame)
        {
            jklPress[0] = true;
            jklToggle[0] = !jklToggle[0];
        }
        if (current.kKey.wasPressedThisFrame)
        {
            jklPress[1] = true;
            jklToggle[1] = true;
        }
        if (current.kKey.wasReleasedThisFrame)
        {
            jklPress[1] = true;
            jklToggle[1] = false;
        }
        if (current.lKey.wasPressedThisFrame)
        {
            jklPress[2] = true;
            jklToggle[2] = true;
            startTime = 0;
        }

        if (startTime >= 0f)
        {
            startTime += Time.deltaTime;
            if (startTime > 3f)
            {
                jklPress[2] = true;
                jklToggle[2] = false;
                startTime = -1;
            }
        }

        //Vector3 curentPos = this.transform.position;
        //float zPos = (float)accX / 4000;
        //curentPos.z = zPos;
        //this.transform.position = curentPos;

        for (int i = 0;i < sw.Length; i++)
            swPre[i] = sw[i];
    }
}
