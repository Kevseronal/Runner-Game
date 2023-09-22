using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class Klon_olustur : MonoBehaviour
{

    public GameObject altin;
    public GameObject karpuz;
    public GameObject barier;
    public GameObject engelkay;
    public GameObject engelzipla;
    

    public Transform karakterr;

    float silinmezamani = 10.0f;
    float sagZkoordinat = 12f;
    float solZkoordinat = 9f;

    public int sayac = 3;

    void Start()
    {
        
        InvokeRepeating("nesne_klonla", -2, 0.5f);
    }

    void nesne_klonla()
    {
        int rastsayi = Random.Range(0, 100);

        if (rastsayi > 0 && rastsayi < 75)
        {
            sayac++;
            klonla(altin, 1.0f);
        }

        if (rastsayi > 75 && rastsayi < 80)
        {
            sayac++;
            klonla(karpuz, 1f);
        }

        if (rastsayi>80 && rastsayi<85)
        {
            sayac++;
            klonla(engelkay, 0f);
        }

        if (rastsayi > 85 && rastsayi < 90)
        {
            sayac++;
            klonla(engelzipla, 0f);
        }

        if (rastsayi > 90 && rastsayi < 100)
        {
            sayac++;
            klonla(barier, 0.5f);
        }

    }

    void klonla(GameObject nesne, float Y_koordinat)
    {


        GameObject yeni_klon = Instantiate(nesne);

        int rastsayi = Random.Range(0, 100);
        

        if (rastsayi < 50)
        {
            yeni_klon.transform.position = new Vector3(karakterr.localPosition.x+30, Y_koordinat, sagZkoordinat);
        }
        else
        {
            yeni_klon.transform.position = new Vector3(karakterr.localPosition.x+30, Y_koordinat, solZkoordinat);
        }
        
        Destroy(yeni_klon, silinmezamani);

    }

    
}
