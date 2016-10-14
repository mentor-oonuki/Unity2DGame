using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour
{
    public float moveStartDistance = 10f;
    public float moveForce = 150f;
    public float maxSpeed = 100f;
    Vector2 horizontal;
    Rigidbody2D rb;

    // 接地しているかフラグ
    private bool isGrounded;
    //地面のレイヤー
    public LayerMask groundLayer;
    //ジャンプ力
    float jumpForce = 500;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (isGrounded && Input.GetKeyDown(KeyCode.Z))
        {
            Jump();
        }
    }

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

        isGrounded = Physics2D.Linecast(
            transform.position + transform.up * 1,
            transform.position - transform.up * 0.1f,
            groundLayer); //Linecastが判定するレイヤー



        // スプライトを移動方向に向かせます
        if (horizontal.normalized.x > 0)
        {
            Vector2 scale = transform.localScale;
            scale.x = 0.5f;
            transform.localScale = scale;
        }
        if (horizontal.normalized.x < 0)
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

    void Jump()
    {
        //上方向へ力を加える
        rb.AddForce(Vector2.up * jumpForce);
        //地面から離れるのでfalseにする
        isGrounded = false;
    }
}
