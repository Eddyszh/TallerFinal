using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NPC.Enemy;

public class Sword : MonoBehaviour
{
    int durability = 20;
	// Use this for initialization
	void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
		if(Input.GetButtonDown("Fire1"))
        {
           
        }
	}

    void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.GetComponent<Zombie>())
        {
            durability -= 1;
            Debug.Log(durability);
        }
    }
}
