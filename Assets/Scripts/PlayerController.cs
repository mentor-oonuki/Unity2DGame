using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour
{
    public float moveStartDistance = 10f;
    public float moveForce = 150f;
    public float maxSpeed = 100f;
    Vector2 horizontal;


    // 物理演算用Update関数
    void FixedUpdate()
    {
        // 移動先が右か左か判定し、その方向に移動する力を加えます
        horizontal = Vector2.zero;
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            horizontal = Vector2.left;
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            horizontal = Vector2.right;
        }


        // スプライトを移動方向に向かせます
        if(horizontal.normalized.x > 0)
        {
            Vector2 scale = transform.localScale;
            scale.x = 0.5f;
            transform.localScale = scale;
        }
        if(horizontal.normalized.x < 0)
        {
            Vector2 scale = transform.localScale;
            scale.x = -0.5f;
            transform.localScale = scale;
        }


        // 移動する力を加えます
        GetComponent<Rigidbody2D>().AddForce(horizontal * moveForce);
    }


    // 敵の攻撃の当たり判定処理
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Fire")
        {
            Animator myAnimator = GetComponent<Animator>();
            myAnimator.SetTrigger("Damage");
        }
    }
}