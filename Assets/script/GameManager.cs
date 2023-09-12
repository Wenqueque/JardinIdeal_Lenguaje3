using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public bool ganar = false;
    // Start is called before the first frame update
    public GameObject prefabArcosAbiertos1A1; // Referencia al prefab de arcos abiertos
    public GameObject prefabArcosAbiertos2A1; // Referencia al prefab de arcos abiertos
    public GameObject prefabArcosAbiertos3A1; // Referencia al prefab de arcos abiertos
    public GameObject prefabArcosAbiertos4A1; // Referencia al prefab de arcos abiertos
    public Transform posicionesArcos1A1; // Transform que define la posici�n y rotaci�n del arco 1
    public Transform posicionesArcos2A1; // Transform que define la posici�n y rotaci�n del arco 2
    public Transform posicionesArcos3A1; // Transform que define la posici�n y rotaci�n del arco 3
    public Transform posicionesArcos4A1; // Transform que define la posici�n y rotaci�n del arco 4


    public GameObject prefabArcosAbiertos1A2; // Referencia al prefab de arcos abiertos
    public GameObject prefabArcosAbiertos2A2; // Referencia al prefab de arcos abiertos
    public GameObject prefabArcosAbiertos3A2; // Referencia al prefab de arcos abiertos
    public GameObject prefabArcosAbiertos4A2; // Referencia al prefab de arcos abiertos
    public Transform posicionesArcos1A2; // Transform que define la posici�n y rotaci�n del arco 1
    public Transform posicionesArcos2A2; // Transform que define la posici�n y rotaci�n del arco 2
    public Transform posicionesArcos3A2; // Transform que define la posici�n y rotaci�n del arco 3
    public Transform posicionesArcos4A2; // Transform que define la posici�n y rotaci�n del arco 4


    private bool arcosYaInstanciados1 = false;
    private bool arcosYaInstanciados2 = false;

    private cambioEstados scriptCambioEstados; // Variable para almacenar la referencia al script "cambioEstados"

    private void Start()
    {
        // Obtener la referencia al script "cambioEstados" desde el GameObject que lo contiene
        scriptCambioEstados = GameObject.FindObjectOfType<cambioEstados>();

        if (scriptCambioEstados == null)
        {
            Debug.LogError("No se encontr� el script 'cambioEstados' en la escena.");
        }


    }

    private void Update()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        // Llamar a la función correspondiente para pasar al siguiente nivel
        if (currentSceneIndex == 0) // Escena 1
        {
            // Llama a la función para pasar al nivel 2
            pasarNivel2();
        } else if (currentSceneIndex == 1) // Escena 2
        {
            // Llama a la función para pasar al nivel 3
            pasarNivel3();
        }
    }

    private void ReiniciarEscena()
    {
        // Obtiene el nombre de la escena actual
        string nombreEscenaActual = SceneManager.GetActiveScene().name;

        // Carga la escena actual nuevamente para reiniciarla
        SceneManager.LoadScene(nombreEscenaActual);
    }

    private void pasarNivel2()
    {
       // Buscar objetos con la etiqueta "bien" en la escena
        GameObject[] objetosBien = GameObject.FindGameObjectsWithTag("bien");

        if (objetosBien.Length >= 6 && !arcosYaInstanciados1)
        {
            // Desactivar objetos con el tag "arcosCerrados"
            GameObject[] arcosCerrados = GameObject.FindGameObjectsWithTag("arcosCerrados");
            foreach (GameObject arco in arcosCerrados)
            {
                arco.SetActive(false);
            }

            // Instanciar 4 nuevos objetos con el prefab de arcos abiertos en posiciones y rotaciones espec�ficas
            Instantiate(prefabArcosAbiertos1A1, posicionesArcos1A1.position, Quaternion.Euler(posicionesArcos1A1.rotation.eulerAngles));
            Instantiate(prefabArcosAbiertos2A1, posicionesArcos2A1.position, Quaternion.Euler(posicionesArcos2A1.rotation.eulerAngles));
            Instantiate(prefabArcosAbiertos3A1, posicionesArcos3A1.position, Quaternion.Euler(posicionesArcos3A1.rotation.eulerAngles));
            Instantiate(prefabArcosAbiertos4A1, posicionesArcos4A1.position, Quaternion.Euler(posicionesArcos4A1.rotation.eulerAngles));

            // Marcar que los arcos ya se instanciaron
            arcosYaInstanciados1 = true;
            AudioManagerSingleton.Instance.PlaySound(8); // 0 es el índice del sonido que deseas 
            //ganar = true;
        }

        else if (objetosBien.Length < 6 && GameObject.FindGameObjectWithTag("SobreRegado") && GameObject.FindGameObjectWithTag("Marchito") && GameObject.FindGameObjectWithTag("fuenteVacia"))
        {
            //ganar = false;
            //Debug.Log("reinicio");scriptCambioEstados.interaccionesConFuente >= scriptCambioEstados.limiteInteraccionesFuente && GameObject.FindGameObjectWithTag("Marchito") != null && cambioEstados.vecesRegadas >= 1
            // Reinicia la escena actual
            Invoke("ReiniciarEscena", 5f);
        } 
    }
    private void pasarNivel3()
    {
       // Buscar objetos con la etiqueta "bien" en la escena
        GameObject[] objetosBien = GameObject.FindGameObjectsWithTag("bien"); 
        if (objetosBien.Length >= 14 && !arcosYaInstanciados2)
        {
            // Desactivar objetos con el tag "arcosCerrados"
            GameObject[] arcosCerrados = GameObject.FindGameObjectsWithTag("arcosCerrados");
            foreach (GameObject arco in arcosCerrados)
            {
                arco.SetActive(false);
            }

            // Instanciar 4 nuevos objetos con el prefab de arcos abiertos en posiciones y rotaciones espec�ficas
            Instantiate(prefabArcosAbiertos1A2, posicionesArcos1A2.position, Quaternion.Euler(posicionesArcos1A2.rotation.eulerAngles));
            Instantiate(prefabArcosAbiertos2A2, posicionesArcos2A2.position, Quaternion.Euler(posicionesArcos2A2.rotation.eulerAngles));
            Instantiate(prefabArcosAbiertos3A2, posicionesArcos3A2.position, Quaternion.Euler(posicionesArcos3A2.rotation.eulerAngles));
            Instantiate(prefabArcosAbiertos4A2, posicionesArcos4A2.position, Quaternion.Euler(posicionesArcos4A2.rotation.eulerAngles));

            // Marcar que los arcos ya se instanciaron
            arcosYaInstanciados2 = true;
            AudioManagerSingleton.Instance.PlaySound(8); // 0 es el índice del sonido que deseas 
            //ganar = true;
        }
    }
}