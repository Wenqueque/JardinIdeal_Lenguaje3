using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CambioEstadoFruta : MonoBehaviour
{
    public GameObject prefabAbonar;
    public GameObject prefabBien;
    public GameObject prefabRecogerFruta;

    public EstadoPlanta estadoInicial; // Estado inicial de la planta
    private EstadoPlanta estadoActual; // Estado actual de la planta

    private GameObject plantaActual; // Referencia al prefab activo actualmente

    //Mira al objetivo
    private bool _isGazedAt = false;

    //Sonido
    public AudioSource SonidoFuente;
    public AudioSource SonidoRegar;
    public AudioSource SonidoAbono;
    public AudioSource SonidoCortar;
    public AudioSource SonidoFruta;


    public enum EstadoPlanta
    {
        Abonar,
        Bien,
        RecogerFruta
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
                CambiarEstado(EstadoPlanta.RecogerFruta); // Cambia a "NecesitaRegar"
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
                if (SonidoAbono != null)
                {
                    SonidoAbono.Play();
                }
            }
            //else if (Input.GetAxis("Fruta") > 0 && estadoActual == EstadoPlanta.RecogerFruta)
            else if (Input.GetKeyDown(KeyCode.F) && estadoActual == EstadoPlanta.RecogerFruta)
            {
                // Realiza acciones para el estado de NecesitaRegar
                Debug.Log("Regando la planta");
                CambiarEstado(EstadoPlanta.Bien);
                if (SonidoFruta != null)
                {
                    SonidoFruta.Play();
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
            case EstadoPlanta.RecogerFruta:
                plantaActual = prefabRecogerFruta;
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