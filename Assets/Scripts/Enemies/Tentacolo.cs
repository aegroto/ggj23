using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class Tentacolo : MonoBehaviour
{

    
    [SerializeField]
    private Animator enemyAnim;
    [SerializeField]
    private GameObject pg;
    private bool isSpawned = false;
    private bool isPlayerInRange = false;
    private bool mustLookAtPlayer = true;
    private bool isAttacking = false;
    private float waitTime = 0.5f;
    private float reduceSpeed = 1.01f;
    private Coroutine attackCoroutine = null;

    
  
    

    // Start is called before the first frame update
    void Start()
    {
        enemyAnim = GetComponentInParent<Animator>();
        pg = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        //if (health < 0) Death();
        if (Physics.CheckSphere(transform.position, 4f, 3) && !isSpawned) {
            enemyAnim.SetTrigger("Spawn");
            
        }

        if (mustLookAtPlayer)
        {
            Vector3 tempRotation = transform.parent.rotation.eulerAngles;
            transform.parent.LookAt(pg.transform);
            transform.parent.rotation = Quaternion.Euler(tempRotation.x, transform.parent.rotation.eulerAngles.y, tempRotation.z);
        }

    }

    public void Death() {
        if (attackCoroutine != null) StopCoroutine(attackCoroutine);
        isAttacking = false;
        enemyAnim.SetTrigger("Dead");
        StartCoroutine(ReduceEnemy());
    }

    public IEnumerator ReduceEnemy ()
    {
        while(transform.parent.localScale.x >= 0.05)
        {
            Debug.Log(transform.parent.localScale);
            transform.parent.localScale /= reduceSpeed;
            yield return null;
        }
        Destroy(transform.parent.gameObject);
    }

    private void OnTriggerEnter(Collider other) {
        if (other.tag == "Player") {
            isPlayerInRange = true;
            //enemyAnim.SetTrigger("Attack");
            if (!isAttacking)
            {
                //Debug.Log("Starting attack coroutine");
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
