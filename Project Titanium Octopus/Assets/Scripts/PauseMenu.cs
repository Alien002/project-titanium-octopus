using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    public bool GamePaused;
    public GameObject pauseMenuUI;
    public GameObject gameOverUI;
    public GameObject camera;
    public GameObject player;
    public Text point_text;
    public Text g_over;
    public bool gameover;
    // Start is called before the first frame update
    void Start()
    {
        GamePaused = false;
        gameover = false;
        Time.timeScale = 1f;

    }

    // Update is called once per frame
    void Update()
    {
        if(player.GetComponent<PlayerUI>().currenthealth == 0)
        {
            GameOver();
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if(GamePaused)
            {
                Resume();


            }
            else
            {
                Pausing();

            }
        }
        
    }

     public void Resume()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        GamePaused = false;
        camera.GetComponent<PlayerControllerCamera>().enabled = true;
        player.GetComponent<PlayerController>().enabled = true;
        player.GetComponent<PlayerControllerCamera>().enabled = true;


    }

    void Pausing()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        GamePaused = true;
        camera.GetComponent<PlayerControllerCamera>().enabled = false;
        player.GetComponent<PlayerControllerCamera>().enabled = false;

        player.GetComponent<PlayerController>().enabled = false;
        Cursor.visible = true;

    }

    void GameOver()
    {
        gameOverUI.SetActive(true);
        Time.timeScale = 0f;
        GamePaused = true;
        camera.GetComponent<PlayerControllerCamera>().enabled = false;
        player.GetComponent<PlayerControllerCamera>().enabled = false;
        gameover = true;
        player.GetComponent<PlayerController>().enabled = false;
        g_over.text = " GAME OVER";
        g_over.fontSize = 40;
        //point_text.text = point_text.GetComponent<PlayerUI>().point_text.text;

    }

}


