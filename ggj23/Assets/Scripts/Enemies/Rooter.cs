using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rooter : MonoBehaviour
{
    [SerializeField] int timeToRoot;
    [SerializeField] int damagePerSeconds;

    private bool isRunning = false;

    private IEnumerator DoDamage(Collider other)
    {
        isRunning = true;
        for (int i = 0; i < timeToRoot; i++)
        {
            yield return new WaitForSeconds(1);
            other.GetComponent<PlayerStats>().AddDamage(damagePerSeconds);
        }
        isRunning = false;
        Destroy(gameObject);
        other.GetComponent<PlayerInput>().SetPlayerState("GROUNDED");

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !isRunning)
        {
            other.GetComponent<PlayerInput>().SetPlayerState("STUNNED");
            StartCoroutine(DoDamage(other));
        }
    }

}
