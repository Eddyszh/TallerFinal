﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ammo : MonoBehaviour
{
    void Start()                                                                               //Inicia la posición de la caja aleatoria.
    {
        Vector3 pos = new Vector3(Random.Range(-70f, 70f), 0f, Random.Range(-70f, 70f));
        transform.position = pos;

    }

    void OnTriggerEnter(Collider other)                                                                     //Cuando entra en contacto con el heroe se desactiva la caja.
    {
        if (other.gameObject.GetComponent<Hero>())
        {
            gameObject.SetActive(false);
        }
    }

    void Spawn()                                                                                            //Método que reubica la caja luego de desaparecer.
    {
        Vector3 pos = new Vector3(Random.Range(-70f, 70f), 0f, Random.Range(-70f, 70f));
        transform.position = pos;
        gameObject.SetActive(true);
    }

    void OnDisable()                                                                                        //Cuando se desactiva, se invoca de nuevo la caja.
    {
        Invoke("Spawn", 5f);
    }
}
