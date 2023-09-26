using UnityEngine;

public class EstadosBalanzas : MonoBehaviour
{
    public GameObject objeto1;
    public GameObject objeto2;

    void Update()
    {
        // Encuentra todos los GameObjects con los tags "Marchito" y "Bien"
        GameObject[] objetosMarchitos = GameObject.FindGameObjectsWithTag("Marchito");

        // Verifica si hay 6 o más objetos con los tags "Marchito" y "Bien" en total
        //if (objetosMarchitos.Length >= 5 || objetosBien.Length >= 5)
        if (objetosMarchitos.Length <= 5)
        {
            // Activa el objeto1 y desactiva el objeto2
            objeto1.SetActive(true);
            objeto2.SetActive(false);
        }
        else
        {
            // Desactiva el objeto1 y activa el objeto2
            objeto1.SetActive(false);
            objeto2.SetActive(true);
            AudioManagerSingleton.Instance.PlaySound(9); // 0 es el índice del sonido que deseas 
        }
    }
}
