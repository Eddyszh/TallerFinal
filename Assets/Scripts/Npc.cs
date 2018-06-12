using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using NPC.Ally;
using NPC.Enemy;
[RequireComponent(typeof(Rigidbody))]                                                            //Añade cuerpo rigido al humanoid.
public class Npc : MonoBehaviour
{
    ZombieInformation zombieInfo;
    CitizenInformation citizenInfo;
    public HumanoidInformation humanoidInfo;
    public int move;
    //bool activeReaction = false;
    public NavMeshAgent agent;

    void Awake ()                                                                                //Inicializa la edad, la velocidad del zombie y ciudadano e inicia la corrutina que contiene los estados.
    {
        StartCoroutine(Behaviour());
        humanoidInfo.age = Random.Range(15, 100);
        humanoidInfo.movementSpeed = 200f / humanoidInfo.age * Time.deltaTime;
        agent = GetComponent<NavMeshAgent>();
        gameObject.layer = 9;
    }	
	
	void Update ()                                                                              //Contiene la ejecución de estados de los zombies y ciudadanos.
    {
        switch (move)
        {
            case 1:
                transform.position += transform.forward * humanoidInfo.movementSpeed;
                transform.Rotate(Vector3.zero);
                Reaction();
                break;
            case 2:
                transform.position -= transform.forward * humanoidInfo.movementSpeed;
                transform.Rotate(Vector3.zero);
                Reaction();
                break;
            case 5:
                transform.position += new Vector3(0, 0, 0);
                transform.Rotate(Vector3.zero);
                Reaction();
                break;
            case 6:
                transform.position += new Vector3(0, 0, 0);
                transform.Rotate(Vector3.up * humanoidInfo.rotatingSpeed);
                Reaction();
                break;
            case 7:
                transform.position += new Vector3(0, 0, 0);
                transform.Rotate(Vector3.down * humanoidInfo.rotatingSpeed);
                Reaction();
                break;
        }
    }

    public IEnumerator Behaviour()                                                              //Corrutina que elige el comportamiento del zombie y ciudadano de manera aleatoria cada 3 segunsdos.
    {
        yield return new WaitForSeconds(3);
        humanoidInfo.hb = (HumanoidBehaviour)Random.Range(0, 3);
        Movement();
        yield return new WaitForSeconds(3);
    }

    public void Movement()                                                                     //Método que inicia los comportamientos del zombie y ciudadano por medio de un switch.
    {
        switch (humanoidInfo.hb)
        {
            case 0:
                move = 5;
                StartCoroutine(Behaviour());
                break;
            case (HumanoidBehaviour)1:
                move = Random.Range(1, 3);
                StartCoroutine(Behaviour());
                break;
            case (HumanoidBehaviour)2:
                move = Random.Range(6, 8);
                humanoidInfo.rotatingSpeed = Random.Range(1, 5);
                StartCoroutine(Behaviour());
                break;
        }
    }

    public virtual void Reaction()                                                                  //Método que contiene la reacción del zombie y el ciudadano al entrar en un rango de distancia.
    {
        foreach (GameObject go in GameManager.npc)
        {
            if (go.GetComponent<Hero>() || go.GetComponent<Citizen>())
            {
                float dist = Vector3.Distance(transform.position, go.transform.position);
                if (dist <= 5f)
                {
                    //transform.position = Vector3.MoveTowards(transform.position, go.transform.position, humanoidInfo.movementSpeed);
                    Vector3 dirToZombie = transform.position - go.transform.position;
                    Vector3 newPos = go.transform.position - dirToZombie;
                    agent.SetDestination(newPos);
                }
            }
        }
    }

    public HumanoidInformation HumanoidInfo()
    {
        return humanoidInfo;
    }
}

public enum HumanoidBehaviour                                                                     //Enumerador que contiene los comportamientos del zombie y ciudadano.
{
    Idle,
    Moving,
    Rotating,
    Reaction
}

public struct HumanoidInformation
{
    public HumanoidBehaviour hb;
    public float rotatingSpeed;
    public float movementSpeed;
    public int age;
}