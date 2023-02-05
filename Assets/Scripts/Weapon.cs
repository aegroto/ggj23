using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] private int attackPower;
    [SerializeField] private float maxActiveTime;
    private float activeTime;

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Rilevata collisione dell'arma");
        if (other.CompareTag("Enemy") || other.CompareTag("Obstacle"))
        {
            Debug.Log("Enemy attacked");
            other.gameObject.GetComponent<EnemyStats>().AddDamage(attackPower);
        }
    }

    private void Update()
    {
        activeTime += Time.deltaTime;
        if (activeTime >= maxActiveTime) {
            activeTime = 0;
            gameObject.GetComponent<SphereCollider>().enabled = false;
        }
    }
}
