using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerStats : MonoBehaviour
{
    [SerializeField] int maxHealth;
    [SerializeField] int baseAttackDamage;
    private int score;
    [SerializeField] private int health;
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
        if (health <= 0) this.Death();
    }

    public void Death()
    {
        gameObject.GetComponentInChildren<Animator>().SetTrigger("Death");
        gameObject.GetComponent<PlayerInput>().SetPlayerState("DEAD");
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
        /*if (health <= 0)
        {
            health = 100;
            score = 0;
        }*/
        PlayerPrefs.SetInt("Score", score);
        PlayerPrefs.SetInt("Health", health);
        //Debug.Log(score + " score");
        //Debug.Log(health + " health");
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            this.AddDamage(25);
        }
    }
}
