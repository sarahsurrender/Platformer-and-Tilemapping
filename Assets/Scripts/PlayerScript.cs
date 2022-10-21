using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerScript : MonoBehaviour
{
    private Rigidbody2D rd2d;

    public float speed;

    // Animator
    public Animator animator;
    private bool facingRight = true;


    // Score and Lives Text and Integers
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI livesText;
    private int scoreValue;
    private int livesValue;

    // Win and Lose Text
    public GameObject WinTextObject;
    public GameObject LoseTextObject;


    // Ground Check Variables
    private bool isOnGround;
    public Transform groundcheck;
    public float checkRadius;
    public LayerMask allGround;

    // Start is called before the first frame update
    void Start()
    {
        rd2d = GetComponent<Rigidbody2D>();
        scoreValue = 0;

        rd2d = GetComponent<Rigidbody2D>();
        livesValue = 3;

        SetCountText();
        WinTextObject.SetActive(false);

        SetCountText();
        LoseTextObject.SetActive(false);
    }

    void SetCountText()
    {
        scoreText.text = "Score: " + scoreValue.ToString();
        if (scoreValue >= 8)
        {
            WinTextObject.SetActive(true);
            Destroy(gameObject);

            SoundMangerScript.PlaySound("yay");
        }

        livesText.text = "Lives: " + livesValue.ToString();
        if (livesValue == 0)
        {
            LoseTextObject.SetActive(true);
            Destroy(gameObject);
        }

        // transform to round two
        scoreText.text = "Score: " + scoreValue.ToString();
        if (scoreValue == 4)
        {
            livesValue = 3;
            transform.position = new Vector2(100f, 0.5f);
        }

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float hozMovement = Input.GetAxis("Horizontal");
        float vertMovement = Input.GetAxis("Vertical");
        rd2d.AddForce(new Vector2(hozMovement * speed, vertMovement * speed));


        // Animation
        animator.SetFloat("HorizontalValue", Mathf.Abs(Input.GetAxis("Horizontal")));
        animator.SetFloat("VerticalValue", Input.GetAxis("Vertical"));

        if (facingRight == false && hozMovement > 0)
        {
            Flip();
        }

        else if (facingRight == true && hozMovement < 0)
        {
            Flip();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Coin")
        {
            scoreValue += 1;
            SetCountText();
            Destroy(collision.collider.gameObject);
        }
        if (collision.collider.tag == "Enemy")
        {
            Destroy(collision.collider.gameObject);
            livesValue = livesValue - 1;

            SetCountText();
        }

    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.collider.tag == "Ground")
        {
            if (Input.GetKey(KeyCode.W))
            {
                rd2d.AddForce(new Vector2(0, 3), ForceMode2D.Impulse);
            }
        }
    }

    void Flip()
    {
        facingRight = !facingRight;
        Vector2 Scaler = transform.localScale;
        Scaler.x = Scaler.x * -1;
        transform.localScale = Scaler;
    }
}
