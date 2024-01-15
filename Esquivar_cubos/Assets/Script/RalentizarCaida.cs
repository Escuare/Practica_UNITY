using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RalentizarCaida : MonoBehaviour
{
    public float ralentizacion = 0.5f;
    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        //SI ESTÁ CAYENDO UN OBJETO
        if (rb.velocity.y < 0)
        {
            Ralentizar();
        }
    }

    void Ralentizar()
    {
        //SE CREA UNA FUERZA CONTRARIA
        Vector3 fuerzaContraria = Vector3.up * ralentizacion;
        rb.AddForce(fuerzaContraria, ForceMode.Force);
    }
}
