using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EstadosArea2 : MonoBehaviour
{
    //fuente--------
    public List<GameObject> estadosFuente; // Lista de estados del objeto fuente
    private int estadoActualIndexFuente = 0; //  ndice del estado actual
    private bool cambioFinalizadoFuente = false; // Indicador de que se ha llegado al quinto estado
    // Mira al objetivo
    private bool isGazedAtFuente = false;
    // Sonido
    public AudioSource SonidoFuente;
    //Cooldown
    private bool puedeInteractuarFuente = true;
    public float tiempoEsperaInteraccion = 1.0f; // Tiempo de espera en segundos
    //Limitaciones
    public int interaccionesConFuente = 0;
    public int limiteInteraccionesFuente = 6; //Esto cambia segun con cuantas plantas interactuamos
    //fuente-------

    private bool estaMirandoFuente = false; // Variable para verificar si el jugador está mirando un objeto con el tag "Fuente"
    public float radioDeteccionFuente = 1.0f; // Radio de detección de la esfera de colisión
    private bool estaColisionandoConFuente = false; // Agrega esta variable de instancia

    public GameObject prefabAbonar;
    public GameObject prefabBien;
    public GameObject prefabNecesitaRegar;
    public GameObject prefabSobreRegar;

    public EstadoPlanta estadoInicial; // Estado inicial de la planta
    public EstadoPlanta estadoActual; // Estado actual de la planta

    private GameObject plantaActual; // Referencia al prefab activo actualmente

    //MIRA AL OBJETIVO
    private bool _isGazedAt = false;

    //CONTADOR DE REGAR ENTRE TODOS LOS OBJETOS
    private static int vecesRegadas = 0;

    //ESTADOS DE PLANTA
    public enum EstadoPlanta
    {
        Abonar,
        Bien,
        NecesitaRegar,
        SobreRegar,
        nada
    }

    //TIEMPO
    public float tiempoEnEstadoBien = 0f; // Tiempo en el estado "Bien"
    public float tiempoParaCambioBien = 50f; // Tiempo para cambiar del estado "Bien" a "NecesitaRegar"

    private void Start()
    {
        estadoActual = estadoInicial; // Inicializa el estado
        CambiarEstado(estadoActual); // Inicializa el estado visual
        //fuente-----
        CambiarEstadoFuente(estadoActualIndexFuente); // Inicializa el estado visual
        //fuente-------
    }

    private IEnumerator PermitirInteraccionDespuesDeEspera()
    {
        yield return new WaitForSeconds(tiempoEsperaInteraccion);
        puedeInteractuarFuente = true;
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
        else if (estadoActual == EstadoPlanta.SobreRegar)
        {
            tiempoEnEstadoBien += Time.deltaTime;
        }

        // Verifica si ha pasado suficiente tiempo en el estado "Bien"
        if (tiempoEnEstadoBien >= tiempoParaCambioBien)
        {
            CambiarEstado(EstadoPlanta.NecesitaRegar); // Cambia a "NecesitaRegar"
        }

        // Verifica si se llegó al límite de interacciones con la fuente y vecesRegadas es igual a 1
        if (interaccionesConFuente >= limiteInteraccionesFuente)
        {
            // Reinicia los parámetros apropiados aquí
            interaccionesConFuente = 0;
            cambioFinalizadoFuente = false;
            estadoActual = estadoInicial;
            AudioManagerSingleton.Instance.PlaySound(2); // 0 es el índice del sonido que deseas reproducir
        }
        //if (_isGazedAt && Input.GetAxis("Abonar") > 0 && estadoActual == EstadoPlanta.Abonar) //JOYSTICK
        if (_isGazedAt && Input.GetKeyDown(KeyCode.B) && estadoActual == EstadoPlanta.Abonar) //TECLADO
        {
            // Realiza acciones para el estado de Abonar
            Debug.Log("Abonando la planta");
            CambiarEstado(EstadoPlanta.Bien);
            AudioManagerSingleton.Instance.PlaySound(2); // 0 es el índice del sonido que deseas reproducir
        }
        //else if (_isGazedAt && Input.GetAxis("Regar") > 0 && estadoActual == EstadoPlanta.NecesitaRegar && vecesRegadas < 1) //JOYSTICK
        else if (_isGazedAt && Input.GetKeyDown(KeyCode.R) && estadoActual == EstadoPlanta.NecesitaRegar && vecesRegadas < 1) //TECLADO
        {
            // Realiza acciones para el estado de NecesitaRegar
            Debug.Log("Regando la planta");
            CambiarEstado(EstadoPlanta.Bien);
            vecesRegadas++; // Incrementa el contador de riegos
            AudioManagerSingleton.Instance.PlaySound(1); // 0 es el índice del sonido que deseas reproducir
        }
        //else if (_isGazedAt && Input.GetAxis("Regar") > 0 && estadoActual == EstadoPlanta.Bien && vecesRegadas < 1) //JOYSTICK
        else if (_isGazedAt && Input.GetKeyDown(KeyCode.R) && estadoActual == EstadoPlanta.Bien && vecesRegadas < 1) //TECLADO
        {
            // Realiza acciones para el estado de NecesitaRegar
            Debug.Log("SobreRegando la planta");
            CambiarEstado(EstadoPlanta.SobreRegar);
            vecesRegadas++; // Incrementa el contador de riegos
            AudioManagerSingleton.Instance.PlaySound(1); // 0 es el índice del sonido que deseas reproducir
        }

        // Obtener la posición y la dirección de la mirada del jugador
        Vector3 posicionCamara = Camera.main.transform.position;
        Vector3 miradaDireccion = Camera.main.transform.forward;

        // Lanzar una esfera de colisión en la dirección de la mirada para verificar objetos con el tag "Fuente"
        Collider[] colliders = Physics.OverlapSphere(posicionCamara, radioDeteccionFuente);

        estaMirandoFuente = false;

        foreach (Collider collider in colliders)
        {
            if (collider.CompareTag("Fuente"))
            {
                estaMirandoFuente = true;
                break; // Salir del bucle si se encuentra una fuente
            }
        }

        //if (isGazedAtFuente && !cambioFinalizadoFuente && Input.GetAxis("Regar") > 0)
        // Si el jugador presiona la tecla "R" y está colisionando con un objeto que tiene el tag "Fuente"
        //if (Input.GetAxis("Regar") > 0 && estaMirandoFuente)
        if (Input.GetKeyDown(KeyCode.R) && estaMirandoFuente)
        {
            if (puedeInteractuarFuente)
            {
                // Realiza las acciones de interacción con la fuente aquí

                // Después de la interacción, espera un tiempo antes de permitir otra interacción
                puedeInteractuarFuente = false;
                StartCoroutine(PermitirInteraccionDespuesDeEspera());

                CambiarEstadoFuente((estadoActualIndexFuente + 1) % estadosFuente.Count);

                // Verifica si hemos llegado al último estado y marcamos el cambio como finalizado
                if (estadoActualIndexFuente == 4) // Índice del último estado
                {
                    cambioFinalizadoFuente = true;
                }

                Debug.Log("Interacción con la fuente");
                interaccionesConFuente++; // Incrementa el contador de interacciones

                vecesRegadas = 0; // Reinicia el contador de riegos
                AudioManagerSingleton.Instance.PlaySound(2); // 0 es el índice del sonido que deseas reproducir
            }
        }
    }

    // Este método se llama cuando el objeto está siendo mirado.
    public void OnPointerEnter()
    {
        _isGazedAt = true;
        isGazedAtFuente = true;
    }

    // Este método se llama cuando el objeto ya no está siendo mirado.
    public void OnPointerExit()
    {
        _isGazedAt = false;
        isGazedAtFuente = false;
    }

    private void CambiarEstadoFuente(int nuevoEstadoIndexFuente)
    {
        // Desactiva el estado actual
        estadosFuente[estadoActualIndexFuente].SetActive(false);

        // Activa el nuevo estado
        estadosFuente[nuevoEstadoIndexFuente].SetActive(true);

        // Actualiza el  ndice del estado actual
        estadoActualIndexFuente = nuevoEstadoIndexFuente;
    }

    public void CambiarEstado(EstadoPlanta nuevoEstado)
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
            case EstadoPlanta.SobreRegar:
                plantaActual = prefabSobreRegar;
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

        // Si el jugador ha regado tres veces en total, desactiva la función de riego
        if (vecesRegadas >= 1)
        {
            _isGazedAt = false;
        }

    }
    // Este método se llama cuando colisionas con un objeto que tiene el tag "Fuente".
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Fuente"))
        {
            estaColisionandoConFuente = true;
        }
    }

    // Este método se llama cuando dejas de colisionar con un objeto que tiene el tag "Fuente".
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Fuente"))
        {
            estaColisionandoConFuente = false;
        }
    }
}