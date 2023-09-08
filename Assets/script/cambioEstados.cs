using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CambioEstados : MonoBehaviour
{
    public GameObject prefabAbonar;
    public GameObject prefabBien;
    public GameObject prefabNecesitaRegar;

    public EstadoPlanta estadoInicial; // Estado inicial de la planta
    private EstadoPlanta estadoActual; // Estado actual de la planta

    private GameObject plantaActual; // Referencia al prefab activo actualmente
   
    //MIRA AL OBJETIVO
    private bool _isGazedAt = false;

    //CONTADOR DE REGAR ENTRE TODOS LOS OBJETOS
    private static int vecesRegadas = 0;

    //LIMITACIONES FUENTE
    private int interaccionesConFuente = 0;
    public int limiteInteraccionesFuente = 3;

    //SONIDO
    public AudioManager audioManager;

    //ESTADOS DE PLANTA
    public enum EstadoPlanta
    {
        Abonar,
        Bien,
        NecesitaRegar
    }

    //TIEMPO
    public float tiempoEnEstadoBien = 0f; // Tiempo en el estado "Bien"
    public float tiempoParaCambioBien = 10f; // Tiempo para cambiar del estado "Bien" a "NecesitaRegar"

    private void Start()
    {
        estadoActual = estadoInicial; // Inicializa el estado
        CambiarEstado(estadoActual); // Inicializa el estado visual
    }

    private void Update()
    {
        if (estadoActual == EstadoPlanta.Bien)
        {
            tiempoEnEstadoBien += Time.deltaTime;

            // Verifica si ha pasado suficiente tiempo en el estado "Bien"
            if (tiempoEnEstadoBien >= tiempoParaCambioBien)
            {
                CambiarEstado(EstadoPlanta.NecesitaRegar); // Cambia a "NecesitaRegar"
            }
        }

        // Detecta la interacción del jugador y cambia el estado solo si el puntero está mirando el objeto
        if (_isGazedAt)
        {
            //if (Input.GetAxis("Abonar") > 0 && estadoActual == EstadoPlanta.Abonar)
            if (Input.GetKeyDown(KeyCode.B) && estadoActual == EstadoPlanta.Abonar)
            {
                // Realiza acciones para el estado de Abonar
                Debug.Log("Abonando la planta");
                CambiarEstado(EstadoPlanta.Bien);
                AudioManagerSingleton.Instance.PlaySound(2); // 0 es el índice del sonido que deseas reproducir
            }
            //else if (Input.GetAxis("Regar") > 0 && estadoActual == EstadoPlanta.NecesitaRegar && vecesRegadas < 3) //JOYSTICK
            else if (Input.GetKeyDown(KeyCode.R) && estadoActual == EstadoPlanta.NecesitaRegar && vecesRegadas < 3) //TECLADO
            {
                // Realiza acciones para el estado de NecesitaRegar
                Debug.Log("Regando la planta");
                CambiarEstado(EstadoPlanta.Bien);
                vecesRegadas++; // Incrementa el contador de riegos
                AudioManagerSingleton.Instance.PlaySound(1); // 0 es el índice del sonido que deseas reproducir
            }
            //else if (Input.GetAxis("Regar") > 0 && estadoActual == EstadoPlanta.Bien && vecesRegadas < 3) //JOYSTICK
            else if (Input.GetKeyDown(KeyCode.R) && estadoActual == EstadoPlanta.Bien && vecesRegadas < 3) //TECLADO
            {
                // Realiza acciones para el estado de NecesitaRegar
                Debug.Log("SobreRegando la planta");
                CambiarEstado(EstadoPlanta.Bien);
                vecesRegadas++; // Incrementa el contador de riegos
                AudioManagerSingleton.Instance.PlaySound(1); // 0 es el índice del sonido que deseas reproducir
            }
        }

        //if (Input.GetAxis("Regar") > 0) //JOYSTICK
        if (Input.GetKeyDown(KeyCode.R)) //TECLADO 
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.CompareTag("Fuente"))
                {
                    if (interaccionesConFuente < limiteInteraccionesFuente)
                    {
                        Debug.Log("Clic en objeto con tag 'Fuente'");
                        interaccionesConFuente++; // Incrementa el contador de interacciones
                        vecesRegadas = 0; // Reinicia el contador de riegos
                        AudioManagerSingleton.Instance.PlaySound(2); // 0 es el índice del sonido que deseas reproducir
                        /*if (interaccionesConFuente >= 3)
                        {
                            _isGazedAt = false;
                        }*/
                    }
                    else
                    {
                        Debug.Log("Límite de interacciones con la fuente alcanzado.");
                    }
                }
            }
        }
    }

    // Este método se llama cuando el objeto está siendo mirado.
    public void OnPointerEnter()
    {
        _isGazedAt = true;
    }

    // Este método se llama cuando el objeto ya no está siendo mirado.
    public void OnPointerExit()
    {
        _isGazedAt = false;
    }

    private void CambiarEstado(EstadoPlanta nuevoEstado)
    {
        estadoActual = nuevoEstado;

        // Desactiva el objeto actual
        if (plantaActual != null)
        {
            plantaActual.SetActive(false);
        }

        switch (estadoActual)
        {
            case EstadoPlanta.Abonar:
                plantaActual = prefabAbonar;
                break;
            case EstadoPlanta.Bien:
                plantaActual = prefabBien;
                break;
            case EstadoPlanta.NecesitaRegar:
                plantaActual = prefabNecesitaRegar;
                break;
            default:
                break;
        }

        // Activa el objeto correspondiente al nuevo estado
        if (plantaActual != null)
        {
            plantaActual.SetActive(true);
        }

        if (estadoActual == EstadoPlanta.Bien)
        {
            tiempoEnEstadoBien = 0f; // Reinicia el temporizador al entrar en el estado "Bien"
        }

        // Si el jugador ha regado tres veces en total, desactiva la función de riego
        if (vecesRegadas >= 3)
        {
            _isGazedAt = false;
        }
    }
}