using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PasarNivel2A3 : MonoBehaviour
{
    public string sceneName; // El nombre de la escena a la que quieres cambiar.

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player")) // Puedes cambiar "Player" por la etiqueta del objeto con el que quieres colisionar.
        {
            SceneManager.LoadScene("circulo3"); // Carga la escena del tercer nivel
        }
    }
}
