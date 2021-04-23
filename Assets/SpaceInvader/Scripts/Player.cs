using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    private float _speed = 12f;

    public Transform shootPosL;
    public Transform shootPosC;
    public Transform shootPosR;
    public GameObject playerBullet;
    public ParticleSystem powerUpParticles;

    public int powerUp;

    private AudioSource _audioSource;
    public AudioClip laserSound;

    //referencing MovementLimit custom class
    public MovementLimit _movementLimit;

    public int playerHealth;
    public ParticleSystem playerExplosion;

    public int score;
    private int highScore;

    //setup UI display - score
    public Text scoreText;
    public Text highscoreText;

    private void Start()
    {
        powerUp = 1;
        _audioSource = GetComponent<AudioSource>();

        if (!PlayerPrefs.HasKey("highscore"))
        {
            highScore = 0;
            PlayerPrefs.SetInt("highscore", highScore);
        }
        else
        {
            highScore = PlayerPrefs.GetInt("highscore", highScore);
        }
    }

    private void Update()
    {
        if (score > highScore)
        {
            highScore = score;
            PlayerPrefs.SetInt("highscore", highScore);
        }

        scoreText.text = score.ToString();
        highscoreText.text = highScore.ToString();

        if (playerHealth <= 0)
        {
            if (playerExplosion)
            {
                Instantiate(playerExplosion, transform.position, transform.rotation);
            }
            Destroy(gameObject);
        }

        Shooting();

        Movement();
        transform.position = new Vector3(
            Mathf.Clamp(transform.position.x, _movementLimit.minX, _movementLimit.maxX),
            Mathf.Clamp(transform.position.y, _movementLimit.minY, _movementLimit.maxY),
            0f);
    }

    private void Movement()
    {
        if (Input.GetKey(KeyCode.A))
        {
            transform.Translate(Vector3.left * _speed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.D))
        {
            transform.Translate(Vector3.right * _speed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.W))
        {
            transform.Translate(Vector3.up * _speed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.S))
        {
            transform.Translate(Vector3.down * _speed * Time.deltaTime);
        }
    }

    private void Shooting()
    {
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
        {
            _audioSource.PlayOneShot(laserSound, 1f);

            switch (powerUp)
            {
                case 1:
                    {
                        GameObject _bullet1 = Instantiate(playerBullet, shootPosC.position, transform.rotation);
                        _bullet1.GetComponent<Rigidbody>().velocity = Vector3.forward * 20f;
                    }
                    break;
                case 2:
                    {
                        GameObject _bullet1 = Instantiate(playerBullet, shootPosL.position, transform.rotation);
                        GameObject _bullet2 = Instantiate(playerBullet, shootPosR.position, transform.rotation);
                        _bullet1.GetComponent<Rigidbody>().velocity = Vector3.forward * 20f;
                        _bullet2.GetComponent<Rigidbody>().velocity = Vector3.forward * 20f;
                    }
                    break;
                case 3:
                    {
                        GameObject _bullet1 = Instantiate(playerBullet, shootPosC.position, transform.rotation);
                        GameObject _bullet2 = Instantiate(playerBullet, shootPosL.position, transform.rotation);
                        GameObject _bullet3 = Instantiate(playerBullet, shootPosR.position, transform.rotation);
                        _bullet1.GetComponent<Rigidbody>().velocity = Vector3.forward * 20f;
                        _bullet2.GetComponent<Rigidbody>().velocity = Vector3.forward * 20f;
                        _bullet3.GetComponent<Rigidbody>().velocity = Vector3.forward * 20f;
                    }
                    break;
                default:
                    {
                        GameObject _bullet1 = Instantiate(playerBullet, shootPosC.position, transform.rotation);
                        _bullet1.GetComponent<Rigidbody>().velocity = Vector3.forward * 20f;
                    }
                    break;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "PowerUp")
        {
            if (powerUp < 3)
            {
                powerUp++;
                Debug.Log(powerUp);
                Destroy(other.gameObject);
                Instantiate(powerUpParticles, other.transform.position, other.transform.rotation);
            }
        }
        else if (other.tag == "PowerDown")
        {
            if (powerUp > 1)
            {
                powerUp--;
                Debug.Log(powerUp);
                Destroy(other.gameObject);
                Instantiate(powerUpParticles, other.transform.position, other.transform.rotation);
            }
        }
        else if (other.tag == "EnemyBullet")
        {
            playerHealth--;
            Instantiate(playerExplosion, transform.position, transform.rotation);
        }
    }
}
