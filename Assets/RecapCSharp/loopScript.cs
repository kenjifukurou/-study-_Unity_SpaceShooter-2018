using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class loopScript : MonoBehaviour
{
    public GameObject[] spawner;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            foreach (GameObject cube in spawner)
            {
                cube.SetActive(true);
            }
        }
    }
}
