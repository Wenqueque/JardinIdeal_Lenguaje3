using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CambioEstadoFuente : MonoBehaviour
{
    public List<GameObject> estadosFuente; // Lista de estados del objeto fuente
    private int estadoActualIndexFuente = 0; // �ndice del estado actual
    private bool cambioFinalizadoFuente = false; // Indicador de que se ha llegado al quinto estado

    // Mira al objetivo
    private bool isGazedAtFuente = false;

    // Sonido
    public AudioSource SonidoFuente;

    private void Start()
    {
        CambiarEstadoFuente(estadoActualIndexFuente); // Inicializa el estado visual
    }

    private void Update()
    {
        // Detecta la interacci�n del jugador y cambia el estado solo si el puntero est� mirando el objeto
        if (isGazedAtFuente && !cambioFinalizadoFuente)
        {
            //if (Input.GetAxis("Regar") > 0)
            if (Input.GetKeyDown(KeyCode.R))
            {
                CambiarEstadoFuente((estadoActualIndexFuente + 1) % estadosFuente.Count);

                // Verifica si hemos llegado al quinto estado y marcamos el cambio como finalizado
                if (estadoActualIndexFuente == 4)
                {
                    cambioFinalizadoFuente = true;
                }
            }
        }
    }

    // Este m�todo se llama cuando el objeto est� siendo mirado.
    public void OnPointerEnter()
    {
        isGazedAtFuente = true;
    }

    // Este m�todo se llama cuando el objeto ya no est� siendo mirado.
    public void OnPointerExit()
    {
        isGazedAtFuente = false;
    }

    private void CambiarEstadoFuente(int nuevoEstadoIndexFuente)
    {
        // Desactiva el estado actual
        estadosFuente[estadoActualIndexFuente].SetActive(false);

        // Activa el nuevo estado
        estadosFuente[nuevoEstadoIndexFuente].SetActive(true);

        // Actualiza el �ndice del estado actual
        estadoActualIndexFuente = nuevoEstadoIndexFuente;
    }
}
