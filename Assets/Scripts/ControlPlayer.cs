using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class ControlPlayer : MonoBehaviour
{
    public float walkSpeed = 5f;
    private CharacterController player;
    private Camera mainCamera;

    private void Awake()
    {
        player = GetComponent<CharacterController>();
        mainCamera = Camera.main; // Obt�n la referencia a la c�mara principal
    }

    private void Update()
    {
        // Obt�n las entradas de movimiento del jugador
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        // Calcula la direcci�n de movimiento en relaci�n con la c�mara
        Vector3 cameraForward = mainCamera.transform.forward;
        Vector3 cameraRight = mainCamera.transform.right;
        cameraForward.y = 0f; // Mant�n la direcci�n horizontal
        cameraRight.y = 0f;   // Mant�n la direcci�n horizontal
        Vector3 moveDirection = cameraForward.normalized * verticalInput + cameraRight.normalized * horizontalInput;

        // Mueve al jugador en la direcci�n calculada
        player.Move(moveDirection.normalized * walkSpeed * Time.deltaTime);
    }
       
}