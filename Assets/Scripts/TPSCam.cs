using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TPSCam : MonoBehaviour
{
    public Transform hero;
    Vector3 offset;

    //[Range(0.01f, 1.0f)]
    public float smooth = 5f;
    public bool lookAtPlayer = false;
    public bool rotateAround = true;

    public float rotationSpeed = 5.0f;
	// Use this for initialization
	void Start ()
    {
        offset = transform.position - hero.position;
	}
	
	// Update is called once per frame
	void FixedUpdate ()
    {
        if(rotateAround)
        {
            Quaternion camTurnAngle = Quaternion.AngleAxis(Input.GetAxis("Mouse X") * rotationSpeed, Vector3.up);
            offset = camTurnAngle * offset;
        }

        Vector3 newPos = hero.position + offset;
        transform.position = Vector3.Lerp(transform.position, newPos, smooth * Time.deltaTime);

        if (lookAtPlayer || rotateAround)
            transform.LookAt(hero);
	}
}
