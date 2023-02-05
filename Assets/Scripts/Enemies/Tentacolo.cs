using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class Tentacolo : MonoBehaviour
{

    private int maxHealth  = 3;
    private int health;
    [SerializeField]
    private Animator enemyAnim;
    [SerializeField]
    private GameObject pg;
    private bool isSpawned = false;

    
  
    

    // Start is called before the first frame update
    void Start()
    {
        health = maxHealth;
        enemyAnim = GetComponent<Animator>();
        
    }

    // Update is called once per frame
    void Update()
    {
        //if (health < 0) Death();
        if (Physics.CheckSphere(transform.position, 4f, 3) && !isSpawned) {
            enemyAnim.SetTrigger("Spawn");
            
        }

        transform.LookAt(pg.transform);


    }

    private void Death() {

    }

    public void AddDamage(int damage) {
        health -= damage;
    }

    private void OnTriggerEnter(Collider other) {
        if (other.tag == "Player") {
            enemyAnim.SetTrigger("Attack");
            other.GetComponent<PlayerStats>().AddDamage(50);
            
        }
    }

}
