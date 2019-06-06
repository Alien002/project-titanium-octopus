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
    public int armor = 0;
    public Text health_text;
    public Text Bullet_text;
    public Text point_text;
    public Text armor_text;
    public Text round_text;
    public GameObject player;
    public AudioSource reload_sound;
    public AudioSource fire_sound;

    // Start is called before the first frame update
    void Start()
    {
        reload_sound.mute = true;
        fire_sound.mute = true;
        currenthealth = 100;
        currentammo = maxclip;
        ammo_used = 0;
        armor = 15;
        health_text = health_text.GetComponent<Text>();
        Bullet_text = Bullet_text.GetComponent<Text>();
        point_text = point_text.GetComponent<Text>();
        armor_text = armor_text.GetComponent<Text>();
        round_text = round_text.GetComponent<Text>();

        health_text.text = currenthealth.ToString();
        health_text.fontSize = 24;
        Bullet_text.text = currentammo.ToString() + "\\" + reserve.ToString();
        Bullet_text.fontSize = 24;
        armor_text.text = armor.ToString();
        armor_text.fontSize = 24;
        round_text.text = "Round: ";
        round_text.fontSize = 32;
        point_text.text = "Points" + points;
        point_text.fontSize = 24;

    }

    // Update is called once per frame
    void Update()
    {

        if (player.GetComponent<PauseMenu>().gameover == false && player.GetComponent<PauseMenu>().GamePaused == false)
        {
            if (Input.GetMouseButtonDown(0))
            {
                if (reserve != 0)
                {
                    fire_sound.mute = false;

                    fire_sound.Play();
                    currentammo--;
                    total_ammo--;
                    ammo_used++;
                    points++;
                    if (currentammo == 0)
                    {
                        ammo_used = 0;
                        if (reserve <= maxclip)
                        {
                            currentammo = reserve;
                            reserve = 0;
                        }
                        else
                        {
                            currentammo = maxclip;
                            reserve -= maxclip;
                        }

                    }
                }
                else
                {
                    if (currentammo > 0)
                    {
                        fire_sound.mute = false;

                        fire_sound.Play();

                        currentammo--;
                        total_ammo--;
                        ammo_used++;
                        points++;

                    }
                }
            }
        }

        if (Input.GetMouseButtonDown(1))
        {
            if (currenthealth == 0)
            {

            }
            else
            {
                if (armor > 0)
                {
                    armor--;
                }
                else
                {
                    currenthealth--;
                }
            }
        }

        if (Input.GetKey(KeyCode.R))
        {
            if (currentammo < maxclip && reserve != 0)
            {
                reload_sound.mute = false;
                reload_sound.Play();
                if (currentammo >= total_ammo)
                {
                    currentammo = total_ammo;
                    reserve = 0;
                }
                else
                {
                    if (currentammo + reserve > maxclip)
                    {
                        currentammo = maxclip;
                        reserve -= ammo_used;
                        ammo_used = 0;
                    }
                    else
                    {
                        currentammo += reserve;
                        reserve = 0;
                        ammo_used = 0;
                    }
                    
                }
            }
        }
        health_text.text = currenthealth.ToString();
        health_text.fontSize = 40;
        Bullet_text.text = currentammo.ToString() + "\\" + reserve.ToString();
        Bullet_text.fontSize = 32;
        point_text.text = "Points: " + points;
        point_text.fontSize = 24;
        round_text.text = "Round: " + player.GetComponent<GameDirector>().round;
        round_text.fontSize = 32;
        armor_text.text = armor.ToString();
        armor_text.fontSize = 40;
    }
}
