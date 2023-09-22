using UnityEngine;
using UnityEditor;

public class Kamera : MonoBehaviour
{
    public Transform takip_kup;
   
    void LateUpdate()
    {
        
        transform.position = Vector3.Lerp(transform.position, takip_kup.position, Time.deltaTime *3f);
    }
}






