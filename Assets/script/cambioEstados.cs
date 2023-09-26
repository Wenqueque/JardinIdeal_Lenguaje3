using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cambioEstados : MonoBehaviour
{
    //fuente--------
    public List<GameObject> estadosFuente; // Lista de estados del objeto fuente
    private int estadoActualIndexFuente = 0; // Índice del estado actual
    private bool cambioFinalizadoFuente = false; // Indicador de que se ha llegado al quinto estado

    // Mira al objetivo
    private bool isGazedAtFuente = false;

    // Sonido
    public AudioSource SonidoFuente;

    // Cooldown
    private bool puedeInteractuarFuente = true;
    public float tiempoEsperaInteraccion = 1.0f; // Tiempo de espera en segundos

    // Limitaciones
    public int interaccionesConFuente = 0;
    public int limiteInteraccionesFuente = 6; // Esto cambia según con cuántas plantas interactuamos

    //fuente-------

    public GameObject prefabBien;
    public GameObject prefabNecesitaRegar;
    public GameObject prefabSobreRegar;

    public EstadoPlanta estadoInicial; // Estado inicial de la planta
    public EstadoPlanta estadoActual; // Estado actual de la planta

    private GameObject plantaActual; // Referencia al prefab activo actualmente

    // MIRA AL OBJETIVO
    private bool _isGazedAt = false;

    // CONTADOR DE REGAR ENTRE TODOS LOS OBJETOS
    private static int vecesRegadas = 1;

    // ESTADOS DE PLANTA
    public enum EstadoPlanta
    {
        Bien,
        NecesitaRegar,
        SobreRegar,
        nada
    }

    // TIEMPO
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

    private IEnumerator PermitirInteraccionDespuésDeEspera()
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
        if (interaccionesConFuente >= limiteInteraccionesFuente && vecesRegadas == 1)
        {
            // Reinicia los parámetros apropiados aquí
            //interaccionesConFuente = 0;
            cambioFinalizadoFuente = false;
            estadoActual = estadoInicial;

            AudioManagerSingleton.Instance.PlaySound(1); // 0 es el índice del sonido que deseas reproducir
            // También puedes reiniciar otros parámetros si es necesario
        }

        if (_isGazedAt && Input.GetAxis("Cortar") > 0 && estadoActual == EstadoPlanta.NecesitaRegar && vecesRegadas < 1)
        //if (_isGazedAt && Input.GetKeyDown(KeyCode.E) && estadoActual == EstadoPlanta.NecesitaRegar && vecesRegadas < 1)
        {
            // Realiza acciones para el estado de NecesitaRegar
            Debug.Log("Regando la planta");
            CambiarEstado(EstadoPlanta.Bien);
            vecesRegadas++; // Incrementa el contador de riegos
            AudioManagerSingleton.Instance.PlaySound(2); // 0 es el índice del sonido que deseas reproducir
        }
        else if (_isGazedAt && Input.GetAxis("Cortar") > 0 && estadoActual == EstadoPlanta.Bien && vecesRegadas < 1)
        //else if (_isGazedAt && Input.GetKeyDown(KeyCode.E) && estadoActual == EstadoPlanta.Bien && vecesRegadas < 1)
        {
            // Realiza acciones para el estado de NecesitaRegar
            Debug.Log("SobreRegando la planta");
            CambiarEstado(EstadoPlanta.SobreRegar);
            vecesRegadas++; // Incrementa el contador de riegos
            AudioManagerSingleton.Instance.PlaySound(2); // 0 es el índice del sonido que deseas reproducir
        }

        if (isGazedAtFuente && !cambioFinalizadoFuente && Input.GetAxis("Cortar") > 0)
        //if (isGazedAtFuente && !cambioFinalizadoFuente && Input.GetKeyDown(KeyCode.E))
        {
            if (puedeInteractuarFuente)
            {
                Collider[] colliders = GetComponentsInChildren<Collider>(); // Obtén los colliders de los objetos hijos del puntero

                foreach (Collider collider in colliders)
                {
                    if (collider.CompareTag("Fuente"))
                    {
                        if (interaccionesConFuente < limiteInteraccionesFuente)
                        {
                            // Realiza las acciones de interacción con la fuente aquí

                            // Después de la interacción, espera un tiempo antes de permitir otra interacción
                            puedeInteractuarFuente = false;
                            StartCoroutine(PermitirInteraccionDespuésDeEspera());

                            CambiarEstadoFuente((estadoActualIndexFuente + 1) % estadosFuente.Count);

                            // Verifica si hemos llegado al quinto estado y marcamos el cambio como finalizado
                            if (estadoActualIndexFuente == 6) // Cambiado de 5 a 4, ya que los índices comienzan desde 0
                            {
                                cambioFinalizadoFuente = true;
                            }

                            Debug.Log("Clic en objeto con tag 'Fuente'");
                            interaccionesConFuente++; // Incrementa el contador de interacciones

                            vecesRegadas = 0; // Reinicia el contador de riegos
                            AudioManagerSingleton.Instance.PlaySound(0); // 0 es el índice del sonido que deseas reproducir

                            // Puedes desactivar el collider del objeto puntero invisible para evitar múltiples interacciones
                            collider.enabled = false;

                            break; // Sal del bucle, ya que hemos encontrado una colisión
                        }
                        if (interaccionesConFuente >= limiteInteraccionesFuente)
                        {
                            // Si llegamos al límite, desactivamos la interacción con la fuente
                            Debug.Log("Límite de interacciones con la fuente alcanzado.");
                            puedeInteractuarFuente = false;
                            AudioManagerSingleton.Instance.PlaySound(1); // 0 es el índice del sonido que deseas reproducir
                        }
                        else
                        {
                            Debug.Log("Límite de interacciones con la fuente alcanzado.");
                            AudioManagerSingleton.Instance.PlaySound(1); // 0 es el índice del sonido que deseas reproducir
                        }
                    }
                }
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

        // Actualiza el índice del estado actual
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
}