using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public int enemyMaxHealth;
    public int enemyCurrHealth;
    public int enemyDamage;
    private GameObject gameDirector;
    // Start is called before the first frame update
    void Start()
    {
        enemyCurrHealth = enemyMaxHealth;
        gameDirector = GameObject.Find("GameDirector");
    }

    // Update is called once per frame
    void Update()
    {
        if(enemyCurrHealth <= 0)
        {
            KillEnemy();
        }
    }

    public void DamageEnemy(int damage)
    {
        enemyCurrHealth -= damage;
    }

    public void KillEnemy()
    {
        //Destroy(this.gameObject);
        Destroy(this.transform.parent.gameObject);
        gameDirector.GetComponent<GameDirector>().enemyKilled();
    }

    public void SetMaxEnemyHealth()
    {
        enemyCurrHealth = enemyMaxHealth;
    }


    void OnTriggerEnter(Collider collider)
    {
        /*if (collider.gameObject.name == ("Player"))
        {
            collider.gameObject.GetComponent<PlayerHealth>().DamagePlayer(enemyDamage);
        }*/
        print(collider.gameObject.name);
        if (collider.gameObject.name.Contains("Bullet"))
        {
            DamageEnemy(50);
        }
    }
}
