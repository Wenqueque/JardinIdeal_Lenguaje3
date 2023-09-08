using UnityEngine;
using System.Collections.Generic;

public class ObjectsManagerLlamado : MonoBehaviour
{
    public float maxDistance = 10f; // La distancia m�xima para el raycasting
    public LayerMask layerMask; // Capas a considerar para el raycasting
    public List<GameObject> objetosAMostrar = new List<GameObject>(); // Lista de GameObjects que se pueden mostrar u ocultar

    private void Update()
    {
        // Lanzar un rayo hacia adelante desde la posici�n de la c�mara u objeto.
        Ray ray = new Ray(transform.position, transform.forward);
        RaycastHit hit;

        // Inicialmente, asumimos que no se ve ning�n objeto con la etiqueta "Fuente" ni "Abonar".
        bool seVeFuente = false;
        bool seVeAbonar = false;

        if (Physics.Raycast(ray, out hit, maxDistance, layerMask))
        {
            // Si el rayo golpea un objeto con la etiqueta "Fuente", establecemos seVeFuente a true.
            if (hit.collider.CompareTag("Fuente"))
            {
                Debug.Log("MOSTRANDO REGADERA");
                seVeFuente = true;
            }

            // Si el rayo golpea un objeto con la etiqueta "Abonar", establecemos seVeAbonar a true.
            if (hit.collider.CompareTag("Abonacion"))
            {
                Debug.Log("MOSTRANDO ABONO");
                seVeAbonar = true;
            }
        }

        // Configuramos el estado activo de los GameObjects en la lista en funci�n de si se ven los objetos con las etiquetas "Fuente" y "Abonar".
        objetosAMostrar[0].SetActive(seVeFuente);
        objetosAMostrar[1].SetActive(seVeAbonar);
    }
}
