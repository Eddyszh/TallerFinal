using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPSMove : MonoBehaviour {

    public float speed;                                                         //Variable publica que define la velocidad con la que se desplaza.   

    void Update()
    {

        if (Input.GetKey(KeyCode.W))                                            //Si, se presiona la tecla "W", ejecuta la acción.
            transform.position += transform.forward * speed;   //Modifica la posición en el eje z moviendose al frente.

        if (Input.GetKey(KeyCode.S))                                            //Si, se presiona la tecla "S", ejecuta la acción.
            transform.position -= transform.forward * speed;     //Modifica la posición en el eje z moviendose atras.

        if (Input.GetKey(KeyCode.D))                                            //Si, se presiona la tecla "D", se ejecuta la acción.
            transform.position += transform.right * speed;       //Modifica la posición en el eje x moviendose a la derecha.

        if (Input.GetKey(KeyCode.A))                                            //Si, se presiona la tecla "A", se ejecuta la acción.
            transform.position -= transform.right * speed;       //Modifica al posición en el eje x moviendose a la izquierda.
    }
}
