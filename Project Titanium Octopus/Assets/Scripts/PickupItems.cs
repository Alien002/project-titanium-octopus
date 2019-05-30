using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//pickup item attributes
public class PickupItems : MonoBehaviour
{
    public int HealAmt;
    public int ArmorAmt;
    public int AmmoAmt;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (collider.gameObject.name.Contains("Player"))
        {
            if (this.gameObject.name.Contains("Heal"))
            {
                PlayerHealth.HealPlayer(HealAmt);
            }
            else if (this.gameObject.name.Contains("Armor"))
            {
                /*Player Armor*/ += ArmorAmt;
            }
            else if (this.gameObject.name.Contains("Ammo"))
            {
                /*Player Ammo*/ += AmmoAmt;
            }


            Destroy(this.gameObject);
        }
    }
}
