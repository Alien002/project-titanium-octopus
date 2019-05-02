using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GUIScript : MonoBehaviour
{
    public Texture2D crosshairImage;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnGUI()
    {
        float xMin = (Screen.width / 2) - (crosshairImage.width / 2) + 40f;
        float yMin = (Screen.height / 2) - (crosshairImage.height / 2) + 15f;
        GUI.DrawTexture(new Rect(xMin, yMin, crosshairImage.width, crosshairImage.height), crosshairImage);
    }
}
