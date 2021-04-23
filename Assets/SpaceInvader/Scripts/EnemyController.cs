using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public float _speed;
    public float changeDirectionTimer;
    private float maxTimer;

    //if true = left, if false = right
    public bool changeDirection;
    
    private Vector3 startPosition;

    Rigidbody _rb;

    public int healthPoint;
    public ParticleSystem deathParticle;

    public MovementLimit _movementLimit;

    public Transform shootPosition;
    public GameObject enemyBullet;
    public bool canShoot;
    public int bulletSpeed;

    //shoot timer
    public float shootTimer;
    public float maxShootTimer;

    //if kill, score reward point
    public int scoreReward;

    public GameObject powerUp;
    public GameObject powerDown;

    private void Start()
    {
        _rb = GetComponent<Rigidbody>();

        maxTimer = changeDirectionTimer;

        shootTimer = maxShootTimer;
    }

    private void Update()
    {
        if (canShoot == true)
        {
            Shoot();
        }

        Movement();

        if (transform.position.x < _movementLimit.minX) SwitchDir(changeDirection);
        if (transform.position.x > _movementLimit.maxX) SwitchDir(changeDirection);

        transform.position = new Vector3(
            Mathf.Clamp(transform.position.x, _movementLimit.minX, _movementLimit.maxX),
            Mathf.Clamp(transform.position.y, _movementLimit.minY, _movementLimit.maxY),
            transform.position.z);
        SwitchDirectionTimer();
    }

    private void Movement()
    {
        if (changeDirection == true)
        {
            _rb.velocity = new Vector3(_speed * Time.deltaTime, 0, -_speed * Time.deltaTime);
        }
        else
        {
            _rb.velocity = new Vector3(-_speed * Time.deltaTime, 0, -_speed * Time.deltaTime);
        }
    }

    private void SwitchDirectionTimer()
    {
        changeDirectionTimer -= Time.deltaTime;

        if (changeDirectionTimer < 0)
        {
            SwitchDir(changeDirection);
            changeDirectionTimer = maxTimer;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "PlayerBullet")
        {
            Destroy(other.gameObject);

            if (deathParticle)
            {
                Instantiate(deathParticle, transform.position, transform.rotation);
            }

            healthPoint--;
            if (healthPoint <= 0)
            {
                GameObject.FindWithTag("Player").GetComponent<Player>().score += scoreReward;
                Destroy(gameObject);

                int powerDropNumber = Random.Range(0, 100);
                if (powerDropNumber <= 30)
                {
                    Debug.Log("power up drop");
                    GameObject powerUpDrop = Instantiate(powerUp, transform.position, transform.rotation);
                    powerUpDrop.GetComponent<Rigidbody>().velocity = Vector3.back * Time.deltaTime * 300f;
                }
                else if (powerDropNumber >= 31 && powerDropNumber <= 60)
                {
                    Debug.Log("power down drop");
                    GameObject powerDownDrop = Instantiate(powerDown, transform.position, transform.rotation);
                    powerDownDrop.GetComponent<Rigidbody>().velocity = Vector3.back * Time.deltaTime * 300f;

                }
                else if (powerDropNumber >= 61 && powerDropNumber <= 100)
                {
                    Debug.Log("nothing drop");
                }
            }
        }

        if (other.tag == "Player")
        {
            Player _player = other.GetComponent<Player>();
            _player.playerHealth--;

            if (deathParticle)
            {
                Instantiate(deathParticle, transform.position, transform.rotation);
            }

            healthPoint--;
            if (healthPoint <= 0)
            {
                other.GetComponent<Player>().score += scoreReward;
                Destroy(gameObject);
            }
        }
    }

    private bool SwitchDir(bool dir)
    {
        //when bool dir pass in check if true or false, then toggle it
        if (dir == true) changeDirection = false;
        else changeDirection = true;
        return changeDirection;
    }

    private void Shoot()
    {
        shootTimer -= Time.deltaTime;
        if (shootTimer < 0)
        {
            GameObject shotBullet = Instantiate(enemyBullet, shootPosition.position, shootPosition.rotation);
            shotBullet.GetComponent<Rigidbody>().velocity = Vector3.back * Time.deltaTime * bulletSpeed;
            maxShootTimer = Random.Range(0.5f, 3f);
            shootTimer = maxShootTimer;
        }
    }
}
