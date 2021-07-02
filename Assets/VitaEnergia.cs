using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VitaEnergia : MonoBehaviour
{
    public float salute;
    public float energia;
    public float _perHitLossBulletBlu;

    void Start()
    {
        salute = 100f;
        energia = 10f;
        _perHitLossBulletBlu = 10f;
    }

    void Update()
    {
        Debug.Log("Health: " + salute);
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.GetComponent<Bullet>() != null)
        {
            salute -= _perHitLossBulletBlu;//gameObject.GetComponent<Bullet>()._perHitLoss;
            Debug.Log("Health: " + salute);
        }
            
    }

    public void Curato() {

        salute = 100;
        energia -= 30;
        Debug.Log("Sono il client. Salute ricaricata! Grazie");
    }
}