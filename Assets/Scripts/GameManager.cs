using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;                                                                       //Librería de interfaz de usuario.
using NPC.Ally;                                                                             //namespace ally, pertenece al namespace NPC y contiene la clase del ciudadano.
using NPC.Enemy;                                                                            //namespace enemy, pertenece al namespace NPC y contiene la clase del zombie.

public class GameManager : MonoBehaviour
{
    ZombieInformation zombieInfo;                                                           //Declaración de la estructura del zombie.
    CitizenInformation citizenInfo;                                                         //Declaración de la estructura del ciudadano.
    public static List<GameObject> npc = new List<GameObject>();                                          //Lista donde se agrega los npc existentes en la escena.
    public Text citizenText;                                                                //Texto que indica la cantidad de ciudadanos en la escena.
    public Text zombieText;                                                                 //Texto que indica la cantidad de zombies en la escena.
    public Text ghostText;                                                                  //Texto que indica la cantidad de fantasmas en la escena.
    public Text ammo;                                                                       //Texto que indica la cantidad de munición disponible.
    public Text citizenTextMsg;
    public Text zombieTextMsg;
    public GameObject citizenMsg;
    public GameObject zombieMsg;
    public Image heroDied;
    public Image success;
    public Slider heroSlider;
    GameObject humanoid;
    public GameObject[] characters = new GameObject[4];
    public const int MAX = 25;                                                                     //Variable constante que tiene la cantidad máxima de cubos a crear.
    public int citizenCount = 0;                                                                   //Contador para ciudadanos.
    public int zombieCount = 0;                                                                    //Contador para zombies.
    public int ghostCount = 0;                                                                     //Contador para fantasmas.

	void Start ()
    {
        zombieMsg.SetActive(false);
        citizenMsg.SetActive(false);
        heroDied.gameObject.SetActive(false);
        success.gameObject.SetActive(false);
        int spawn = -1;

        for (int i = 0; i < Random.Range(new MinValue().minValue, MAX); i++)                //Bucle que crea una cantidad aleatoria de primitivas con posición aleatoria.                                                 
        {
            Vector3 pos = new Vector3(Random.Range(-50, 50), 0, Random.Range(-50, 50));
            //GameObject humanoid; //= Instantiate(characters[Random.Range(1, 4)], pos, transform.rotation);
            switch (spawn)                                                                  //Asigna "personalidades" de manera aleatoria.
            {
                case 1:                                                                     //Asigna "personalidad" de ciudadano añadadiendo la clase.
                    humanoid = Instantiate(characters[1], pos, Quaternion.identity);
                    humanoid.name = "Citizen";
                    humanoid.AddComponent<Citizen>();
                    break;
                case 2:                                                                     //Asigna "personalidad" de zombie añadiendo clase y color.
                    humanoid = Instantiate(characters[2], pos, Quaternion.identity);
                    humanoid.name = "Zombie";
                    humanoid.AddComponent<Zombie>();
                    break;
                case 3:
                    humanoid = Instantiate(characters[3], pos, Quaternion.identity);
                    humanoid.name = "Ghost";
                    humanoid.AddComponent<Ghost>();
                    break;
                default:                                                                    //Asigna "personalidad" de heroe añadiendo clase y color.
                    //humanoid = Instantiate(characters[0], pos, Quaternion.identity);
                    //humanoid.name = "Hero";
                    //humanoid.AddComponent<Hero>();
                    humanoid = FindObjectOfType<Hero>().GetComponent<Hero>().gameObject;
                    humanoid.GetComponent<Renderer>().material.SetColor("_Color", Color.red);
                    break;
            }
            humanoid.transform.position = pos;
            spawn = Random.Range(1, 4);                                                     //Selecion aleatoria de "personalidad".
            if (humanoid.gameObject != null)                                                 //Condición para agregar npc a la lista.
                npc.Add(humanoid);                                                          //Agrega npc a la lista.
        }
        foreach (GameObject go in npc)                                                      //Por cada elemento en la lista, aumenta el contador de ciudadano o zombie.
        {
            if(go.gameObject.GetComponent<Citizen>())
            {
                citizenCount++;
                citizenText.text = "Citizen: " + citizenCount.ToString();
            }
            if(go.gameObject.GetComponent<Zombie>())
            {
                zombieCount++;
                zombieText.text = "Zombie: " + zombieCount.ToString();
            }
            if (go.gameObject.GetComponent<Ghost>())
            {
                ghostCount++;
                ghostText.text = "Ghost: " + ghostCount.ToString();
            }
        }
    }

    void Update()
    {
        EndGame();
    }

    void EndGame()
    {
        if (heroSlider.value == 0 && Time.timeScale == 1.0f)
        {
            heroDied.gameObject.SetActive(true);
            Time.timeScale = 0.0f;
        }

        if (zombieCount == 0 && ghostCount == 0)
        {
            success.gameObject.SetActive(true);
            Time.timeScale = 0.0f;
        }
    }
}
public class MinValue                                                                          //Constructor para obtener el valor de creación mínimo de personajes, por medio de una variable entera de solo lectura.
{
    public readonly int minValue;
    public MinValue()
    {
        minValue = Random.Range(5, 15);
    }
}

