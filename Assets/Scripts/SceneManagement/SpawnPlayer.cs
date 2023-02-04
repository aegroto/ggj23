using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPlayer : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject playerSpawnPoint;

    private void Start()
    {
        Instantiate(player, playerSpawnPoint.transform);
    }
}
