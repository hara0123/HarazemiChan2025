using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    float moveValue_;
    public TextMeshProUGUI debugText_;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        moveValue_ = 0.01f;
        debugText_.text = "debug";
    }

    // Update is called once per frame
    void Update()
    {
        var current = Keyboard.current;

        if (current == null)
            return;

        if (current.rightArrowKey.isPressed)
            this.transform.position += new Vector3(moveValue_, 0f, 0f);

        if (current.leftArrowKey.isPressed)
            this.transform.position -= new Vector3(moveValue_, 0f, 0f);
    }
}
