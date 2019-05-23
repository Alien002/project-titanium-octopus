﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{
    public BoxCollider boxCol;

    // Start is called before the first frame update
    void Start()
    {
        getCollider();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void getCollider()
    {
        Bounds bounds = new Bounds(Vector3.zero, Vector3.zero);
        //Renderer thisRenderer = transform.GetComponent<Renderer>();
        // bounds.Encapsulate(thisRenderer.bounds);
        //boxCol.offset = bounds.center - transform.position;
        //boxCol.size = bounds.size;

        //Transform floor = this.GetComponent<Transform>();
        bounds = this.GetComponent<MeshCollider>().bounds;

        boxCol = this.gameObject.AddComponent<BoxCollider>();
        boxCol.center = bounds.center + new Vector3(0f, -2f, 0f);
        boxCol.size = bounds.size;

        /*if (floor)
        {
            boxCol = this.gameObject.AddComponent<BoxCollider>();

            boxCol.isTrigger = true;

            //print(fl.position);
            boxCol.center = floor.localPosition;
            boxCol.size = new Vector3(floor.localScale.x, 10.0f, floor.localScale.z);
        }*/
        /*Transform[] allDescendants = gameObject.GetComponentsInChildren<Transform>();
        foreach (Transform desc in allDescendants)
        {
            if (desc.gameObject.name == "Floor")
            {
                Renderer childRenderer = desc.GetComponent<Renderer>();
                // NOTE: Set bounding box to floor
                bounds.Encapsulate(childRenderer.bounds);
                boxCol.center = desc.position;
                boxCol.size = desc.lossyScale;
                //print(desc.position);
                //print(desc.lossyScale);
            }
            //boxCol.offset = bounds.center - transform.position;
           
        }*/
    }
}
