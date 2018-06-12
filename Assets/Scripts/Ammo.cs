using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ammo : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<Hero>())
        {
            gameObject.SetActive(false);
        }
    }

    void Spawn()
    {
        Vector3 pos = new Vector3(Random.Range(-70f, 70f), 0f, Random.Range(-70f, 70f));
        transform.position = pos;
        gameObject.SetActive(true);
    }

    void OnDisable()
    {
        Invoke("Spawn", 5f);
    }
}
