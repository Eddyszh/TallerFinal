using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NPC.Ally;

namespace NPC                                                                                   //namespace NPC, contiene los namespace ally y enemy.
{
    namespace Enemy                                                                             //namespace Enemy, pertenece al namespace NPC y contiene la clase del zombie.
    {
        public sealed class Zombie : Npc 
        {
            ZombieInformation zombieInfo;                                                       //Declaración de la estructura del zombie.
            GameManager gm;
	        void Start ()
            {
                zombieInfo.taste = (Taste)Random.Range(0, 5);                                   //Asigna el gusto del zombie al azar.
                zombieInfo.color = new Color[] { Color.cyan, Color.green, Color.magenta };      //Array de color para asignar de manera aleatoria a cada zombie.
                gameObject.GetComponent<Renderer>().material.color = zombieInfo.color[Random.Range(0, 3)];
                gm = FindObjectOfType<GameManager>().GetComponent<GameManager>();
                gameObject.AddComponent<EnemyHealth>();
	        }

            public ZombieInformation ZombieInfo()                                               //Función que devuelve la estructura del zombie.
            {
                return zombieInfo;
            }

            private void OnCollisionEnter(Collision collision)                                  //Si entra en colision con el ciudadano lo convierte en zombiey modifica el contador.
            {
                if (collision.gameObject.GetComponent<Citizen>())
                {
                    Citizen c = collision.gameObject.GetComponent<Citizen>();
                    Zombie z = c;
                    print("Desde zombie " + z.humanoidInfo.age);
                    gm.citizenCount--;
                    gm.citizenText.text = "Citizen: " + gm.citizenCount.ToString();
                    gm.zombieCount++;
                    gm.zombieText.text = "Zombie: " + gm.zombieCount.ToString();
                }
            }

            void OnDisable()
            {
                gm.zombieCount--;
                gm.zombieText.text = "Zombie: " + gm.zombieCount.ToString();
            }
        }
    }
}

public enum Taste                                                                               //Enumerador que contiene los gustos del zombie.
{
    cerebros,
    brazos,
    piernas,
    ojos,
    tripas
}

public struct ZombieInformation                                                         //Estructura que contiene la información del zombie.
{
    public Color[] color;
    public Taste taste;
    public float zombieAge;
}