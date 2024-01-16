using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CrearCubos : MonoBehaviour
{
    [Header("Cubos")]
    public GameObject Cubo;
    public GameObject CuboPositivo;
    private float porcentajeCuboPositivo = 30f;
    public float tiempoSpawn = 2f;
    public float ratioRepeticion = 2f;
    private float[] tamanyos = { 0.4f, 0.6f, 1f, 1.2f, 1.4f, 2f };

    [Header("Objetos")]
    public GameObject VidaPrefab;
    private float porcentajeVida = 50f; 

    public GameObject PuntosDoblesPrefab;
    private float porcentajePuntosDobles = 50f;
    public float tiempoRestante = 11f;

    public GameObject ExplosionPrefab;
    private float porcentajeExplosion = 30f;

    [Header("Puntuación")]
    public float puntos;
    public bool puntosDoblesBool = false;

    [Header("Textos")]
    public TextMeshPro txtPuntos;
    public TextMeshPro txtVidas;
    public TextMeshPro txtPuntosDobles;
    public TextMeshPro txtPuntosDoblesSegundos;



    // Start is called before the first frame update
    void Start()
    {
        //CREAR CUBOS//
        InvokeRepeating("spawnCubos", tiempoSpawn, ratioRepeticion);

        //CREAR CUBOS ROSAS//
        GameObject cuboPositivoRandom = setCubo(CuboPositivo);
        StartCoroutine(spawnObjetos(cuboPositivoRandom, porcentajeCuboPositivo, 4f)); //CADA 4 SEGUNDOS PUEDE APARECER UN CUBO POSITIVO, 30% POSIBILIDAD

        //CREAR VIDAS//
        StartCoroutine(spawnObjetos(VidaPrefab, porcentajeVida, 6f)); //CADA 6 SEGUNDOS PUEDE APARECER UNA VIDA, 50% POSIBILIDAD

        //CREAR PUNTOS DOBLES//
        StartCoroutine(spawnObjetos(PuntosDoblesPrefab, porcentajePuntosDobles, 5f)); //CADA 5 SEGUNDOS PUEDE APARECER UN PUNTOS DOBLES, 50% POSIBILIDAD

        //CREAR EXPLOSIÓN//
        StartCoroutine(spawnObjetos(ExplosionPrefab, porcentajeExplosion, 8f)); //CADA 8 SEGUNDOS PUEDE APARECER UNA EXPLOSION, 30% POSIBILIDAD

        //TEXTOS//
        actualizarVidas(GameObject.Find("Esfera").GetComponent<ControlEsfera>().vidas);

    }

    // Update is called once per frame
    void Update()
    {
        if (puntosDoblesBool && tiempoRestante > 0)
        {
            //TEMPORIZADOR CUENTA ATRÁS
            tiempoRestante -= Time.deltaTime;
            txtPuntosDoblesSegundos.text = Mathf.FloorToInt(tiempoRestante % 60).ToString();
        }
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
            bool exito = numRandom <= porcentaje;   //SI EL NUM RANDOM ES MENOR AL PORCENTAJE DE VIDA, APARECE UNA.
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
        if (puntosDoblesBool)
            puntos += (puntosGanados * 2);
        else
            puntos += puntosGanados;
        txtPuntos.text = puntos.ToString();
        //VELOCIDAD DE LOS CUBOS
        if(puntos%50 == 0 && tiempoSpawn > 0.2f) //CADA 50 PUNTOS IRÁ MÁS RÁPIDO, SE HACE LA SEGUNDA CONDICIONAL PARA NO DA FALLOS Y TENER UNA VELOCIDAD MÁXIMA
        {
            CancelInvoke("spawnCubos");
            tiempoSpawn -= 0.2f;
            ratioRepeticion -= 0.2f;
            InvokeRepeating("spawnCubos", tiempoSpawn, ratioRepeticion);
        }
    }

    public void actualizarTxtPuntos(bool boolEnabled)
    {
        txtPuntosDobles.gameObject.SetActive(boolEnabled);
        txtPuntosDoblesSegundos.gameObject.SetActive(boolEnabled);
        puntosDoblesBool = boolEnabled;
        if (!boolEnabled)
            tiempoRestante = 11f;
    }

  
}
