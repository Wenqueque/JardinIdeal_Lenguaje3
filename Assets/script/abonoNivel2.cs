using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class abonoNivel2 : MonoBehaviour
{
    public GameObject prefabAbonar;
    public GameObject prefabBien;

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
    }

    public float tiempoEnEstadoBien = 0f; // Tiempo en el estado "Bien"
    public float tiempoParaCambioBien = 70f; // Tiempo para cambiar del estado "Bien" a "abonar"

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
                CambiarEstado(EstadoPlanta.Abonar); // Cambia a "abonar"
            }
        }

        // Detecta la interacci�n del jugador y cambia el estado solo si el puntero est� mirando el objeto
        if (_isGazedAt)
        {
            if (Input.GetAxis("Cortar") > 0 && estadoActual == EstadoPlanta.Abonar)
            //if (Input.GetKeyDown(KeyCode.E) && estadoActual == EstadoPlanta.Abonar)
            {
                // Realiza acciones para el estado de Abonar
                Debug.Log("Abonando la planta");
                CambiarEstado(EstadoPlanta.Bien);
                if (SonidoAbono != null)
                {
                    SonidoAbono.Play();
                    AudioManagerSingleton.Instance.PlaySound(3); // 0 es el índice del sonido que deseas 
                }
                AudioManagerSingleton.Instance.PlaySound(3); // 0 es el índice del sonido que deseas 

            }

        }
    }

    // Este m�todo se llama cuando el objeto est� siendo mirado.
    public void OnPointerEnter()
    {
        _isGazedAt = true;
    }

    // Este m�todo se llama cuando el objeto ya no est� siendo mirado.
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