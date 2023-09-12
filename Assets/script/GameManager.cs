using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public bool ganar = false;
    // Start is called before the first frame update
    public GameObject prefabArcosAbiertos1; // Referencia al prefab de arcos abiertos
    public GameObject prefabArcosAbiertos2; // Referencia al prefab de arcos abiertos
    public GameObject prefabArcosAbiertos3; // Referencia al prefab de arcos abiertos
    public GameObject prefabArcosAbiertos4; // Referencia al prefab de arcos abiertos
    public Transform posicionesArcos1; // Transform que define la posición y rotación del arco 1
    public Transform posicionesArcos2; // Transform que define la posición y rotación del arco 2
    public Transform posicionesArcos3; // Transform que define la posición y rotación del arco 3
    public Transform posicionesArcos4; // Transform que define la posición y rotación del arco 4

    private bool arcosYaInstanciados = false;

    private cambioEstados scriptCambioEstados; // Variable para almacenar la referencia al script "cambioEstados"

    private void Start()
    {
        // Obtener la referencia al script "cambioEstados" desde el GameObject que lo contiene
        scriptCambioEstados = GameObject.FindObjectOfType<cambioEstados>();

        if (scriptCambioEstados == null)
        {
            Debug.LogError("No se encontró el script 'cambioEstados' en la escena.");
        }

    }

    void Update()
    {
        // Buscar objetos con la etiqueta "bien" en la escena
        GameObject[] objetosBien = GameObject.FindGameObjectsWithTag("bien");

        if (objetosBien.Length >= 6 && !arcosYaInstanciados)
        {
            // Desactivar objetos con el tag "arcosCerrados"
            GameObject[] arcosCerrados = GameObject.FindGameObjectsWithTag("arcosCerrados");
            foreach (GameObject arco in arcosCerrados)
            {
                arco.SetActive(false);
            }

            // Instanciar 4 nuevos objetos con el prefab de arcos abiertos en posiciones y rotaciones específicas
            Instantiate(prefabArcosAbiertos1, posicionesArcos1.position, Quaternion.Euler(posicionesArcos1.rotation.eulerAngles));
            Instantiate(prefabArcosAbiertos2, posicionesArcos2.position, Quaternion.Euler(posicionesArcos2.rotation.eulerAngles));
            Instantiate(prefabArcosAbiertos3, posicionesArcos3.position, Quaternion.Euler(posicionesArcos3.rotation.eulerAngles));
            Instantiate(prefabArcosAbiertos4, posicionesArcos4.position, Quaternion.Euler(posicionesArcos4.rotation.eulerAngles));

            // Marcar que los arcos ya se instanciaron
            arcosYaInstanciados = true;
            ganar = true;
        }

        else if (objetosBien.Length < 6 && GameObject.FindGameObjectWithTag("SobreRegado") && GameObject.FindGameObjectWithTag("Marchito"))
        {
            //ganar = false;
            //Debug.Log("reinicio");scriptCambioEstados.interaccionesConFuente >= scriptCambioEstados.limiteInteraccionesFuente && GameObject.FindGameObjectWithTag("Marchito") != null && cambioEstados.vecesRegadas >= 1
            // Reinicia la escena actual
            Invoke("ReiniciarEscena", 5f);
        }

    }
    private void ReiniciarEscena()
    {
        // Obtiene el nombre de la escena actual
        string nombreEscenaActual = SceneManager.GetActiveScene().name;

        // Carga la escena actual nuevamente para reiniciarla
        SceneManager.LoadScene(nombreEscenaActual);
    }
}