using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BossEncounterManager : MonoBehaviour
{
    [SerializeField] GameObject[] enemies = new GameObject[4];

    private void Update()
    {
        if (enemies.All(item => item == null)) Debug.Log("Nemici schiattati");
    }
}
