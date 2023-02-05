using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerStats : MonoBehaviour
{
    [SerializeField] int maxHealth;
    [SerializeField] int baseAttackDamage;
    private int score;
    private int health;
    private int attackDamage;

    private void Start()
    {

        score = PlayerPrefs.GetInt("Score", 0);
        health = PlayerPrefs.GetInt("Health", maxHealth);
        attackDamage = baseAttackDamage;
    }

    private void OnSceneUnloaded(Scene scene)
    {
        PlayerPrefs.SetInt("Score", score);
        PlayerPrefs.SetInt("Health", health);
    }

    public void AddScore(int points)
    {
        score += points;
    }

    public void AddDamage(int damage)
    {
        health -= damage;
    }

    public void AddHealth(int health)
    {
        if (health <= maxHealth)
        {
            health += health;
        }
        
    }

    public int getHealth()
    {
        return health;
    }


    public int getScore()
    {
        return score;
    }

    public int getPlayerAttackPower()
    {
        return attackDamage;
    }

    private void Update()
    {
        if (health <= 0)
        {
            health = 100;
            score = 0;
        }
        PlayerPrefs.SetInt("Score", score);
        PlayerPrefs.SetInt("Health", health);
        //Debug.Log(score + " score");
        //Debug.Log(health + " health");
    }
}
