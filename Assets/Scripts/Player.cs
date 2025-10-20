using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    float moveValue_;
    float jumpValue_;
    public TextMeshProUGUI debugText_;

    int[] swPre = new int[3];
    public int[] sw = new int[3];
    public int accX;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        moveValue_ = 0.01f;
        jumpValue_ = 5f;
        debugText_.text = "debug";

        swPre[0] = 1;
        swPre[1] = 1;
        swPre[2] = 1;

        sw[0] = 1;
        sw[1] = 1;
        sw[2] = 1;
        accX = 0;
    }

    // Update is called once per frame
    void Update()
    {
        var current = Keyboard.current;

        if (current == null)
            return;

        string str=string.Format("acc:{0} sw:{1}{2}{3}", accX, sw[0], sw[1], sw[2]);
        debugText_.text = str;

        if (current.rightArrowKey.isPressed || sw[0] == 0)
            this.transform.position += new Vector3(moveValue_, 0f, 0f);

        if (current.leftArrowKey.isPressed || sw[1] == 0)
            this.transform.position -= new Vector3(moveValue_, 0f, 0f);

        if (current.zKey.wasPressedThisFrame || (sw[2] == 0 && swPre[2] == 1))
            GetComponent<Rigidbody>().linearVelocity = Vector3.up * jumpValue_;

        Vector3 curentPos = this.transform.position;
        float zPos = (float)accX / 4000;
        curentPos.z = zPos;
        this.transform.position = curentPos;

        for(int i = 0;i < sw.Length; i++)
            swPre[i] = sw[i];
    }
}
