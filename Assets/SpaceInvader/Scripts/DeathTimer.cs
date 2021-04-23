using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathTimer : MonoBehaviour
{
    public float timer = 2f;
    public ParticleSystem deathParticles;

    private void Update()
    {
        timer -= Time.deltaTime;
        if (timer < 0)
        {
            if (deathParticles == true)
            {
                Instantiate(deathParticles, transform.position, transform.rotation);
            }
            Destroy(gameObject);
        }
    }
}
