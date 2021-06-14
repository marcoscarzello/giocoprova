using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 

public class VirusAttack : MonoBehaviour
{
    public Image virus;
    public static bool spawnAllowed;
    public GameObject panel;

    void Start()
    {
        spawnAllowed = false;


        for (int i = 0; i < 3; i++) {

            SpawnVirus();
        }


        InvokeRepeating(nameof(SpawnVirus), 0f, Random.Range(0.4f, 0.8f));
    }

    // Update is called once per frame
    void Update()
    {
    }

    void SpawnVirus()
    {
        if (spawnAllowed)
        {
            float xPos = Random.Range(0, Screen.width);
            float yPos = Random.Range(0, Screen.height);
            //float xPos = Random.Range(0, 1920);
            //float yPos = Random.Range(0, 1080);
            Vector3 spawnPosition = new Vector3(xPos, yPos, 0f);
            Image imgSpawned = Instantiate(virus, spawnPosition, Quaternion.identity);
            imgSpawned.transform.SetParent(panel.transform);
            imgSpawned.transform.position = spawnPosition;
            imgSpawned.transform.localScale = new Vector3(1f, 5f, 5f);
        }
    }


}
