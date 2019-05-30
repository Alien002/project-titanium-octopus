using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int playerMaxHealth;
    public int playerCurrHealth;
    private GameObject gameDirector;
    // Start is called before the first frame update
    void Start()
    {
        playerCurrHealth = playerMaxHealth;
        gameDirector = GameObject.Find("GameDirector");
    }

    // Update is called once per frame
    void Update()
    {
        if (playerMaxHealth <= 0)
        {
            gameDirector.GetComponent<GameDirector>().restartLevel();   // resets level on death
            Destroy(this.gameObject);
        }
    }

    public void DamagePlayer(int damage)
    {
        playerCurrHealth -= damage;
    }

    public void HealPlayer(int heal)
    {
        playerCurrHealth += heal;
    }
}
