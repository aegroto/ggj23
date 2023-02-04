using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private int maxHealth = 3;
    private int health;
    [SerializeField]
    private Animator enemyAnim;

    // Start is called before the first frame update
    void Start()
    {
        health = maxHealth;
        enemyAnim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (health <= 0) Death();
            
    }

    private void Death() {
        Destroy(gameObject);
    }

    public void AddDamage(int damage) {
        health -= damage;
        //enemyAnim.SetTrigger("Hurt");
        Debug.Log(health + "hp rimanenti");
    }

    private void OnCollisionEnter(Collision collision) {
        if(collision.gameObject.tag == "Player") {
            enemyAnim.SetTrigger("Attack");
            collision.gameObject.GetComponent<PlayerStats>().AddDamage(1);
            Debug.Log("Giocatore attaccato");
        }
    }

    private void OnTriggerEnter(Collider other) {
        if(other.tag == "Player") {
            enemyAnim.SetTrigger("Spawn");
            Debug.Log("Spawnato");
        }
    }
}
