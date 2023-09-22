using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;
using UnityEngine.SceneManagement;

public class Controller : MonoBehaviour
{
    Animator karakterAnim;

    public GameObject bitti_panel;
    public TMPro.TextMeshProUGUI toplananaltin_text;
    public TMPro.TextMeshProUGUI puan_text;


    public Transform yol_1;
    public Transform yol_2;

    public Rigidbody rb;

    bool sagMi = false;
    public int puan;
    int toplananaltin = 0;

    public bool miknatisalindi = false;

    public AudioSource kosma_ses_dosyasi;
    public AudioSource altin_temas_sesi;
    public AudioSource cigne_sesi;
    public AudioSource gameoversound;
        

    public GameObject startimage;
    public float moveSpeed = 3;
    private bool touched;
    private Vector2 touchStartPos;
    bool playerDead = false; // Oyuncu öldü mü?




    private void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.name == "son_1")
        {
            yol_2.position = new Vector3(yol_1.position.x + 50f, yol_1.position.y, yol_1.position.z );
        }
        if (other.gameObject.name == "son_2")
        {
            UnityEngine.Debug.Log("2.yol");
            yol_1.position = new Vector3(yol_2.position.x + 50f, yol_2.position.y, yol_2.position.z);
        }

 


        if (other.gameObject.tag == "barier")
        {
            kosma_ses_dosyasi.Stop();
            gameoversound.Play();
            UnityEngine.Debug.Log("çarptý");

            bitti_panel.SetActive(true);
            Time.timeScale = 0.0f;

            playerDead = true; 

            // Oyunu kapatmayý baþlat (5 saniye sonra)
            Invoke("QuitGame", 5.0f);
        }
        if (other.gameObject.tag == "engelkay")
        {
            kosma_ses_dosyasi.Stop();
            gameoversound.Play();
            UnityEngine.Debug.Log("çarptý");

            bitti_panel.SetActive(true);
            Time.timeScale = 0.0f;

            playerDead = true;

            // Oyunu kapatmayý baþlat (5 saniye sonra)
            Invoke("QuitGame", 5.0f);
        }
        if (other.gameObject.tag == "engelzipla")
        {
            kosma_ses_dosyasi.Stop();
            gameoversound.Play();
            UnityEngine.Debug.Log("çarptý");

            bitti_panel.SetActive(true);
            Time.timeScale = 0.0f;

            playerDead = true;

            // Oyunu kapatmayý baþlat (5 saniye sonra)
            Invoke("QuitGame", 5.0f);
        }

        if (other.gameObject.tag == "karpuz")
        {
            UnityEngine.Debug.Log("karpuzzz");
            cigne_sesi.Play();

            Destroy(other.gameObject);

            puan += 20;
            puan_text.text = puan.ToString();

        }

        if (other.gameObject.tag == "altin")
        {
            UnityEngine.Debug.Log("altinnnn");
            altin_temas_sesi.Play();


            Destroy(other.gameObject);

            toplananaltin++;

            puan += 5;

            puan_text.text = puan.ToString();
            toplananaltin_text.text = toplananaltin.ToString();
        }

       
    }

    

    private void OnCollisionStay(Collision collision)
    {
        if (gameStarted ==true)
        {
            kosma_ses_dosyasi.enabled = true;

        }
    }

    private void OnCollisionExit(Collision collision)
    {
        kosma_ses_dosyasi.enabled = false;
    }

    void Start()
    {
       startimage.SetActive(true);

    }

    bool gameStarted = false; // Deðiþkeni tanýmladýk


    public void StartGame()
    {
        startimage.SetActive(false);
        karakterAnim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();

        gameStarted = true; 

    }
    
        
    void Update()
    {
        if (gameStarted)
        {
            hareket();

        }
        if (playerDead)
        {
            Time.timeScale = 0;
            bitti_panel.SetActive(true);
            if (Input.GetMouseButtonDown(0))
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
        }


    }
    
    void hareket()
    {

        float targetX = sagMi ? 1.5f : -0.2f;
        transform.Translate(0, 0, 3 * Time.deltaTime); // Hýzý global deðiþken kullanarak ayarla
        

        if (Input.GetKeyDown(KeyCode.Space))
        {
            karakterAnim.SetBool("ziplaX", true);
            rb.AddForce(Vector3.up * 40.0f, ForceMode.Impulse);
            Invoke("ziplama_iptal", 0.5f);
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            karakterAnim.SetBool("kayX", true);
            Vector3 kaymaYonu = new Vector3(0, 1.0f, 0); // Sadece x ekseni boyunca kaydýrma
            rb.AddForce(kaymaYonu * 50f, ForceMode.Impulse);

            Invoke("kayma_iptal", 1.0f); // Kayma süresi ne kadar olacaksa ona göre ayarlayýn
        }

        if (Input.GetKeyDown(KeyCode.D))
        {
            sagMi = true;
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            sagMi = false;
        }

        if (sagMi)
        {

            transform.position = Vector3.Lerp(transform.position, new Vector3(transform.position.x, transform.position.y, 9.0f), Time.deltaTime * 1.0f);
        }
        else
        {
            transform.position = Vector3.Lerp(transform.position, new Vector3(transform.position.x, transform.position.y, 12f), Time.deltaTime * 1.0f);
        }

        transform.Translate(0, 0, 5 * Time.deltaTime);

        //-------------------------------------------------------yukarý

        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began)
            {
                touchStartPos = touch.position;
                touched = true;
            }
            else if (touch.phase == TouchPhase.Moved && touched)
            {
                Vector2 touchDelta = touch.position - touchStartPos;

                if (touchDelta.y > 25f) // Yukarý kaydýrma mesafesini ayarlayýn
                {
                    karakterAnim.SetBool("ziplaX", true);
                    rb.velocity = Vector3.up * 40.0f * Time.deltaTime;
                    Invoke("ziplama_iptal", 0.5f);
                }
            }
            else if (touch.phase == TouchPhase.Ended)
            {
                touched = false;
            }
        }
        else
        {
            touched = false;
        }
        //-------------------------------------------aþaðý
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began)
            {
                touchStartPos = touch.position;
                touched = true;
            }
            else if (touch.phase == TouchPhase.Moved && touched)
            {
                Vector2 touchDelta = touch.position - touchStartPos;

                if (touchDelta.y > 25f) // Yukarý kaydýrma mesafesini ayarlayýn
                {
                    karakterAnim.SetBool("ziplaX", true);
                    rb.velocity = Vector3.up * 40.0f * Time.deltaTime;
                    Invoke("ziplama_iptal", 0.5f);
                }
                else if (touchDelta.y < -25f) // Aþaðý kaydýrma mesafesini ayarlayýn
                {
                    karakterAnim.SetBool("kayX", true);
                    Invoke("kayma_iptal", 1.0f); // Kayma süresi ne kadar olacaksa ona göre ayarlayýn
                }
            }
            else if (touch.phase == TouchPhase.Ended)
            {
                touched = false;
            }
        }
        else
        {
            touched = false;
        }

        //---------------------------------------saða - sola

        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began)
            {
                touchStartPos = touch.position;
                touched = true;
            }
            else if (touch.phase == TouchPhase.Moved && touched)
            {
                Vector2 touchDelta = touch.position - touchStartPos;

                if (touchDelta.x > 30f) // Saða kaydýrma mesafesini ayarlayýn
                {
                    sagMi = true;
                }
                else if (touchDelta.x < -30f) // Sola kaydýrma mesafesini ayarlayýn
                {
                    sagMi = false;
                }
            }
            else if (touch.phase == TouchPhase.Ended)
            {
                touched = false;
            }
        }
        else
        {
            touched = false;
        }

        if (sagMi)
        {
            transform.position = Vector3.Lerp(transform.position, new Vector3(transform.position.x, transform.position.y, 9.0f), Time.deltaTime * 0.5f);
        }
        else
        {
            transform.position = Vector3.Lerp(transform.position, new Vector3(transform.position.x, transform.position.y, 12f), Time.deltaTime * 0.5f);
        }


    }

    void ziplama_iptal()
    {
        karakterAnim.SetBool("ziplaX", false);
    }
    void kayma_iptal()
    {
        karakterAnim.SetBool("kayX", false);
    }

   
}
