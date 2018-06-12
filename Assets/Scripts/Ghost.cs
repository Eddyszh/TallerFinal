using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NPC.Ally;

namespace NPC
{
    namespace Enemy
    {
        public class Ghost : Npc
        {
            GameManager gm;
            void Start()
            {
                gameObject.GetComponent<Rigidbody>().useGravity = false;
                gameObject.GetComponent<Rigidbody>().isKinematic = true;
                gm = FindObjectOfType<GameManager>().GetComponent<GameManager>();
                gameObject.AddComponent<EnemyHealth>();
                transform.position += new Vector3(0f, 0.3f, 0f);
            }

            private void OnCollisionEnter(Collision collision)                                  //Si entra en colision con el ciudadano lo convierte en zombiey modifica el contador.
            {
                if (collision.gameObject.GetComponent<Citizen>())
                {
                    Citizen c = collision.gameObject.GetComponent<Citizen>();
                    Ghost g = c;
                    print("Desde zombie " + g.humanoidInfo.age);
                    gm.citizenCount--;
                    gm.citizenText.text = "Citizen: " + gm.citizenCount.ToString();
                    gm.ghostCount++;
                    gm.ghostText.text = "Ghost: " + gm.ghostCount.ToString();
                }
            }

            void OnDisable()
            {
                gm.ghostCount--;
                gm.ghostText.text = "Ghost: " + gm.ghostCount.ToString();
            }
        }
    }
}

