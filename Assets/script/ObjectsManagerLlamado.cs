using UnityEngine;
using System.Collections.Generic;

public class ObjectsManagerLlamado : MonoBehaviour
{
    public float maxDistance = 10f; // La distancia máxima para el raycasting
    public LayerMask layerMask; // Capas a considerar para el raycasting
    public List<GameObject> objetosAMostrar = new List<GameObject>(); // Lista de GameObjects que se pueden mostrar u ocultar

    private void Update()
    {
        // Lanzar un rayo hacia adelante desde la posición de la cámara u objeto.
        Ray ray = new Ray(transform.position, transform.forward);
        RaycastHit hit;

        // Inicialmente, asumimos que no se ve ningún objeto con la etiqueta "Fuente" ni "Abonar".
        bool seVeRegadera = false;
        bool seVeAbonar = false;
        bool seVeTijeras = false;
        bool seVeMano = false;
        bool seVePozo = false;

        if (Physics.Raycast(ray, out hit, maxDistance, layerMask))
        {
            // Si el rayo golpea un objeto con la etiqueta "Fuente", establecemos seVeFuente a true.
            if (hit.collider.CompareTag("Fuente") || hit.collider.CompareTag("Regadera"))
            {
                //Debug.Log("MOSTRANDO REGADERA");
                seVeRegadera = true;
            }

            // Si el rayo golpea un objeto con la etiqueta "Abonar", establecemos seVeAbonar a true.
            if (hit.collider.CompareTag("Abonacion"))
            {
                //Debug.Log("MOSTRANDO ABONO");
                seVeAbonar = true;
            }

            // Si el rayo golpea un objeto con la etiqueta "Abonar", establecemos seVeAbonar a true.
            if (hit.collider.CompareTag("Fruta"))
            {
                //Debug.Log("MOSTRANDO ABONO");
                seVeMano = true;
            }

            // Si el rayo golpea un objeto con la etiqueta "Abonar", establecemos seVeAbonar a true.
            if (hit.collider.CompareTag("Semilla"))
            {
                //Debug.Log("MOSTRANDO ABONO");
                seVePozo = true;
            }
        }

        // Configuramos el estado activo de los GameObjects en la lista en función de si se ven los objetos con las etiquetas "Fuente" y "Abonar".
        objetosAMostrar[0].SetActive(seVeRegadera);
        objetosAMostrar[1].SetActive(seVeAbonar);
        objetosAMostrar[2].SetActive(seVeTijeras);
        objetosAMostrar[3].SetActive(seVeMano);
        objetosAMostrar[4].SetActive(seVePozo);
    }
}
