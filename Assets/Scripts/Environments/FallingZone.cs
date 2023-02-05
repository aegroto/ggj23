using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FallingZone : MonoBehaviour
{
    [SerializeField] int fallingDamage = 25;
    private PlayerStats stats;
    void Start()
    {
        stats = FindObjectOfType(typeof(PlayerStats)) as PlayerStats;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            stats.AddDamage(25);
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}
