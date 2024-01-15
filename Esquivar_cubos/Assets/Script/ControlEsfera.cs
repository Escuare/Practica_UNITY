using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ControlEsfera : MonoBehaviour
{
    [Header("Movimiento")]
    public float movimientoHorizontal;
    public float velocidad = 10f;
    private float limiteX = 10f;

    [Header("Vida")]
    public float vidas = 3;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //MOVIMIENTO//
        movimientoHorizontal = Input.GetAxis("Horizontal");
        transform.Translate(Vector3.right * movimientoHorizontal * velocidad * Time.deltaTime);

        //LÍMITE//
        if(transform.position.x > limiteX) 
            transform.position = new Vector3(limiteX, transform.position.y, transform.position.z);
        if (transform.position.x < -limiteX)
            transform.position = new Vector3(-limiteX, transform.position.y, transform.position.z);
    }

    public void perderVida()
    {
        vidas -= 1;
        if (vidas > 0)
        {
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
        if(vidas >= 5)
        {
            vidas += 1;
            GameObject.Find("DatosJuego").GetComponent<CrearCubos>().actualizarVidas(vidas);
        }
    }

    //COLISIONES//
    private void OnCollisionEnter(Collision collision)
    {
        //CHOQUE CON CUBO//
        if (collision.gameObject.CompareTag("Cubo"))
        {
            Destroy(collision.gameObject);
            perderVida();
        }

        if (collision.gameObject.CompareTag("Vida"))
        {
            Destroy(collision.gameObject);
            ganarVida();
        }
    }


}
