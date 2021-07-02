using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


public class GestioneParamsInRete : MonoBehaviour
{
    //IMPORTANTISSIMO: CONTROLLARE SEMPRE CHE NON SIA NULL LA ROBA CHE SI PRENDE DA QUI, PRIMA DI USARLA
    //Perch� se il client non � ancora collegato ma il server s� queste cose sono sicuramente null

    //evento di ricarica salute 
    public delegate void Cura();
    public static event Cura SaluteRicaricata;

    //evento aumenta potenza: al client ricordarsi di ritornare alla potenza normale dopo tot secondi
    public delegate void Forza();
    public static event Forza PotenzaAumentata;

    //Evento ricarica munizioni
    public delegate void Ammos();
    public static event Ammos MunizioniRicaricate;

    public Vector3 posizioneShooter;

    public Vector3 posizionelv1;
    public Vector3 posizionelv2_1;
    public Vector3 posizionelv2_2;
    public Vector3 posizionelv3;

    public int munizioniPistola;
    public int munizioniPompa;
    public int munizioniMitra;

    public List<Vector3> posizioniArmi;

    //parametri aggiornati continuamente dal client
    public float salute;
    public float energia;
    
    void Start()
    {
        
    }

    void Update()
    {
        
    }


    public void RicaricaSalute() {

        Debug.Log("Sono il server. Ricarico salute");
        //fare evento ricarica salute: mandare al client l'info e sar� esso a riaggiornare il server con la nuova salute e la nuova energia
        //controllare prima di avere energia a sufficienza. 

        if (salute < 100f && energia >= 30f)
        {
            if (SaluteRicaricata != null)
            {
                SaluteRicaricata();
            }
        }


    }
    public void AumentaPotenza()
    {

        Debug.Log("sono il server, Aumenta potenza");
        if (energia >= 60f)
        {
            if (PotenzaAumentata != null)
            {
                PotenzaAumentata();
            }
        }
    }

    public void RicaricaMunizioni()
    {

        Debug.Log("sono il server, Ricarico munizioni");
        if (energia >= 99f)
        {
            if (MunizioniRicaricate != null)
            {
                MunizioniRicaricate();
            }
        }
    }
}