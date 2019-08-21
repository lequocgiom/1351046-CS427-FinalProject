using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoShoot : MonoBehaviour {

    public ShootProfile shootProfile;
    public GameObject bulletPrefabs;
    public Transform firePoint;
    

    private float totalSpread;
    private WaitForSeconds rate, interval;

    private void OnEnable()
    {
        interval = new WaitForSeconds(shootProfile.interval);
        rate = new WaitForSeconds(shootProfile.fireRate);

        if (firePoint == null)
            firePoint = transform;

        totalSpread = shootProfile.spread * shootProfile.amount;

        StartCoroutine(ShootingSequence());
    }

    private void OnDisable()
    {
        StopAllCoroutines();
    }

    IEnumerator ShootingSequence()
    {
        yield return rate;

        while (true)
        {
            float angle = 0;

            if(shootProfile.amount > 1)
            {
                for (int i = 0; i < shootProfile.amount; i++)
                {
                    angle = totalSpread * (i / (float)shootProfile.amount);
                    angle -= (totalSpread / 2f) - (shootProfile.spread / shootProfile.amount) ;

                    Shoot(angle);
                    yield return rate;
                }
            }
            

            yield return interval;

        }
    }

    void Shoot(float angle)
    {
        GameObject temp = PoolingManager.instance.UseObject(bulletPrefabs, firePoint.position, firePoint.rotation);
        temp.name = shootProfile.damage.ToString();
        temp.transform.Rotate(Vector3.up, angle);
    }
}
