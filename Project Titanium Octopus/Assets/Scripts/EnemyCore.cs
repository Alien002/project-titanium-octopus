using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCore : MonoBehaviour
{
    // Start is called before the first frame update
    public int coreMaxHealth;
    public int coreCurrHealth;
    private GameObject gameDirector;
    // Start is called before the first frame update
    void Start()
    {
        coreCurrHealth = coreMaxHealth;
        gameDirector = GameObject.Find("GameDirector");
    }

    // Update is called once per frame
    void Update()
    {
        if (coreCurrHealth <= 0)
        {
            //Destroy(this.gameObject);
            KillCore();
        }
    }

    public void KillCore()
    {
        Destroy(this.transform.parent.gameObject);
        coreCurrHealth = 0;
        gameDirector.GetComponent<GameDirector>().enemyKilled();
    }

    public void SetMaxEnemyCore()
    {
        coreCurrHealth = coreMaxHealth;
    }

    void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.name.Contains("Bullet"))
        {
            print("headshot");
            KillCore();
        }
    }
}
