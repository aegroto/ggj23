using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ViewHealth : MonoBehaviour
{
    [SerializeField] Text Kp_Display;
    private int totLife = 0;
    private PlayerStats stats;

    void Start()
    {
        stats = FindObjectOfType(typeof(PlayerStats)) as PlayerStats;
        totLife = stats.getHealth();
    }

    void Update()
    {
        totLife = stats.getHealth();
        Kp_Display.text = totLife.ToString();
    }
}
