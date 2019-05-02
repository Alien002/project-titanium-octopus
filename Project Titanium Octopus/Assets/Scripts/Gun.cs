using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public static Gun gunObject;
    public GameObject camera;
    public GameObject bullet;
    public GameObject player;
    public float barrelLength; // In meters
    public float barrelDiameter; // In meters
    public int barrelMaterial; // Each number represents a different material like stainless steel, used for calculating friction: 0 = Stainless Steel
    public float barrelMetersPerTwist; // In meters
    float bulletSpin; // Rotations per second
    float bulletGyroscopicStabilityFactor; // Dimensionless
    float barrelSpecificHeat;// In Joules per Kelvin
    float barrelHeat; // In Kelvin
    float barrelSurfaceArea; // In meters^2
    float barrelVolume; // In meters^3
    float frictionCoefficientStatic; // Dimensionless
    float frictionCoefficientKinetic; // Dimensionless
    float explosionEnergy; // In Joules
    float explosionGasHeatEnergy; // In Joules
    float explosionUnburnedPotentialEnergy; // In Joules
    float barrelHeatEnergy; // In Joules
    float barrelFrictionEnergy; // In Joules
    float projectileEnergy; // In Joules
    float muzzleVelocity; // In meters/seconds^2
    int i = 0; // Counter to reduce shot per second

    private Vector3 pos;

    // Start is called before the first frame update
    void Start()
    {
        gunObject = this;
        // Gun barrel stats
        barrelSurfaceArea = barrelLength * barrelDiameter * Mathf.PI;
        barrelVolume = barrelLength * (Mathf.Pow((barrelDiameter / 2.0f), 2.0f)) * Mathf.PI;
        if ((barrelMaterial == 0) && (bullet.GetComponent<Bullet>().projectileMaterial == 0))
        {
            frictionCoefficientStatic = 0.53f;
            frictionCoefficientKinetic = 0.36f;
        }
        // Energy on bullet and speed calculations
        explosionEnergy = (bullet.GetComponent<Bullet>().propellantMass * bullet.GetComponent<Bullet>().propellantEnergyDensity * bullet.GetComponent<Bullet>().propellantSpecificVolume);
        projectileEnergy = explosionEnergy - (explosionGasHeatEnergy + explosionUnburnedPotentialEnergy + barrelHeatEnergy + barrelFrictionEnergy);
        muzzleVelocity = (Mathf.Pow(((2.0f * projectileEnergy) / bullet.GetComponent<Bullet>().projectileMass), 0.5f));
        bulletSpin = muzzleVelocity * barrelMetersPerTwist;
        bulletGyroscopicStabilityFactor = 0.0f;
    }

    // Update is called once per frame
    void Update()
    {
        //if (Input.GetButtonDown("Fire1"))
        {
            i++;
            // Shoot every 4 cycles
            if (i == 4)
            {
                Shoot();
                i = 0;
            }
        }
    }

    void Shoot()
    {
        // Create Bullet
        GameObject tempBullet = Instantiate(bullet);
        tempBullet.SetActive(true);
        tempBullet.GetComponent<Bullet>().enabled = true;
        Vector3 pos = bullet.GetComponent<Transform>().position;
        // Set bullet transforms
        tempBullet.GetComponent<Transform>().position = new Vector3(pos.x, pos.y, pos.z);
        tempBullet.GetComponent<Transform>().position += (1.7f * (camera.GetComponent<Transform>().forward));
        tempBullet.GetComponent<Transform>().rotation = bullet.GetComponent<Transform>().rotation;
        tempBullet.GetComponent<Transform>().localScale = new Vector3(7.5f, 7.5f, 7.5f);
        // Impart velocity to bullet
        tempBullet.GetComponent<Rigidbody>().AddForce(muzzleVelocity * 0.4f * (camera.GetComponent<Transform>().forward), ForceMode.VelocityChange);
        // tempBullet.GetComponent<Transform>().rotation = Quaternion.LookRotation(tempBullet.GetComponent<Rigidbody>().velocity);
    }
}
