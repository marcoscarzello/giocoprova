using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GestioneMinigameLabirinto : MonoBehaviour
{
   public void AvvioLabirinto() {

        gameObject.SetActive(true);
        if (GameObject.Find("Simon") != null)
        GameObject.Find("Simon").SetActive(false);
        if (GameObject.Find("Simon2") != null)
            GameObject.Find("Simon2").SetActive(false);

    }
    public void FineLabirinto()
    {

        gameObject.SetActive(false);

    }


}