using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPSAim : MonoBehaviour {

    float mouseX;                                                       //Variable que almacena el movimiento del mouse en el eje "x".
    float mouseY;                                                       //Variable que almacena el movimiento del mouse en el eje "y".
    public bool invertedAim;                                            //Variable para configurar la mira invertida en el eje "y".

    public float yMin = -50, yMax = 50;                                 //Variable para dar límite en el eje "y".
    public int caso;                                                    //Variable para definir un "switch".

    void Start()
    {
        if (gameObject.CompareTag("MainCamera"))
            caso = 1;
        else
            caso = 2;
    }
    void Update()
    {
        switch (caso)                                                       //"Switch" que tiene dos casos, cada caso da rotacion en los ejes "x", "y".
        {
            case 1:                                                         //Primer caso, identificado con un entero de valor "1", este se asigna a la camara en el ispector.
                mouseX += Input.GetAxis("Mouse X");                         //Obtiene el movimiento del eje "x" proveniente del mouse.
                if (invertedAim)                                            //Si es verdadera, se invierte la mira en el eje "y".
                {
                    mouseY += Input.GetAxis("Mouse Y");                     //Obtiene el movimiento invertido del eje "y" proveniente del mouse.
                    mouseY = Mathf.Clamp(mouseY, yMin, yMax);               //Limita el movimiento en el eje "y".
                }
                else                                                        //Si no es verdadera la opción, la mira se mueve de manera natural en el eje "y".
                    mouseY -= Input.GetAxis("Mouse Y");                     //Obtiene el movimiento del eje "y" proveniente del mouse. 
                mouseY = Mathf.Clamp(mouseY, yMin, yMax);                   //Limita el movimiento en el eje "y".
                transform.eulerAngles = new Vector3(mouseY, mouseX, 0);     //Otorga la rotación de la camara en los ejes "x", "y" proveniente del mouse.
                break;                                                      //Finaliza el primer caso.
            case 2:                                                         //Segundo caso, identificado con un entero de valor "2", este se asigna a la cápsula en el ispector.
                mouseX += Input.GetAxis("Mouse X");                         //Obtiene el movimiento del eje "x" proveniente del mouse.
                transform.eulerAngles = new Vector3(0f, mouseX, 0f);        //Otorga la rotación de la capsula en el eje "y".
                break;                                                      //Finaliza el segundo caso.
        }
    }
}
