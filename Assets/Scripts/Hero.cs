using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NPC.Ally;                                                                                   //namespace ally, pertenece al namespace NPC y contiene la clase del ciudadano.
using NPC.Enemy;                                                                                  //namespace enemy, pertenece al namespace NPC y contiene la clase del zombie.

public class Hero : MonoBehaviour
{
    CitizenInformation citizenInfo;                                                               //Declaración de la estructura del ciudadano.
    ZombieInformation zombieInfo;                                                                 //Declaración de la estructura del zombie.
    HumanoidInformation humanoidInfo;                                                             //Declaración de la estructura del humanoide, este contiene la información que se comparte en zombie y ciudadano.

    float health = 100f;
    Gun gun;
    GameManager gm;
    //[SerializeField]Npc npc;
    float msgTime = 2f;
    bool touching = false;
    float dist;
    public bool isAlive = true;

    void Awake ()                                                                                 //Agrega los scripts de movimiento al heroe, asigna la cámara como hijo y la ubica en la posición del heroe.
    {
        //gameObject.AddComponent<FPSAim>();
        gameObject.GetComponent<TPSMove>().speed += new MovSpeed().movSpeed;
        //Camera.main.gameObject.transform.localPosition = gameObject.transform.position + (new Vector3(0f, 1.5f, -2.5f));
        /*Camera.main.transform.SetParent(gameObject.transform);
        Camera.main.gameObject.AddComponent<FPSAim>();*/
        gm = FindObjectOfType<GameManager>().GetComponent<GameManager>();
        //npc = FindObjectOfType<Npc>().GetComponent<Npc>();
        gm.heroSlider.value = health;
        gun = FindObjectOfType<Gun>().GetComponent<Gun>();
    }

    public void OnCollisionEnter(Collision collision)                                            //Método OnCollisionEnter, compara con el cubo que colisione si es ciudadano o zombie y muestre el mesaje correspondiente.
    {
        if (collision.gameObject.GetComponent<Citizen>())
        {
            msgTime = 2f;
            touching = true;
            citizenInfo = collision.gameObject.GetComponent<Citizen>().CitizenInfo();            //Asigna la información del ciudadano para usar en el mensaje.
            humanoidInfo = collision.gameObject.GetComponent<Citizen>().HumanoidInfo();          //Asigna la información del ciudadano alojada en la estructura humanoid para usarn en mensaje.
            //gm.citizenMsg.transform.SetParent(collision.gameObject.GetComponent<Citizen>().transform);
            //gm.citizenMsg.transform.localPosition = collision.gameObject.GetComponent<Citizen>().transform.up;
            gm.citizenMsg.SetActive(true);
            gm.citizenTextMsg.text = "Hola soy " + citizenInfo.name + " y tengo " + humanoidInfo.age + " años";   //Mensaje que da el ciudadano al entrar en contacto.
        }

        if (collision.gameObject.GetComponent<Zombie>() || collision.gameObject.GetComponent<Ghost>())              //Si colisiona con algún enemigo disminuye vida al heroe.
        {
            health -= 10f;
            gm.heroSlider.value = health;
            if (health <= 0f)
                GameOver();
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<Heal>())                                                                  //Al entrar en contacto con la caja de salud regenera algo de salud al heroe
        {
            if(health < 100)
            {
                health += 20f;
                gm.heroSlider.value = health;
            }
        }

        if (other.gameObject.GetComponent<Ammo>())                                                                  //Al entrar en contacto con la caja de munición recupera algo de munición.
        {
            if (gun.ammo < 20)
            {
                gun.ammo += 10;
                gm.ammo.text = gun.ammo.ToString() + "/20"; 
            }
        }
    }

    void Update()                                                                                       //Contiene toda la parte de UI, se activa dependiendo la situación.
    {
        //gm.citizenMsg.GetComponent<Transform>().LookAt(transform);
        //gm.zombieMsg.GetComponent<Transform>().LookAt(transform);
        if (touching == true)
            msgTime -= Time.deltaTime;
        if (msgTime < 0)
        {
            gm.citizenMsg.SetActive(false);
            gm.zombieMsg.SetActive(false);
        }

        foreach (GameObject go in GameManager.npc)
        {
            if (go.GetComponent<Zombie>())
            {
                dist = Vector3.Distance(go.transform.position, transform.position);
                if (dist <= 5f)
                {
                    ZombieMsg(go);
                }
            }
        }

        if (health >= 100)
            health = 100f;
        if (gun.ammo >= 20)
            gun.ammo = 20;
    }

    public void ZombieMsg(GameObject go)
    {
        if (isAlive)
        {
            msgTime = 2f;
            touching = true;
            //gm.zombieMsg.transform.SetParent(go.gameObject.GetComponent<Zombie>().transform);
            //gm.zombieMsg.transform.localPosition = go.GetComponent<Zombie>().transform.up;
            zombieInfo = go.GetComponent<Zombie>().ZombieInfo();                                //Asigna la información del zombie para usar en el mensaje.
            gm.zombieMsg.SetActive(true);
            gm.zombieTextMsg.text = "Waaaarrrr quiero comer " + zombieInfo.taste;               //Mensaje que da el zombie al entrar en el rango asignado.
        }
        else return;
    }

    public void GameOver()
    {
        gm.citizenMsg.SetActive(false);
        gm.zombieMsg.SetActive(false);
        gameObject.GetComponent<TPSMove>().enabled = false;
        gameObject.GetComponent<Hero>().enabled = false;
    }
}

public class MovSpeed                                                                                   //Constructor para asignar la velocidad de moviento del heroe al azar por medio de una variable flotante de solo lectura.
{
    public readonly float movSpeed;
    public MovSpeed()
    {
        movSpeed = Random.Range(4.0f, 5.0f);
    }
}