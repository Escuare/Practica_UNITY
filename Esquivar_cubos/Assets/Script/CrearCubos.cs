using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CrearCubos : MonoBehaviour
{
    [Header("Cubos")]
    public GameObject Cubo;
    public GameObject CuboPositivo;
    private float porcentajeCuboPositivo = 100f;
    public float tiempoSpawn = 2f;
    public float ratioRepeticion = 2f;
    private float[] tamanyos = { 0.4f, 0.6f, 1f, 1.2f, 1.4f, 2f };

    [Header("Objetos")]
    public GameObject Vida;
    private float porcentajeVida = 50f; //50% DE QUE SALGA UNA VIDA CADA 5 SEGUNDOS

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
        StartCoroutine(spawnObjetos(Vida, porcentajeVida, 5f)); //CADA 5 SEGUNDOS PUEDE APARECER UNA VIDA, 50% POSIBILIDAD
        GameObject cuboPositivoRandom = setCubo(CuboPositivo);
        StartCoroutine(spawnObjetos(cuboPositivoRandom, porcentajeCuboPositivo, 1f)); //CADA 8 SEGUNDOS PUEDE APARECER UN CUBO POSITIVO, 30% POSIBILIDAD

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

    private GameObject setCubo(GameObject cubo)
    {
        //POSICION
        Vector3 posicionSpawn = generarPosicionSpawn();
        cubo.transform.position = posicionSpawn;
        //TAMAÑO
        int numRandom = Random.Range(0, tamanyos.Length);
        float tamanyoRandom = tamanyos[numRandom];
        Cubo.transform.localScale = new Vector3(tamanyoRandom, tamanyoRandom, tamanyoRandom);
        //ROTACION
        float randomX = Random.Range(0f, 360f);
        float randomY = Random.Range(0f, 360f);
        float randomZ = Random.Range(0f, 360f);
        Vector3 rotacionRandom = new Vector3(randomX, randomY, randomZ);
        cubo.transform.rotation = Quaternion.Euler(rotacionRandom);

        return cubo;
    }

    void spawnCubos()
    {
        GameObject cuboNegativo = setCubo(Cubo);
        //INSTANCIAR
        Instantiate(cuboNegativo);
    }

    IEnumerator spawnObjetos(GameObject objetoInstanciar, float porcentaje, float tiempo)
    {
        yield return new WaitForSeconds(tiempo);
        while (true)
        {
            float numRandom = Random.Range(0f, 100f);
            bool exito = numRandom <= porcentajeVida;   //SI EL NUM RANDOM ES MENOR AL PORCENTAJE DE VIDA, APARECE UNA.
            if(exito)
                Instantiate(objetoInstanciar, generarPosicionSpawn(), objetoInstanciar.transform.rotation);
            yield return new WaitForSeconds(tiempo);
        }
    }


    public void actualizarVidas(int vidasRestantes)
    {
        txtVidas.text = vidasRestantes.ToString();
        
    }

    public void actualizarPuntos(float puntosGanados)
    {
        puntos += puntosGanados;
        txtPuntos.text = puntos.ToString();
        //VELOCIDAD DE LOS CUBOS
        if(puntos%50 == 0 && tiempoSpawn > 0.4f) //CADA 50 PUNTOS IRÁ MÁS RÁPIDO, SE HACE LA SEGUNDA CONDICIONAL PARA NO DA FALLOS Y TENER UNA VELOCIDAD MÁXIMA
        {
            CancelInvoke("spawnCubos");
            tiempoSpawn -= 0.2f;
            ratioRepeticion -= 0.2f;
            InvokeRepeating("spawnCubos", tiempoSpawn, ratioRepeticion);
        }
    }

  
}
