using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScriptCreditos : MonoBehaviour
{
    [Header("Puntos finales")]
    private float puntoConseguidos;
    public TextMeshProUGUI txtPuntosConseguidos;

    private void Awake()
    {
        puntoConseguidos = PlayerPrefs.GetFloat("PuntosFinales");  //RECOGE LOS PUNTOS CONSEGUIDOS EN LA ESCENA DE JUEGO
        txtPuntosConseguidos.text = puntoConseguidos.ToString();
    }

    public void btnMenu()
    {
        SceneManager.LoadScene("MenuPrincipal");
    }
}
