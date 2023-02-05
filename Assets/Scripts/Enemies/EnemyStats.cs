using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStats : MonoBehaviour
{
    [SerializeField] Tentacolo tentacolo;
    private int maxHealth = 3;
    private int health;

    private void Start()
    {
        health = maxHealth;
    }

    public void AddDamage(int damage)
    {
        health -= damage;
        Debug.Log("Enemy health = " + health);
        if (health <= 0 && tentacolo != null) {
            tentacolo.Death();
        }
        if (gameObject.tag == "Obstacle" && health <= 0)
        {
            gameObject.SetActive(false);
            gameObject.GetComponent<BoxCollider>().enabled = false;
        }
    }


}
