using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class plantar : MonoBehaviour
{
    public GameObject prefabSinPlantar;
    public GameObject prefabPlantado;

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
        sinPlantar,
        Plantado,
    }

    private void Start()
    {
        estadoActual = estadoInicial; // Inicializa el estado
        CambiarEstado(estadoActual); // Inicializa el estado visual
    }

    private void Update()
    {
        
        // Detecta la interacci�n del jugador y cambia el estado solo si el puntero est� mirando el objeto
        if (_isGazedAt)
        {
            //if (Input.GetAxis("Cortar") > 0 && estadoActual == EstadoPlanta.sinPlantar)
            if (Input.GetKeyDown(KeyCode.E) && estadoActual == EstadoPlanta.sinPlantar)
            {
                // Realiza acciones para el estado de Abonar
                Debug.Log("Abonando la planta");
                CambiarEstado(EstadoPlanta.Plantado);
                if (SonidoAbono != null)
                {
                    SonidoAbono.Play();
                    AudioManagerSingleton.Instance.PlaySound(3); // 0 es el índice del sonido que deseas reproducir
                }
                AudioManagerSingleton.Instance.PlaySound(3); // 0 es el índice del sonido que deseas reproducir
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
            case EstadoPlanta.sinPlantar:
                plantaActual = prefabSinPlantar;
                break;
            case EstadoPlanta.Plantado:
                plantaActual = prefabPlantado;
                break;
            default:
                break;
        }

        // Activa el objeto correspondiente al nuevo estado
        if (plantaActual != null)
        {
            plantaActual.SetActive(true);
        }

        
    }
}