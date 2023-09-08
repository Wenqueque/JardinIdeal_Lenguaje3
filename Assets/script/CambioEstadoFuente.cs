using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CambioEstadoFuente : MonoBehaviour
{
    public List<GameObject> estadosFuente; // Lista de estados del objeto fuente
    private int estadoActualIndex = 0; // �ndice del estado actual
    private bool cambioFinalizado = false; // Indicador de que se ha llegado al quinto estado

    // Mira al objetivo
    private bool isGazedAt = false;

    // Sonido
    public AudioSource SonidoFuente;

    private void Start()
    {
        CambiarEstado(estadoActualIndex); // Inicializa el estado visual
    }

    private void Update()
    {
        // Detecta la interacci�n del jugador y cambia el estado solo si el puntero est� mirando el objeto
        if (isGazedAt && !cambioFinalizado)
        {
            //if (Input.GetAxis("Regar") > 0)
            if (Input.GetKeyDown(KeyCode.R))
            {
                CambiarEstado((estadoActualIndex + 1) % estadosFuente.Count);

                // Verifica si hemos llegado al quinto estado y marcamos el cambio como finalizado
                if (estadoActualIndex == 4)
                {
                    cambioFinalizado = true;
                }
            }
        }
    }

    // Este m�todo se llama cuando el objeto est� siendo mirado.
    public void OnPointerEnter()
    {
        isGazedAt = true;
    }

    // Este m�todo se llama cuando el objeto ya no est� siendo mirado.
    public void OnPointerExit()
    {
        isGazedAt = false;
    }

    private void CambiarEstado(int nuevoEstadoIndex)
    {
        // Desactiva el estado actual
        estadosFuente[estadoActualIndex].SetActive(false);

        // Activa el nuevo estado
        estadosFuente[nuevoEstadoIndex].SetActive(true);

        // Actualiza el �ndice del estado actual
        estadoActualIndex = nuevoEstadoIndex;
    }
}
