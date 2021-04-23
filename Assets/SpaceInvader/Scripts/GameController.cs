using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public MovementLimit _movementLimit;
    public GameObject _enemy1;
    public GameObject _enemy2;
    public GameObject _enemy3;

    //timer
    public float spawnTimer;
    public float maxTimer;

    // Start is called before the first frame update
    void Start()
    {
        Instantiate(_enemy1, new Vector3(
            Random.Range(_movementLimit.minX, _movementLimit.maxX), 
            Random.Range(_movementLimit.minY, _movementLimit.maxY), 
            35), _enemy1.transform.rotation);

        spawnTimer = maxTimer;
    }

    private void Update()
    {
        spawnTimer -= Time.deltaTime;
        if (spawnTimer < 0)
        {
            SpawnEnemy();
            spawnTimer = maxTimer;
        }
    }

    private void SpawnEnemy()
    {
        int randomNumber = Random.Range(0, 3);
        
        switch (randomNumber)
        {
            case 0 :
                {
                    Instantiate(_enemy1, new Vector3(
                        Random.Range(_movementLimit.minX, _movementLimit.maxX),
                        Random.Range(_movementLimit.minY, _movementLimit.maxY),
                        35), _enemy1.transform.rotation);
                }
                break;
            case 1:
                {
                    Instantiate(_enemy2, new Vector3(
                        Random.Range(_movementLimit.minX, _movementLimit.maxX),
                        Random.Range(_movementLimit.minY, _movementLimit.maxY),
                        35), _enemy2.transform.rotation);
                }
                break;
            case 2:
                {
                    Instantiate(_enemy3, new Vector3(
                        Random.Range(_movementLimit.minX, _movementLimit.maxX),
                        Random.Range(_movementLimit.minY, _movementLimit.maxY),
                        35), _enemy3.transform.rotation);
                }
                break;
            default:
                {
                    Instantiate(_enemy1, new Vector3(
                        Random.Range(_movementLimit.minX, _movementLimit.maxX),
                        Random.Range(_movementLimit.minY, _movementLimit.maxY),
                        35), _enemy1.transform.rotation);
                }
                break;                
        }
    }
    
}
