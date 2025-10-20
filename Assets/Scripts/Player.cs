using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    float moveValue_;
    public TextMeshProUGUI debugText_;

    public int[] sw = new int[3];
    public int accX;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        moveValue_ = 0.01f;
        debugText_.text = "debug";

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

        if (current.rightArrowKey.isPressed)
            this.transform.position += new Vector3(moveValue_, 0f, 0f);

        if (current.leftArrowKey.isPressed)
            this.transform.position -= new Vector3(moveValue_, 0f, 0f);
    }
}
