using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cambioEstados : MonoBehaviour
{
    public GameObject prefabAbonar;
    public GameObject prefabBien;
    public GameObject prefabNecesitaRegar;

    public EstadoPlanta estadoInicial; // Estado inicial de la planta
    private EstadoPlanta estadoActual; // Estado actual de la planta

    private GameObject plantaActual; // Referencia al prefab activo actualmente

    public enum EstadoPlanta
    {
        Abonar,
        Bien,
        NecesitaRegar
    }

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

        // Detecta la interacción del jugador y cambia el estado
        if (Input.GetKeyDown(KeyCode.B) && estadoActual == EstadoPlanta.Abonar)
        {
            // Realiza acciones para el estado de Abonar
            Debug.Log("Abonando la planta");
            CambiarEstado(EstadoPlanta.Bien);
        }
        else if (Input.GetKeyDown(KeyCode.R) && estadoActual == EstadoPlanta.NecesitaRegar)
        {
            // Realiza acciones para el estado de NecesitaRegar
            Debug.Log("Regando la planta");
            CambiarEstado(EstadoPlanta.Bien);
        }
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
    }
}
