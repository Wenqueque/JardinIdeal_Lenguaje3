using UnityEngine;
using System.Collections;

public class StateManager : MonoBehaviour
{
    public GameObject[] states; // Array para almacenar los estados
    private int currentState = 0; // El estado actual
    private bool isChangingState = false; // Indicador para evitar cambios múltiples simultáneos

    private float timer = 0f; // Temporizador para el cambio de estado
    private float state2Duration = 10f; // Duración del estado en segundos

    private bool _isGazedAt = false;

    // Variable estática para el contador de riegos compartido entre todos los objetos
    private static int vecesRegadas = 0;

    private void Start()
    {
        // Desactiva todos los estados excepto el primero (estado inicial)
        for (int i = 1; i < states.Length; i++)
        {
            states[i].SetActive(false);
        }
    }

    private void Update()
    {
        if (_isGazedAt && currentState == 0 && Input.GetMouseButtonDown(0) && !isChangingState && vecesRegadas < 3)
        {
            Debug.Log("Eje 'Regar' activado");
            ChangeToState(1);
            vecesRegadas++; // Incrementa el contador de riegos
        }

        // Si estamos en el estado 2, actualiza el temporizador
        if (currentState == 1)
        {
            timer -= Time.deltaTime;

            // Si el temporizador llega a cero, cambia de nuevo al estado 1
            if (timer <= 0)
            {
                ChangeToState(0);
            }
        }

        // Si hacemos clic en un objeto con el tag "Fuente"
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.CompareTag("Fuente"))
                {
                    Debug.Log("Clic en objeto con tag 'Fuente'");
                    vecesRegadas = 0; // Reinicia el contador de riegos
                }
            }
        }
    }

    private void ChangeToState(int newState)
    {
        isChangingState = true;

        // Desactiva el estado actual
        states[currentState].SetActive(false);

        // Cambia al nuevo estado
        currentState = newState;

        // Activa el nuevo estado
        states[currentState].SetActive(true);

        // Reinicia el temporizador cuando cambiamos al estado 2
        if (currentState == 1)
        {
            timer = state2Duration;
        }

        isChangingState = false;

        // Si el jugador ha regado tres veces en total, desactiva la función de riego
        if (vecesRegadas >= 3)
        {
            _isGazedAt = false;
        }
    }

    // Este método se llama cuando el objeto está siendo mirado.
    public void OnPointerEnter()
    {
        _isGazedAt = true;
    }

    // Este método se llama cuando el objeto ya no está siendo mirado.
    public void OnPointerExit()
    {
        _isGazedAt = false;
    }
}
