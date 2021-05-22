using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Gun : MonoBehaviour
{

    public float damage = 10f;
    public float range = 100f;
    public float fireRate = 3f;

    //munizioni
    public int maxAmmo = 30;
    private int currentAmmo;
    public float reloadTime = 1f;
    private bool isReloading = false;

    public Camera fpsCam;
    public ParticleSystem muzzleFlash = null;
    public GameObject impactEffect;
    public float impactForce = 30f;

    private float nextTimeToFire = 0f;

    public Animator animator;

    void Start() 
    {
        currentAmmo = maxAmmo;
    }

    //impedire che il cambio arma blocchi lo sparo
    void OnEnable() {
        isReloading = false;
        animator.SetBool("isReloading", false);
    }

    void Update()
    {
        if (isReloading) return;

        if (currentAmmo <= 0) {
            StartCoroutine(Reload());
            return;
        }

        if (Input.GetButtonDown("Fire1") && Time.time >= nextTimeToFire)
        {
            nextTimeToFire = Time.time + 1f / fireRate;
            Shoot();
        }
    }

    IEnumerator Reload() 
    {
        isReloading = true;

        animator.SetBool("isReloading", true);

        yield return new WaitForSeconds(reloadTime);
        currentAmmo = maxAmmo;
        isReloading = false;
        animator.SetBool("isReloading", false);
    }


    void Shoot() {

        muzzleFlash.Play();
        animator.SetBool("isShooting", true);
        animator.SetBool("isShooting", false);

        currentAmmo--;

        RaycastHit hit; //grazie ad out hit, hit contiene tutte le info sul colpo
        if (Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit, range)) //ritorna true se colpisce 
        {
            Nemico nemico = hit.transform.GetComponent<Nemico>();
            if (nemico != null) 
            {
                nemico.TakeDamage(damage);
            }

            if (hit.rigidbody != null)
            {
                hit.rigidbody.AddForce(-hit.normal * impactForce);
            }

            GameObject impactGO = Instantiate(impactEffect, hit.point, Quaternion.LookRotation(hit.normal));
            Destroy(impactGO, 2f);
        }
    }
}
