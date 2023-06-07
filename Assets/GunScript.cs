using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunScript : MonoBehaviour
{

    public float damage = 10f;
    public float range = 200f;
    public float impactForce = 50f;
    public float fireRate = 10f;
    private float nextBullet = 0f;
    public int maxAmmo = 10;
    private int currentAmmo;
    private bool isReloading = false;
    public ParticleSystem muzzleFlash;
    public Camera fpscam;

    void start()
    {
        currentAmmo = maxAmmo;
    }


    void Update()
    {
        if (isReloading)
        {
            return;
        }
        if(currentAmmo<=0)
        {
            StartCoroutine(Reload());
            return;
        }
        if (Input.GetButton("Fire1") && Time.time >= nextBullet)
        {
            nextBullet = Time.time + 1f/fireRate;
            Shoot();
        }
    }
    IEnumerator Reload()
    {
        Debug.Log("Reloding");
        isReloading = true;
        yield return new WaitForSeconds(1f);
        currentAmmo = maxAmmo;
        isReloading = false;
        
    }
    void Shoot()
    {
        muzzleFlash.Play();
        currentAmmo--;
        RaycastHit hit;
        if (Physics.Raycast(fpscam.transform.position,fpscam.transform.forward, out hit, range))
        {
            Debug.Log(hit.transform.name);
            if(hit.rigidbody != null)
            {
                hit.rigidbody.AddForce(-hit.normal * impactForce);
            }
            Enemy enemy = hit.transform.GetComponent<Enemy>();
            if(enemy != null)
            {
                enemy.GetDamage(damage);
            }
        }
    }
}
