using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CrearCubos : MonoBehaviour
{
    [Header("Cubos")]
    public GameObject Cubo;
    public float tiempoSpawn = 2f;
    public float ratioRepeticion = 2f;

    [Header("Objetos")]
    public GameObject Vida;
    private float porcentajeVida = 60f;

    [Header("Puntuación")]
    public float puntos;

    [Header("Textos")]
    public TextMeshPro txtPuntos;
    public TextMeshPro txtVidas;


    // Start is called before the first frame update
    void Start()
    {
        //CREAR CUBOS//
        InvokeRepeating("spawnCubos", tiempoSpawn, ratioRepeticion);

        //CREAR VIDAS//
        StartCoroutine(spawnVida(2f));

        //TEXTOS//
        actualizarVidas(GameObject.Find("Esfera").GetComponent<ControlEsfera>().vidas);

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private Vector3 generarPosicionSpawn() //PARA GENERAR UNA POSICION AL INSTANCIAR ALGUN OBJETO
    {
        return new Vector3(Random.Range(9, -9), 10, 0);  //POSICION ALEATORIA, CON Y EN 10 SIEMPRE
    }

    void spawnCubos()
    {
        Vector3 posicionSpawn = generarPosicionSpawn();
        Instantiate(Cubo, posicionSpawn, Cubo.transform.rotation);
    }

    IEnumerator spawnVida(float tiempo)
    {
        yield return new WaitForSeconds(tiempo);
        while (true)
        {
            float numRandom = Random.Range(0f, 100f);
            bool exito = numRandom <= porcentajeVida;
            Debug.Log(exito);
            if(exito)
                Instantiate(Vida, generarPosicionSpawn(), Vida.transform.rotation);
        }
    }

    public void actualizarVidas(float vidasRestantes)
    {
        txtVidas.text = vidasRestantes.ToString();
    }

    public void actualizarPuntos(float puntosGanados)
    {
        puntos += puntosGanados;
        txtPuntos.text = puntos.ToString();
        if(puntos%50 == 0 && tiempoSpawn > 0.2f) //CADA 50 PUNTOS IRÁ MÁS RÁPIDO, SE HACE LA SEGUNDA CONDICIONAL PARA NO DA FALLOS Y TENER UNA VELOCIDAD MÁXIMA
        {
            CancelInvoke("spawnCubos");
            tiempoSpawn -= 0.2f;
            ratioRepeticion -= 0.2f;
            InvokeRepeating("spawnCubos", tiempoSpawn, ratioRepeticion);
        }
    }

  
}
