using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NPC.Enemy;

public class EnemyHealth : MonoBehaviour
{
    int currentHealth = 3;                                                          //Cantidad de vida de los enemigos.
    Hero hero;                                                                      //Declaración de la clase heroe.

    void Start()                                                                    //Obtiene el componente de la clase heroe.
    {
        hero = FindObjectOfType<Hero>().GetComponent<Hero>();
    }

    public void TakeDamage(int amount, Vector3 hitPoint)                            //Método que calcula el daño cuando lo impacta un proyectil.
    {
        currentHealth -= amount;

        if (currentHealth <= 0)
        {
            hero.isAlive = false;
            gameObject.SetActive(false);
        }
    }
}
