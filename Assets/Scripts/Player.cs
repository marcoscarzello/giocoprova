﻿using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : NetworkBehaviour
{
    public GameObject canva_mappa;
    //prova variabile sincronizzata
    [SyncVar(hook = nameof(OnContatoreChange))]
    int HolaCount = 0;


    //float x = transform.position.x;
    //[SyncVar]
    //float posx = transform.position.x;
    void Start()
    {


        if (isLocalPlayer)
        {
            if (isServer)
            {
                //GameObject.Find("Camera").gameObject.transform.parent = this.transform;
            }
            else
            {
                GameObject.Find("Camera").gameObject.transform.parent = this.transform;
            }
        }
    }

    void HandleMovement()
    {
        if (isLocalPlayer)
        {
            float moveHorizontal = Input.GetAxis("Horizontal");
            float moveVertical = Input.GetAxis("Vertical");
            Vector3 movement = new Vector3(moveHorizontal * 0.1f, moveVertical * 0.1f, 0);

            transform.position = transform.position + movement;

        }
    }

    void Update()
    {
        HandleMovement();

        if (isLocalPlayer && Input.GetKeyDown(KeyCode.X))
        {
            Debug.Log("Salutando il server");
            Hola();
        }

    }
    [Command]
    void Hola()
    {
        HolaCount++;
        Debug.Log("ciao dal client");
    }

    void OnContatoreChange(int old, int nuovo) {
        Debug.Log($"avevamo{old}, adesso abbiamo {nuovo}");
    }
}
