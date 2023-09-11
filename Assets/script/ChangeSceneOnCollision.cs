using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeSceneOnCollision : MonoBehaviour
{
    public string sceneName; // El nombre de la escena a la que quieres cambiar.

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player")) // Puedes cambiar "Player" por la etiqueta del objeto con el que quieres colisionar.
        {
            SceneManager.LoadScene("circulo2"); // Carga la escena "circulo2".
        }
    }
}
