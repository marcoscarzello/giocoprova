using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;


public class NemicoManagerLV3 : MonoBehaviour
{

    private AudioSource[] audios;


    public float vitaModuloSigla;
    public float vitaModuloLinea;
    public float vitaModuloFaccia;

    private bool primoSchermoDistrutto;
    private bool secondoSchermoDistrutto;
    private string qualeIlPrimoDistrutto; //contiene il nome del primo schermo distrutto 1, 2, 3

    private string soluzione;
    private string enemycode;

    private bool attivati;
    private int count = 0;

    public GameObject DBScriptStarter;
    [SerializeField] private Munizioni ammo;
    [SerializeField] private Particle ExplosionParticle;
    [SerializeField] private VitaEnergia aggiungi;
    [SerializeField] private Particle AT_Field;
    [SerializeField] private Particle SmokeSinistra;
    [SerializeField] private Particle SmokeDestra;
    [SerializeField] private Particle SmokeCentro;


    private float energiaVal;


    public Material[] M1Distrutti = new Material[8];
    public Material[] M2Distrutti = new Material[6];
    public Material[] M3Distrutti = new Material[4];

    void Start()
    {

        audios = GetComponents<AudioSource>();


        vitaModuloSigla = 80f;
        vitaModuloLinea = 80f;
        vitaModuloFaccia = 80f;
        attivati = false;


        primoSchermoDistrutto = false;
        secondoSchermoDistrutto = false;
        qualeIlPrimoDistrutto = "0";

        var DataBase = DBScriptStarter.GetComponent<DB_Generator>().DataBase;

        //prendo codice nemico
        enemycode = gameObject.GetComponent<LV3_Enemy_Generator>().GetEnemyCode();

        //prendo soluzione
        soluzione = Convert.ToString(DataBase.Rows.Find(enemycode)[2]);
        Debug.Log("Soluzione nemico livello 3 " + soluzione);

    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.N))
        {
            Die();
        }


    }
    private IEnumerator disattiva()
    {

        if (attivati == true && count == 1)
        {
            yield return new WaitForSeconds(4);
            attivati = false;
            AT_Field.gameObject.SetActive(false);
            count = 0;
        }

    }

    private IEnumerator disattivaSmoke()
    {
        yield return new WaitForSeconds(1);
        SmokeSinistra.gameObject.SetActive(false);
        SmokeDestra.gameObject.SetActive(false);
        SmokeCentro.gameObject.SetActive(false);
    }

    public void Colpito(float damage, string tagArma, string schermoColpito)   //riceve danno e tag con colore arma e tag schermo 
    {
        //mappo tag arma -> lettera soluzione
        switch (tagArma)
        {
            case "ArmaRossa":
                tagArma = "u";
                break;
            case "ArmaVerde":
                tagArma = "v";
                break;
            case "ArmaBlu":
                tagArma = "z";
                break;
            default:
                break;
        }
        //mappo schermocolpito -> numero soluzione
        switch (schermoColpito)
        {
            case "Schermo1":
                schermoColpito = "1";
                break;
            case "Schermo2":
                schermoColpito = "2";
                break;
            case "Schermo3":
                schermoColpito = "3";
                break;
            default:
                break;
        }

        Debug.Log(schermoColpito);
        if (soluzione.Substring(1, 1) == schermoColpito)
        {
            if (tagArma == soluzione.Substring(0, 1) && attivati == false)
            {
                if (schermoColpito == "1")
                {
                    if (vitaModuloSigla > 0)
                        vitaModuloSigla -= damage;
                }
                else if (schermoColpito == "2")
                {
                    if (vitaModuloLinea > 0)
                        vitaModuloLinea -= damage;
                }
                else if (schermoColpito == "3")
                {
                    if (vitaModuloFaccia > 0)
                        vitaModuloFaccia -= damage;
                }
                if (vitaModuloSigla <= 0)
                {
                    primoSchermoDistrutto = true;
                    qualeIlPrimoDistrutto = "1";
                    Debug.Log("LV3 - schermo 1 - distrutto");

                    char[] moduli = enemycode.ToCharArray();
                    int materialmodulo1 = gameObject.GetComponent<LV3_Enemy_Generator>().DecodeM1(Convert.ToString(moduli[1]));
                    var rend1 = this.gameObject.transform.Find("SchermoSinistro").gameObject.GetComponent<Renderer>();
                    var materials1 = rend1.materials;
                    materials1[1] = M1Distrutti[materialmodulo1];
                    rend1.materials = materials1;
                    SmokeSinistra.gameObject.SetActive(true);
                    StartCoroutine(disattivaSmoke());
                    //distrutto schermo sinistro

                }
                if (vitaModuloLinea <= 0)
                {
                    primoSchermoDistrutto = true;
                    qualeIlPrimoDistrutto = "2";
                    Debug.Log("LV3 - schermo 1 - distrutto");

                    char[] moduli = enemycode.ToCharArray();
                    int materialmodulo2 = gameObject.GetComponent<LV3_Enemy_Generator>().DecodeM2(Convert.ToString(moduli[2]));
                    var rend5 = this.gameObject.transform.Find("SchermoDestra").gameObject.GetComponent<Renderer>();
                    var materials5 = rend5.materials;
                    materials5[1] = M2Distrutti[materialmodulo2];
                    rend5.materials = materials5;
                    SmokeDestra.gameObject.SetActive(true);
                    StartCoroutine(disattivaSmoke());
                    //distrutto schermo destra
                }
                if (vitaModuloFaccia <= 0)
                {
                    primoSchermoDistrutto = true;
                    qualeIlPrimoDistrutto = "3";
                    Debug.Log("LV3 - schermo 1 - distrutto");

                    char[] moduli = enemycode.ToCharArray();
                    int materialmodulo3 = gameObject.GetComponent<LV3_Enemy_Generator>().DecodeM3(Convert.ToString(moduli[3]));
                    var rend6 = this.gameObject.transform.Find("Corpo").gameObject.transform.Find("SchermoCentrale").gameObject.GetComponent<Renderer>();
                    var materials6 = rend6.materials;
                    materials6[1] = M3Distrutti[materialmodulo3];
                    rend6.materials = materials6;
                    SmokeCentro.gameObject.SetActive(true);
                    StartCoroutine(disattivaSmoke());
                    //distrutto schermo centrale

                }
            }
            else
            {
                //penalit? per lo shooter
                Debug.Log("PENALITA'");
                AT_Field.gameObject.SetActive(true);
                attivati = true;
                count++;
                StartCoroutine(disattiva());
            }
        }



        if (soluzione.Substring(3, 1) == schermoColpito)
        {
            if (tagArma == soluzione.Substring(2, 1) && primoSchermoDistrutto && attivati == false)
            {
                if (schermoColpito == "1")
                {
                    if (vitaModuloSigla > 0)
                        vitaModuloSigla -= damage;
                }
                else if (schermoColpito == "2")
                {
                    if (vitaModuloLinea > 0)
                        vitaModuloLinea -= damage;
                }
                else if (schermoColpito == "3")
                {
                    if (vitaModuloFaccia > 0)
                        vitaModuloFaccia -= damage;
                }

                if (vitaModuloSigla <= 0 && qualeIlPrimoDistrutto != "1")
                {
                    secondoSchermoDistrutto = true;
                    Debug.Log("LV3 - schermo 2 - distrutto");

                    char[] moduli = enemycode.ToCharArray();
                    int materialmodulo1 = gameObject.GetComponent<LV3_Enemy_Generator>().DecodeM1(Convert.ToString(moduli[1]));
                    var rend1 = this.gameObject.transform.Find("SchermoSinistro").gameObject.GetComponent<Renderer>();
                    var materials1 = rend1.materials;
                    materials1[1] = M1Distrutti[materialmodulo1];
                    rend1.materials = materials1;
                    SmokeSinistra.gameObject.SetActive(true);
                    StartCoroutine(disattivaSmoke());
                    //distrutto schermo sinistro

                }
                if (vitaModuloLinea <= 0 && qualeIlPrimoDistrutto != "2")
                {
                    secondoSchermoDistrutto = true;
                    Debug.Log("LV3 - schermo 2 - distrutto");

                    char[] moduli = enemycode.ToCharArray();
                    int materialmodulo2 = gameObject.GetComponent<LV3_Enemy_Generator>().DecodeM2(Convert.ToString(moduli[2]));
                    var rend5 = this.gameObject.transform.Find("SchermoDestra").gameObject.GetComponent<Renderer>();
                    var materials5 = rend5.materials;
                    materials5[1] = M2Distrutti[materialmodulo2];
                    rend5.materials = materials5;
                    SmokeDestra.gameObject.SetActive(true);
                    StartCoroutine(disattivaSmoke());
                    //distrutto schermo destra
                }
                if (vitaModuloFaccia <= 0 && qualeIlPrimoDistrutto != "3")
                {
                    secondoSchermoDistrutto = true;
                    Debug.Log("LV3 - schermo 2 - distrutto");

                    char[] moduli = enemycode.ToCharArray();
                    int materialmodulo3 = gameObject.GetComponent<LV3_Enemy_Generator>().DecodeM3(Convert.ToString(moduli[3]));
                    var rend6 = this.gameObject.transform.Find("Corpo").gameObject.transform.Find("SchermoCentrale").gameObject.GetComponent<Renderer>();
                    var materials6 = rend6.materials;
                    materials6[1] = M3Distrutti[materialmodulo3];
                    rend6.materials = materials6;
                    SmokeCentro.gameObject.SetActive(true);
                    StartCoroutine(disattivaSmoke());
                    //distrutto schermo centrale

                }
            }
            else
            {
                //penalit? per lo shooter
                Debug.Log("PENALITA'");
                AT_Field.gameObject.SetActive(true);
                attivati = true;
                count++;
                StartCoroutine(disattiva());
            }
        }


        if (soluzione.Substring(5, 1) == schermoColpito)
        {
            if (tagArma == soluzione.Substring(4, 1) && primoSchermoDistrutto && secondoSchermoDistrutto && attivati == false)
            {

                if (schermoColpito == "1")
                {
                    if (vitaModuloSigla > 0)
                        vitaModuloSigla -= damage;
                }
                else if (schermoColpito == "2")
                {
                    if (vitaModuloLinea > 0)
                        vitaModuloLinea -= damage;
                }
                else if (schermoColpito == "3")
                {
                    if (vitaModuloFaccia > 0)
                        vitaModuloFaccia -= damage;
                }
            }
            else
            {
                //penalit? per lo shooter
                Debug.Log("PENALITA'");
                AT_Field.gameObject.SetActive(true);
                attivati = true;
                count++;
                StartCoroutine(disattiva());
            }
        }

        
        //muore
        if (vitaModuloSigla <= 0f && vitaModuloLinea <= 0f && vitaModuloFaccia <= 0f)
        {
            Die();
        }

    }
    public void SpawnAmmo()
    {
        int rnd = UnityEngine.Random.Range(0, 99);
        if (rnd <= 45)
        {
            Debug.Log("Spawna");
            Instantiate(ammo, transform.position/*new Vector3(spawnPos.position.x, -1.3f , spawnPos.position.y )*/, Quaternion.Euler(-90.0f, 0.0f, 0.0f));
        }
        else Debug.Log("NonSpawna");
    }

    public void updateEnergy()
    {
        energiaVal = 20;
        if (energiaVal + aggiungi.energia > 100)
        {
            aggiungi.energia = 100;
        }
        else aggiungi.energia = aggiungi.energia + energiaVal;
    }

    void Die()
    {

        audios[0].Play();


        updateEnergy();
        Debug.Log("ENERGIA: " + aggiungi.energia);
        DBScriptStarter.GetComponent<Enemy_Spawner>().defeatedLV2_LV3 = true;
        SpawnAmmo();
        Vector3 temp = transform.position;
        temp.y = 0.7f;
        transform.position = temp;
        Instantiate(ExplosionParticle, transform.position /*new Vector3(spawnPos.position.x, 0.0f, spawnPos.position.y)*/, Quaternion.identity);
        Destroy(gameObject);
    }
}
