using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ViewAcorn : MonoBehaviour
{
    [SerializeField] Text Kp_Display;
    private int totAcorn = 0;
    private PlayerStats stats;

    void Start()
    {
        stats = FindObjectOfType(typeof(PlayerStats)) as PlayerStats;
        totAcorn = stats.getScore();
    }

    void Update()
    {
        totAcorn = stats.getScore();
        Kp_Display.text = totAcorn.ToString();
    }
}
