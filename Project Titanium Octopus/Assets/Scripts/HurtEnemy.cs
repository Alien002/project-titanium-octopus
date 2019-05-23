using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HurtEnemy : MonoBehaviour
{   // attach to bullet
    public int BulletDamage;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // might need checking to make sure it register hit correctly
    void OnTriggerEnter(Collider collider)
    {   //standard damage
        if (collider.gameObject.name == "enemy")
        {
            //gameDirector.GetComponent<GameDirector>().enemyKilled();
            collider.gameObject.GetComponent<EnemyHealth>().DamageEnemy(BulletDamage);
        }
        //instant kill when hit core *might need checking*
        if (collider.gameObject.name == "enemy_core")
        {
            collider.gameObject.GetComponent<EnemyCore>().KillCore();
            collider.gameObject.GetComponent<EnemyHealth>().KillEnemy();
        }
    }
}
