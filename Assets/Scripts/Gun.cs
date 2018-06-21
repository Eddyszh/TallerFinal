using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    int gunDamage = 1;                                                                                              //Daño que causa el arma.
    float fireRate = 0.25f;                                                                                         //Cadencia de disparo.
    float weaponRange = 10f;                                                                                        //Rango de alcance del arma.
    public int ammo = 20;                                                                                           //Cantidad de munición del arma.
    float timer;                                                                                                    //Temporizador para disparar.
    Ray shootRay;                                                                                                   //Representa un rayo desde el origen en una dirección asignada.
    RaycastHit shootHit;                                                                                            //Analiza cuando el raycast impacta en un objeto.
    int shootableMask;                                                                                              //
    LineRenderer gunLine;                                                                                           //Linea que se traza para indicar el efecto de disparo.
    float effectTime = 0.2f;                                                                                        //Temporizador que se encarga de desactivar la linea de render.

    GameManager gm;

	void Awake ()                                                                                                   //
    {
        shootableMask = LayerMask.GetMask("Shootable");
        gunLine = GetComponent<LineRenderer>();
        gm = FindObjectOfType<GameManager>().GetComponent<GameManager>();
        gm.ammo.text = ammo.ToString() + "/20";
	}
	
	void Update ()
    {
        timer += Time.deltaTime;
        if (Input.GetButtonDown("Fire1") && timer >= fireRate && ammo > 0 && Time.timeScale == 1.0f)
            Shoot();

        if (timer >= fireRate * effectTime)
            DisableEffect();
	}

    public void DisableEffect()                                                                         //Desactiva la linea de render.
    {
        gunLine.enabled = false;
    }

    void Shoot()                                                                                        //Método que ejecuta todo el proceso de disparo.
    {
        timer = 0f;
        ammo -= 1;
        gunLine.enabled = true;
        gunLine.SetPosition(0, transform.position);

        shootRay.origin = transform.position;
        shootRay.direction = transform.forward;

        if (Physics.Raycast(shootRay, out shootHit, weaponRange, shootableMask))
        {
            EnemyHealth enemyHealth = shootHit.collider.GetComponent<EnemyHealth>();

            if (enemyHealth != null)
                enemyHealth.TakeDamage(gunDamage, shootHit.point);

            gunLine.SetPosition(1, shootRay.origin + shootRay.direction * weaponRange);
        }
        else
            gunLine.SetPosition(1, shootRay.origin + shootRay.direction * weaponRange);

        gm.ammo.text = ammo.ToString() + "/20";
    }
}
