using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCore : MonoBehaviour
{
    // Start is called before the first frame update
    public int coreMaxHealth;
    public int coreCurrHealth;
    // Start is called before the first frame update
    void Start()
    {
        coreCurrHealth = coreMaxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        if (coreCurrHealth <= 0)
        {
            Destroy(this.gameObject);
        }
    }

    public void KillCore()
    {
        coreCurrHealth = 0;
    }

    public void SetMaxEnemyCore()
    {
        coreCurrHealth = coreMaxHealth;
    }
}
