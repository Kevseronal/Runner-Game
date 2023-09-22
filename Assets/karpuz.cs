using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class Karpuz : MonoBehaviour
{
    Controller karakterr;
    Transform temas_kup;

    public float donmeHizi = 90.0f;


    void Start()
    {
        karakterr = GameObject.Find("karakterr").GetComponent<Controller>();
        temas_kup = GameObject.Find("karakterr/temas_kup").transform;
    }


    void Update()
    {
        transform.Rotate(Vector3.up * donmeHizi * Time.deltaTime);


    }
}
