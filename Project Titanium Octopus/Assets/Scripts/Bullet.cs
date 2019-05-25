using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public static Bullet bulletObject;
    public float projectileMass; // In kilograms
    public float projectileDiameter; // In meters
    public float projectileLength; // In meters
    public int projectileMaterial; // Each number represents a different material like lead and copper, used for calculating friction; 0 = FMJ Copper
    float crossSectionalArea; // In meters^2
    public float ballisticCoefficient; // Dimensionless
    public float propellantMass; // In kilograms
    public int propellantType; // Each number represents a different propellant formulations
    public float propellantEnergyDensity = 5934000000f; // In Joules per meters^3
    public float propellantSpecificVolume = (1.0f / 989.0f);
    float propellantPotentialEnergy; // In Joules
    float propellantPotentialBurnRate; // In kilograms per second
    public float specificGasConstant;
    public float airPressure; // In Pascals
    public float airTemperature; // In Kelvin
    float airDensity;
    float dynamicPressureWOVelocity;
    float dynamicPressure;
    float dragfloat;
    float dragfloatWOVelocity;
    float dragCoefficientG1 = 0.5f; // Dimensionless, using value at M1 for testing
    float dragCoefficient;
    bool initialShot;
    bool inGel = false;
    Vector3 gravity = new Vector3(0f, -9.8f, 0f);
    Vector3 drag = new Vector3(0.0f, 0f, 0f);
    Vector3 wind = new Vector3(0f, 0f, 0f);

    // Start is called before the first frame update
    void Start()
    {
        // Sets up new object
        bulletObject = this;
        initialShot = true;
        GetComponent<Rigidbody>().mass = projectileMass;
        // Calculations required for velocity
        crossSectionalArea = (Mathf.Pow((projectileDiameter / 2.0f), 2.0f)) * Mathf.PI;
        airDensity = airPressure / (specificGasConstant * airTemperature);
        dragCoefficient = (GetComponent<Rigidbody>().mass / (ballisticCoefficient * (Mathf.Pow(projectileDiameter, 2.0f)))) * 0.0014223f * dragCoefficientG1;
        dynamicPressureWOVelocity = 0.5f * airDensity;
        dragfloatWOVelocity = -crossSectionalArea * dragCoefficient;
    }

    // Update is called once per frame
    void Update()
    {
        // Moves like a bullet before hitting a surface
        if (initialShot)
        {
                dynamicPressure = dynamicPressureWOVelocity * (Mathf.Pow((GetComponent<Rigidbody>().velocity.magnitude), 2.0f));
                dragfloat = (dynamicPressure * dragfloatWOVelocity);
                drag = GetComponent<Rigidbody>().velocity.normalized * dragfloat;
                GetComponent<Rigidbody>().velocity += ((gravity + drag + wind) * Time.fixedDeltaTime);
                // Delete object if too far away or lasting too long
                if (GetComponent<Rigidbody>().position.x > 1000.0f || GetComponent<Rigidbody>().position.y > 1000.0f || GetComponent<Rigidbody>().position.z > 1000.0f)
                {
                    Destroy(gameObject);
                }
                Destroy(gameObject, 5.0f);
        }
        // Gravity is main force after hitting a surface
        else if (inGel)
            {
                if ((GetComponent<Rigidbody>().velocity.x < 0.1f) || (GetComponent<Rigidbody>().velocity.z < 0.1f))
                {
                    GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
                }
                Destroy(gameObject, 5.0f);
            }
        else
        {
            GetComponent<Rigidbody>().velocity += ((gravity + wind) * Time.fixedDeltaTime);
            Destroy(gameObject, 5.0f);
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        // RICOCHET GOES HERE
        if (initialShot)
        {
            initialShot = false;
        }
        // If it hits a wall, it should bounce horizontally and continue falling vertically
        //if (collision.gameObject.name.Contains("Wall"))
        if (collision.gameObject.name.Contains("Room"))
        {
            GetComponent<Rigidbody>().velocity = new Vector3(-GetComponent<Rigidbody>().velocity.x / 10f, GetComponent<Rigidbody>().velocity.y, -GetComponent<Rigidbody>().velocity.z / 10f);
        }
        // If it hits a floor, it should bounce vertically and continue its horizontal path
        if (collision.gameObject.name.Contains("Floor"))
        {
            GetComponent<Rigidbody>().velocity = new Vector3(GetComponent<Rigidbody>().velocity.x, -GetComponent<Rigidbody>().velocity.y / 10.0f, GetComponent<Rigidbody>().velocity.z);
            if (GetComponent<Rigidbody>().velocity.x < 0.1f)
                GetComponent<Rigidbody>().velocity = new Vector3(0.0f, GetComponent<Rigidbody>().velocity.y, GetComponent<Rigidbody>().velocity.z);
            if (GetComponent<Rigidbody>().velocity.y < 0.1f)
                GetComponent<Rigidbody>().velocity = new Vector3(GetComponent<Rigidbody>().velocity.x, 0.0f, GetComponent<Rigidbody>().velocity.z);
            if (GetComponent<Rigidbody>().velocity.z < 0.1f)
                GetComponent<Rigidbody>().velocity = new Vector3(GetComponent<Rigidbody>().velocity.x, GetComponent<Rigidbody>().velocity.y, 0.0f);
        }
    }

    void OnTriggerEnter(Collider collider)
    {
        if (initialShot)
        {
            initialShot = false;
        }
        if (collider.gameObject.name.Contains("Gel"))
        {
            if (!inGel)
            {
                inGel = true;
            }
            GetComponent<Rigidbody>().velocity = new Vector3(GetComponent<Rigidbody>().velocity.x / 100f, 0.0f, GetComponent<Rigidbody>().velocity.z / 100f);
            if (GetComponent<Rigidbody>().velocity.x < 0.1f)
                GetComponent<Rigidbody>().velocity = new Vector3(0.0f, GetComponent<Rigidbody>().velocity.y, GetComponent<Rigidbody>().velocity.z);
            if (GetComponent<Rigidbody>().velocity.z < 0.1f)
                GetComponent<Rigidbody>().velocity = new Vector3(GetComponent<Rigidbody>().velocity.x, GetComponent<Rigidbody>().velocity.y, 0.0f);
        }
    }

    void OnTriggerStay(Collider collider)
    {
        if (collider.gameObject.name.Contains("Gel"))
        {
            GetComponent<Rigidbody>().velocity = new Vector3(GetComponent<Rigidbody>().velocity.x / 100f, 0.0f, GetComponent<Rigidbody>().velocity.z / 100f);
            if (GetComponent<Rigidbody>().velocity.x < 0.1f)
                GetComponent<Rigidbody>().velocity = new Vector3(0.0f, GetComponent<Rigidbody>().velocity.y, GetComponent<Rigidbody>().velocity.z);
            if (GetComponent<Rigidbody>().velocity.z < 0.1f)
                GetComponent<Rigidbody>().velocity = new Vector3(GetComponent<Rigidbody>().velocity.x, GetComponent<Rigidbody>().velocity.y, 0.0f);
        }
    }
}