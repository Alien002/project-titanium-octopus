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

    private float t; // Time, used for bulletCam
    private bool bulletCamReady;
    private bool shotFired; // Checks if shot is fired already, for bulletCam
    private GameObject tempCamera;
    private GameObject bulletToFollow;
    private float initDeltaTime;


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
        muzzleVelocity = 1.0f;// (Mathf.Pow(((2.0f * projectileEnergy) / bullet.GetComponent<Bullet>().projectileMass), 0.5f));
        bulletSpin = muzzleVelocity * barrelMetersPerTwist;
        bulletGyroscopicStabilityFactor = 0.0f;

        // BulletCam initializations
        bulletCamReady = false;
    }


    void Update()
    {
        if (bulletCamReady)
        {
            updateBulletCam();
        }
        else
        {
            if (Input.GetButtonDown("Fire1"))
            {
                // Update is called once per frame
                // i++;
                //if (i == 4)
                
                {
                    Shoot();
                    //  i = 0;
                    //initBulletCam();
                }
            }
        }
    }

    GameObject Shoot()
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

        return tempBullet;
    }

    void initBulletCam()
    {
        tempCamera = new GameObject();
        tempCamera.AddComponent<Camera>();

        tempCamera.GetComponent<Transform>().position = bullet.GetComponent<Transform>().position + new Vector3(1f, 0f, 0f);
        tempCamera.GetComponent<Transform>().rotation = Quaternion.LookRotation(bullet.GetComponent<Transform>().position - tempCamera.GetComponent<Transform>().position);

        tempCamera.GetComponent<Camera>().enabled = true;
        camera.GetComponent<Camera>().enabled = false;

        t = 0.0f;
        shotFired = false;
        bulletCamReady = true;
    }

    void updateBulletCam()
    {
        t += Time.deltaTime;

        if (t >= 1.0f && !shotFired)
        {
            bulletToFollow = Shoot();
            shotFired = true;
        }

        if (shotFired)
        {
            if (bulletToFollow != null)
            {
                tempCamera.GetComponent<Transform>().position = bulletToFollow.GetComponent<Transform>().position + new Vector3(1f, 0f, 0f);
                initDeltaTime = Time.fixedDeltaTime;
                //Time.timeScale = .5f;
                //Time.fixedDeltaTime = Time.timeScale * initDeltaTime;
                //print(initDeltaTime);
            }

            if (t >= 5.0f || bulletToFollow == null)
            {
                tempCamera.GetComponent<Camera>().enabled = false;
                camera.GetComponent<Camera>().enabled = true;

                GameObject toDestroy = tempCamera;
                tempCamera = null;
                Destroy(toDestroy);

                //Time.timeScale = 1.0f;
                //Time.fixedDeltaTime = .02f;

                bulletCamReady = false;
            }
        }
    }
}
