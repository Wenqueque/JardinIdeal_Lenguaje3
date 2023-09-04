using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class limiteGeneral : MonoBehaviour
{
    public float limiteXMin = -45f; // Límite mínimo en el eje X
    public float limiteXMax = 45f;  // Límite máximo en el eje X
    public float limiteZMin = -45f; // Límite mínimo en el eje Z
    public float limiteZMax = 45f;  // Límite máximo en el eje Z

    private void Update()
    {
        // Obtener la posición actual del jugador
        Vector3 posicionJugador = transform.position;

        // Limitar el movimiento en el eje X
        posicionJugador.x = Mathf.Clamp(posicionJugador.x, limiteXMin, limiteXMax);

        // Limitar el movimiento en el eje Z
        posicionJugador.z = Mathf.Clamp(posicionJugador.z, limiteZMin, limiteZMax);

        // Asignar la nueva posición al jugador
        transform.position = posicionJugador;
    }
}

