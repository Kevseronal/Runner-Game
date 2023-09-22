using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEditor;

public class Bitti_panel : MonoBehaviour
{
    public TMPro.TextMeshProUGUI score;
    private Controller controller;
    public GameObject deadimage;



    private void Start()
    {
        // Controller scriptine eriþim saðlayýn
        Controller controllerScript = FindObjectOfType<Controller>();

        // Controller scriptindeki puaný alarak score metnine aktar
        score.text = "Score : " + controllerScript.puan.ToString();



    }

  

}


