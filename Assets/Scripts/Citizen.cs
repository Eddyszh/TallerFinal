using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NPC.Enemy;

namespace NPC                                                                           //namespace NPC, contiene los namespace ally y enemy.
{
    namespace Ally                                                                      //namespace Ally, pertenece al namespace NPC y contiene la clase del ciudadano.
    {
        public class Citizen : Npc
        {
            CitizenInformation citizenInfo;                                             //Declaración de la estructura del ciudadano.
	        void Start ()
            {
                citizenInfo.name = (CitizenName)Random.Range(0, 20);                    //Asigna el nombre de manera aleatoria.
	        }
	
	       public CitizenInformation CitizenInfo()                                      //Función que devuelve la estructura del ciudadano.
           {
                return citizenInfo;
           }

            public override void Reaction()                                             //Sobreescribe el método de reacción para que el ciudadano huya del zombie al entrar en el rango de distancia.
            {
                foreach (GameObject go in GameManager.npc)
                {
                    if(go.GetComponent<Zombie>())
                    {
                        float dist = Vector3.Distance(transform.position, go.transform.position);
                        if (dist <= 5f)
                        {
                            //transform.position = Vector3.MoveTowards(transform.position, go.transform.position, -humanoidInfo.movementSpeed);
                            Vector3 dirToZombie = transform.position - go.transform.position;
                            Vector3 newPos = transform.position + dirToZombie;
                            agent.SetDestination(newPos);
                        }
                    }
                }
            }

            public static implicit operator Zombie(Citizen c)                           //Hace el cast de la clase ciudadano a la clase zombie, manteniendo la edad del mismo.
            {
                Zombie z = c.gameObject.AddComponent<Zombie>();
                print(z.humanoidInfo.age);
                z.humanoidInfo.age = c.humanoidInfo.age;
                print(z.humanoidInfo.age);
                Destroy(c);
                return z;
            }

            public static implicit operator Ghost(Citizen c)
            {
                Ghost g = c.gameObject.AddComponent<Ghost>();
                print(g.humanoidInfo.age);
                g.humanoidInfo.age = c.humanoidInfo.age;
                print(g.humanoidInfo.age);
                Destroy(c);
                return g;
            }
        }
    }
}

public enum CitizenName                                                                 //Enumerador que contiene la lista de nombres que se asignan al azar.
{
    Adolfo,
    Ramiro,
    Bob,
    Jimmy,
    Josefo,
    Leopoldo,
    Cirilo,
    Fabio,
    Yisus,
    Jasinto,
    Arnulfa,
    Berta,
    Gregoria,
    Gertrudis,
    Lola,
    Marta,
    Eva,
    Beatriz,
    Facunda,
    Pepa
}
public struct CitizenInformation                                                    //Estructura que contiene la informacion del ciudadano.
{
    public CitizenName name;
    static public explicit operator ZombieInformation(CitizenInformation c)
    {
        return new ZombieInformation();
    }
}


