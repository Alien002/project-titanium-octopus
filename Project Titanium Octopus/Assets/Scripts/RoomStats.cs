using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomStats : MonoBehaviour
{
    public GameObject roomTracker;
    GameObject pivotLeftFront;
    GameObject pivotLeftBackA;
    GameObject pivotLeftBackB;
    GameObject pivotLeftBackC;
    GameObject pivotLeftBackD;
    public int doorAmount;
    public bool doorXPlus;
    public bool doorXMinus;
    public bool doorZPlus;
    public bool doorZMinus;
    public bool isRoom;
    public bool isDoorway;
    bool doorXPlusNeedsNewRoom;
    bool doorXMinusNeedsNewRoom;
    bool doorZPlusNeedsNewRoom;
    bool doorZMinusNeedsNewRoom;
    bool tooFar;
    int hallwayCount;
    // Start is called before the first frame update
    void Start()
    {
        pivotLeftFront = this.transform.GetChild(2).gameObject;
        if (doorXPlus == true)
        {
            doorXPlusNeedsNewRoom = true;
        }
        if (doorXMinus == true)
        {
            doorXMinusNeedsNewRoom = true;
        }
        if (doorZPlus == true)
        {
            doorZPlusNeedsNewRoom = true;
        }
        if (doorZMinus == true)
        {
            doorZMinusNeedsNewRoom = true;
        }
    }

    // Update is called once per frame
    void Update()
    {

        if (doorXPlusNeedsNewRoom == true)
        {
            GameObject a = Instantiate(roomTracker.GetComponent<Prefabs>().prefabList[(UnityEngine.Random.Range(0, roomTracker.GetComponent<Prefabs>().prefabList.Count))]);
            doorXPlusNeedsNewRoom = false;
        }
        if (doorXMinusNeedsNewRoom == true)
        {
            GameObject b = Instantiate(roomTracker.GetComponent<Prefabs>().prefabList[(UnityEngine.Random.Range(0, roomTracker.GetComponent<Prefabs>().prefabList.Count))]);
            doorXMinusNeedsNewRoom = false;
        }
        if (doorZPlusNeedsNewRoom == true)
        {
            if (tooFar == false)
            {
                GameObject c;
                if (!isDoorway && !isRoom)
                {
                    hallwayCount += 1;
                    if (hallwayCount >= 2)
                    {
                        hallwayCount = 0;
                        c = Instantiate(roomTracker.GetComponent<Prefabs>().prefabList[3]);
                    }
                    else
                    {
                        c = Instantiate(roomTracker.GetComponent<Prefabs>().prefabList[(UnityEngine.Random.Range(1, roomTracker.GetComponent<Prefabs>().prefabList.Count))]);
                    }
                }
                else if (isRoom)
                {
                    hallwayCount = 0;
                    c = Instantiate(roomTracker.GetComponent<Prefabs>().prefabList[(UnityEngine.Random.Range(1, roomTracker.GetComponent<Prefabs>().prefabList.Count - 1))]);
                }
                else
                {
                    int[] validChoices = new int[3];
                    validChoices[0] = 2;
                    validChoices[1] = 3;
                    c = Instantiate(roomTracker.GetComponent<Prefabs>().prefabList[validChoices[UnityEngine.Random.Range(0, 1)]]);
                }
                    pivotLeftBackC = c.transform.GetChild(0).gameObject;
                    Vector3 difference = pivotLeftFront.transform.position - pivotLeftBackC.transform.position;
                    if (c.name.Contains("Room"))
                    {
                        c.transform.Translate(0, difference.y, difference.z);
                        c.GetComponent<RoomStats>().roomTracker = roomTracker;
                        c.GetComponent<RoomStats>().doorZPlus = true;
                        float dist = Vector3.Distance(c.transform.position, Vector3.zero);
                        if (dist > 400)
                        {
                            c.GetComponent<RoomStats>().tooFar = true;
                        }
                    }
                    else
                    {
                        c.transform.Translate(-difference.z, difference.y, 0);
                        c.GetComponent<RoomStats>().roomTracker = roomTracker;
                        c.GetComponent<RoomStats>().doorZPlus = true;
                        float dist = Vector3.Distance(c.transform.position, Vector3.zero);
                        if (dist > 400)
                        {
                            c.GetComponent<RoomStats>().tooFar = true;
                        }
                    }
                c.GetComponent<RoomStats>().hallwayCount = hallwayCount;
            }
            else
            {
                GameObject c = Instantiate(roomTracker.GetComponent<Prefabs>().prefabList[0]);
                pivotLeftBackC = c.transform.GetChild(0).gameObject;
                Vector3 difference = pivotLeftFront.transform.position - pivotLeftBackC.transform.position;
                c.transform.Translate(difference.z, difference.y, 0);
            }
            doorZPlusNeedsNewRoom = false;
        }
        if (doorZMinusNeedsNewRoom == true)
        {
            GameObject d = Instantiate(roomTracker.GetComponent<Prefabs>().prefabList[(UnityEngine.Random.Range(0, roomTracker.GetComponent<Prefabs>().prefabList.Count))]);
            doorZMinusNeedsNewRoom = false;
        }
    }

}
