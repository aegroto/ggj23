using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FallingZone : MonoBehaviour
{
    [SerializeField] int fallingDamage = 25;
    private PlayerStats stats;
    private bool isFalling = false;
    void Start()
    {
        stats = FindObjectOfType(typeof(PlayerStats)) as PlayerStats;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !isFalling)
        {
            isFalling = true;
            stats.AddDamage(25);
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}
