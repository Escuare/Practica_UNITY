using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ControlEsfera : MonoBehaviour
{
    [Header("Movimiento")]
    public float movimientoHorizontal;
    public float movimientoVertical;
    public float velocidad = 10f;
    private float limiteX = 10f;
    private float limiteY = 4f;

    [Header("Vida")]
    public int vidas = 3;

    [Header("Material")]
    public Material[] nuevoMaterial;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //MOVIMIENTO//
        movimientoHorizontal = Input.GetAxis("Horizontal");
        transform.Translate(Vector3.right * movimientoHorizontal * velocidad * Time.deltaTime);
        movimientoVertical = Input.GetAxis("Vertical");
        transform.Translate(Vector3.up * movimientoVertical * velocidad * Time.deltaTime);

        //LÍMITE//
        if(transform.position.x > limiteX) 
            transform.position = new Vector3(limiteX, transform.position.y, transform.position.z);
        if (transform.position.x < -limiteX)
            transform.position = new Vector3(-limiteX, transform.position.y, transform.position.z);
        if(transform.position.y > limiteY)
            transform.position = new Vector3(transform.position.x, limiteY, transform.position.z);
        if (transform.position.y < -limiteY)
            transform.position = new Vector3(transform.position.x, -limiteY, transform.position.z);
    }

    public void perderVida()
    {
        vidas -= 1;
        if (vidas > 0)
        {
            if(vidas == 2)
                velocidad = 6f;
            if(vidas == 1)
                velocidad = 4f;

            cambiarColorEsfera();

            GameObject.Find("DatosJuego").GetComponent<CrearCubos>().actualizarVidas(vidas);
        }
        else
        {
            //GUARDA LA VARIABLE DE LOS PUNTOS AL MORIR PARA ENSEÑARLO EN LOS CRÉDITOS
            PlayerPrefs.SetFloat("PuntosFinales", GameObject.Find("DatosJuego").GetComponent<CrearCubos>().puntos);
            SceneManager.LoadScene("Creditos");   //REINICIO DE LA ESCENA
        }
    }

    public void ganarVida()
    {
        if(vidas <= 5)
        {
            vidas += 1;
            cambiarColorEsfera();

            if (vidas == 2)
                velocidad = 6f;
            if (vidas >= 3)
                velocidad = 10f;

            GameObject.Find("DatosJuego").GetComponent<CrearCubos>().actualizarVidas(vidas);
        }
    }

    void cambiarColorEsfera()
    {
        Renderer rend = GetComponent<Renderer>();
        rend.material = nuevoMaterial[vidas-1];
    }

    //COLISIONES//

    private void OnTriggerEnter(Collider other)
    {
        //CHOQUE CON CUBO//
        if (other.gameObject.CompareTag("Cubo"))
        {
            Destroy(other.gameObject);
            perderVida();
        }

        if (other.gameObject.CompareTag("Cubo2"))
        {
            Destroy(other.gameObject);
            GameObject.Find("DatosJuego").GetComponent<CrearCubos>().actualizarPuntos(30);
            //SI CHOCA CON UN CUBO ROSA SUMA 30 PUNTOS
        }

        if (other.gameObject.CompareTag("Vida"))
        {
            Destroy(other.gameObject);
            ganarVida();
        }
    }


}
