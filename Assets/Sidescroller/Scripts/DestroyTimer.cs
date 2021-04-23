using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyTimer : MonoBehaviour
{
    public float destroyTimer;
    public ParticleSystem bulletExplosionPS;

    private void Update()
    {
        destroyTimer -= Time.deltaTime;
        if (destroyTimer < 0)
        {
            if (bulletExplosionPS)
            {
                Instantiate(bulletExplosionPS, transform.position, transform.rotation);
            }
            Destroy(this.gameObject);
        }
    }
}
