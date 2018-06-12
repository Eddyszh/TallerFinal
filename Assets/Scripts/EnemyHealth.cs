using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    int currentHealth = 3;

    public void TakeDamage(int amount, Vector3 hitPoint)
    {
        currentHealth -= amount;

        if (currentHealth <= 0)
            gameObject.SetActive(false);
    }
}
