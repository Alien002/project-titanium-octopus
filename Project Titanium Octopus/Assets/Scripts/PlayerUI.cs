using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class PlayerUI : MonoBehaviour
{
    public int maxhealth = 100;
    public int currenthealth;
    public int maxclip = 30;
    public int reserve = 90;
    public int currentammo;
    public int ammo_used;
    public int total_ammo = 120;
    public int points = 0;
    public Text health_text;
    public Text Bullet_text;
    public Text point_text;
    // Start is called before the first frame update
    void Start()
    {
        currenthealth = maxhealth;
        currentammo = maxclip;
        ammo_used = 0;
        health_text = health_text.GetComponent<Text>();
        Bullet_text = Bullet_text.GetComponent<Text>();
        point_text = point_text.GetComponent<Text>();
        

        health_text.text = currenthealth.ToString();
        health_text.fontSize = 18;
        Bullet_text.text = currentammo.ToString() + "\\" + reserve.ToString();
        Bullet_text.fontSize = 18;
        point_text.text = "Points" + points;

    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            if (reserve != 0)
            {
                currentammo--;
                ammo_used++;
                points++;
                if (currentammo == 0)
                {
                    ammo_used = 0;
                    if (reserve <= maxclip)
                    {
                        currentammo = reserve;
                        reserve -= maxclip;
                        total_ammo -= maxclip;
                    }
                    else
                    {
                        currentammo = maxclip;
                        reserve -= maxclip;
                        total_ammo -= maxclip;
                    }

                }
            }
            else
            {
                if(currentammo > 0)
                {
                    currentammo--;

                }
            }
        }

        if (Input.GetMouseButtonDown(1))
        {
            currenthealth--;
        }

        if (Input.GetKey(KeyCode.R))
        {
            if (currentammo < 30)
            {
                if (ammo_used > total_ammo)
                {
                    currentammo = total_ammo;
                }
                else
                {
                    currentammo = maxclip;
                    reserve -= ammo_used;
                    total_ammo -= ammo_used;
                    ammo_used = 0;
                }
            }
        }
        health_text.text = currenthealth.ToString();
        health_text.fontSize = 18;
        Bullet_text.text = currentammo.ToString() + "\\" + reserve.ToString();
        Bullet_text.fontSize = 18;
        point_text.text = "Points" + points;
    }
}
