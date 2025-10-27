using UnityEngine;
using StarterAssets;

public class BlockDetector : MonoBehaviour
{
    GameObject previousGround;

    ThirdPersonController tpcScript;
    CharacterController ccScript;

    GameObject playerObject;
    Player playerScript;

    public bool led1on;
    public bool led1off;
    public bool led2on;
    public bool led2off;
    public bool led3on;
    public bool led3off;
    public bool soudnPlay;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        tpcScript = GetComponent<ThirdPersonController>();
        ccScript = GetComponent<CharacterController>();
        previousGround = null;

        playerObject = GameObject.Find("PlayerObject");
        playerScript = playerObject.GetComponent<Player>();

        led1on = false;
        led1off = false;
        led2on = false;
        led2off = false;
        led3on = false;
        led3off = false;
        soudnPlay = false;
    }

    // Update is called once per frame
    void Update()
    {
        GameObject newGround = null;

        if (tpcScript.Grounded)
        {
            // CharacterController の足元中心を求める
            Vector3 origin = transform.position
                           + ccScript.center
                           + Vector3.down * (ccScript.height * 0.5f - ccScript.radius - 0.03f)
                           + Vector3.up * 0.02f; // 少し上から撃つと安定する

            float radius = ccScript.radius * 0.95f;
            float distance = 0.3f;

            // ~0は全てのレイヤーを有効という意味
            if (Physics.SphereCast(origin, radius, Vector3.down, out RaycastHit hit, distance, ~0, QueryTriggerInteraction.Ignore))
            {
                // 法線の向きが上向き寄りなら地面と判定
                if (hit.normal.y > 0.3f)
                {
                    newGround = hit.collider.gameObject;
                }
            }
        }

        // 変化あり
        if (newGround != previousGround)
        {
            if (newGround != null)
            {
                if (newGround.name == "Block1")
                {
                    led1on = true;
                    led2off = true;
                    led3off = true;
                    Debug.Log("block1");
                }
                else if (newGround.name == "Block2")
                {
                    led1off = true;
                    led2on = true;
                    led3off = true;
                    Debug.Log("block2");
                }
                else if (newGround.name == "Block3")
                {
                    led1off = true;
                    led2off = true;
                    led3on = true;
                    soudnPlay = true;
                    Debug.Log("block3");
                }
            }
            else
            {
                led1off = true;
                led2off = true;
                led3off = true;
            }
        }

        previousGround = newGround;
    }
}
