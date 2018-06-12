using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    int gunDamage = 1;
    float fireRate = 0.25f;
    float weaponRange = 50f;
    public int ammo = 20;
    float timer;
    Ray shootRay;
    RaycastHit shootHit;
    int shootableMask;
    LineRenderer gunLine;
    float effectTime = 0.2f;

    GameManager gm;

	// Use this for initialization
	void Awake ()
    {
        shootableMask = LayerMask.GetMask("Shootable");
        gunLine = GetComponent<LineRenderer>();
        gm = FindObjectOfType<GameManager>().GetComponent<GameManager>();
        gm.ammo.text = ammo.ToString() + "/20";
	}
	
	// Update is called once per frame
	void Update ()
    {
        timer += Time.deltaTime;
        if (Input.GetButtonDown("Fire1") && timer >= fireRate && ammo > 0 && Time.timeScale == 1.0f)
            Shoot();

        if (timer >= fireRate * effectTime)
            DisableEffect();
	}

    public void DisableEffect()
    {
        gunLine.enabled = false;
    }

    void Shoot()
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

            gunLine.SetPosition(1, transform.position);
        }
        else
            gunLine.SetPosition(1, shootRay.origin + shootRay.direction * weaponRange);

        gm.ammo.text = ammo.ToString() + "/20";
    }
}
