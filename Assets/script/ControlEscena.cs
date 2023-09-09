using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ControlEscena : MonoBehaviour
{
    void Update()
    {
        // Buscar objetos con la etiqueta "bien" en la escena
        GameObject[] objetosbien = GameObject.FindGameObjectsWithTag("bien");
        if (objetosbien.Length >= 6)
        {
            {
                // Cambia a la Escena 2
                SceneManager.LoadScene("circulo2");
            }
        }
    }
}