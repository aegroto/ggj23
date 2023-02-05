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
    private bool isPlayerInRange = false;
    private bool mustLookAtPlayer = true;
    private bool isAttacking = false;
    private float waitTime = 0.5f;
    private Coroutine attackCoroutine = null;

    
  
    

    // Start is called before the first frame update
    void Start()
    {
        health = maxHealth;
        enemyAnim = GetComponent<Animator>();
        pg = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        //if (health < 0) Death();
        if (Physics.CheckSphere(transform.position, 4f, 3) && !isSpawned) {
            enemyAnim.SetTrigger("Spawn");
            
        }

        if (mustLookAtPlayer) transform.LookAt(pg.transform);


    }

    private void Death() {
        if (attackCoroutine != null) StopCoroutine(attackCoroutine);
        isAttacking = false;
    }

    public void AddDamage(int damage) {
        health -= damage;
    }

    private void OnTriggerEnter(Collider other) {
        if (other.tag == "Player") {
            isPlayerInRange = true;
            //enemyAnim.SetTrigger("Attack");
            if (!isAttacking)
            {
                Debug.Log("Starting attack coroutine");
                attackCoroutine = StartCoroutine(AttackPlayer());
                isAttacking = true;
            }
            //other.GetComponent<PlayerStats>().AddDamage(50);
            
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            isPlayerInRange = false;
            //enemyAnim.SetTrigger("Attack");
            //other.GetComponent<PlayerStats>().AddDamage(50);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            //enemyAnim.SetTrigger("Attack");
            collision.gameObject.GetComponent<PlayerStats>().AddDamage(50);

        }
    }

    public IEnumerator AttackPlayer()
    {
        while (isPlayerInRange)
        {
            enemyAnim.SetTrigger("Attack");
            mustLookAtPlayer = false;
            yield return new WaitForSeconds(waitTime);
            mustLookAtPlayer = true;
            yield return new WaitForSeconds(waitTime);
        }
        isAttacking = false;
    }
}
