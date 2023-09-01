using UnityEngine;
using System.Collections;

public class StateManager : MonoBehaviour
{
    public GameObject[] states; // Array para almacenar los estados
    private int currentState = 0; // El estado actual
    private bool isChangingState = false; // Indicador para evitar cambios múltiples simultáneos

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
        // Detecta la interacción con el joystick (por ejemplo, un botón en el joystick)
        if (Input.GetMouseButtonDown(0) && !isChangingState) // 0 indica el botón izquierdo del mouse
        {
            StartCoroutine(ChangeState());
        }
    }

    private IEnumerator ChangeState()
    {
        isChangingState = true;

        // Desactiva el estado actual
        states[currentState].SetActive(false);

        // Cambia al siguiente estado (circularmente)
        currentState = (currentState + 1) % states.Length;

        // Activa el nuevo estado
        states[currentState].SetActive(true);

        // Espera 10 segundos antes de volver al estado anterior
        yield return new WaitForSeconds(10f);

        // Desactiva el estado actual
        states[currentState].SetActive(false);

        // Vuelve al estado anterior
        currentState = (currentState - 1 + states.Length) % states.Length;
        states[currentState].SetActive(true);

        isChangingState = false;
    }
}