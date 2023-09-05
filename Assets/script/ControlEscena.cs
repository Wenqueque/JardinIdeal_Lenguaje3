using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CambioDeEscena : MonoBehaviour
{
    void Update()
    {
       // Buscar objetos con la etiqueta "marchito" en la escena
        GameObject[] objetosbien = GameObject.FindGameObjectsWithTag ("bien");
                if (objetosbien.Length >= 5)
        {
                {
                    // Cambia a la Escena 2
                    SceneManager.LoadScene("circulo2");
                }
            }
        }
    }





