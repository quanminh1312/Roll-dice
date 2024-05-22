using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectSystem : MonoBehaviour
{
    public static EffectSystem instance;
    public GameObject ExplosionPrefab = null;
    void Start()
    {
        if (instance)
        {
            Debug.Log("more than 1 effect system");
            Destroy(gameObject);
            return;
        }

        instance = this;
    }
    public void SpawnSmallExplosion(Vector3 position)
    {
        Instantiate(ExplosionPrefab,position,Quaternion.identity);
    }
}
