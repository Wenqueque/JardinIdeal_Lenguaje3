using UnityEngine;
using System.Collections;

public class StateManager : MonoBehaviour
{
    public GameObject[] states; // Array para almacenar los estados
    private int currentState = 0; // El estado actual
    private bool isChangingState = false; // Indicador para evitar cambios múltiples simultáneos

    private float timer = 0f; // Temporizador para el cambio de estado
    private float state2Duration = 5f; // Duración del estado 2 en segundos

    private bool _isGazedAt = false;

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
        // Si estamos en el estado 1 y el eje "Regar" está activado
        if (_isGazedAt && currentState == 0 && Input.GetAxis("Regar") > 0 && !isChangingState)
        {
            Debug.Log("Eje 'Regar' activado");
            ChangeToState(1);
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
