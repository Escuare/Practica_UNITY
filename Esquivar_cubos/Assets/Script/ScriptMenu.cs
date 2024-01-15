using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScriptMenu : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void btnJugar()
    {
        SceneManager.LoadScene("JuegoPrincipal");
    }

    public void btnCreditos()
    {
        SceneManager.LoadScene("Creditos");
    }

    public void btnInfo()
    {
        SceneManager.LoadScene("Info");
    }

    public void btnMenu()
    {
        SceneManager.LoadScene("MenuPrincipal");
    }
}
