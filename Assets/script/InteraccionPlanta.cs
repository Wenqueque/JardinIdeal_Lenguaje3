using UnityEngine;

public class InteraccionPlanta : MonoBehaviour
{
    public cambioEstados.EstadoPlanta estadoInicial; // Estado inicial de la planta
    private cambioEstados.EstadoPlanta estadoActual; // Estado actual de la planta

    private void Start()
    {
        estadoActual = estadoInicial; // Inicializa el estado
    }

    private void Update()
    {
        // Detecta la interacción del jugador y cambia el estado
        if (Input.GetKeyDown(KeyCode.B) && estadoActual == cambioEstados.EstadoPlanta.Abonar)
        {
            // Realiza acciones para el estado de Abonar
            Debug.Log("Abonando la planta");
            CambiarEstado(cambioEstados.EstadoPlanta.NecesitaRegar);
        }
        else if (Input.GetKeyDown(KeyCode.R) && estadoActual == cambioEstados.EstadoPlanta.NecesitaRegar)
        {
            // Realiza acciones para el estado de NecesitaRegar
            Debug.Log("Regando la planta");
            CambiarEstado(cambioEstados.EstadoPlanta.Bien);
        }
    }

    private void CambiarEstado(cambioEstados.EstadoPlanta nuevoEstado)
    {
        estadoActual = nuevoEstado;
        // Aquí puedes cambiar el modelo de la planta según el nuevo estado
        // Por ejemplo, desactivar un modelo y activar otro
    }
}
