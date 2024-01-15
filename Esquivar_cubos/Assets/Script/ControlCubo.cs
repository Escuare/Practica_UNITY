using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlCubo : MonoBehaviour
{
    [Header("Puntuación")]
    private float puntosPorCubo = 10;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.y < -5f) //CONSEGUIR PUNTOS
        {
            Destroy(gameObject);
            GameObject.Find("DatosJuego").GetComponent<CrearCubos>().actualizarPuntos(puntosPorCubo);
        }

    }
}
