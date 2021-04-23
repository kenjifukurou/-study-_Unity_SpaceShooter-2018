using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hero : MonoBehaviour
{
    private float _speed = 6f;
    public SpriteRenderer _heroSprite;
    public Animator _heroAnim;

    private float _jumpForce = 1000f;
    public Rigidbody2D _rb2D;
    private bool isGrounded;

    private bool shotTimer;
    private float shootTimer = 0.25f;

    public Transform left_Bullet_Position;
    public Transform right_Bullet_Position;
    public GameObject _bullet;

    private void Start()
    {
        shotTimer = false;
    }

    private void Update()
    {
        //timer on off loop every x seconds
        if (shotTimer == true)
        {
            shootTimer -= Time.deltaTime;
        }
        if (shootTimer <= 0)
        {
            shotTimer = false;
            _heroAnim.SetBool("isShooting", false);
            shootTimer = 0.25f;
        }

        Shoot();
        Movement();

        if (isGrounded)
        {
            Jump();
            _heroAnim.SetBool("isGrounded", true);
        }
        else
        {
            _heroAnim.SetBool("isGrounded", false);
        }

        //accelerate falling speed, give weight feel to character
        if (_rb2D.velocity.y < 0)
        {
            _rb2D.velocity += Vector2.up * Physics2D.gravity * 2 * Time.deltaTime;
        }
    }

    private void Movement()
    {
        if (Input.GetKey(KeyCode.A))
        {
            transform.Translate(Vector2.left * _speed * Time.deltaTime);
            _heroSprite.flipX = false;
            _heroAnim.SetBool("isWalking", true);
            _heroAnim.SetBool("isShooting", false);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            transform.Translate(Vector2.right * _speed * Time.deltaTime);
            _heroSprite.flipX = true;
            _heroAnim.SetBool("isWalking", true);
            _heroAnim.SetBool("isShooting", false);
        }
        else
        {
            _heroAnim.SetBool("isWalking", false);
        }
    }

    private void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            _rb2D.AddForce(Vector2.up * _jumpForce);
            isGrounded = false;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground" || collision.gameObject.tag == "Bullet")
        {
            isGrounded = true;
            _heroAnim.SetBool("isGrounded", true);
            _heroAnim.SetBool("isShooting", false);
        }
    }

    private void Shoot()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (shotTimer == false)
            {
                if (_heroSprite.flipX == false)
                {
                    GameObject left_Bullet = Instantiate(_bullet, left_Bullet_Position.position, transform.rotation);
                    left_Bullet.GetComponent<Rigidbody2D>().velocity = Vector2.left * 15f;
                }
                else
                {
                    GameObject right_Bullet = Instantiate(_bullet, right_Bullet_Position.position, transform.rotation);
                    right_Bullet.GetComponent<Rigidbody2D>().velocity = Vector2.right * 15f;
                }

                _heroAnim.SetBool("isShooting", true);
                shotTimer = true;
            }
        }
    }
}
