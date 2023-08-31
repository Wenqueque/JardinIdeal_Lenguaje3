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
        mainCamera = Camera.main; // Obtén la referencia a la cámara principal
    }

    private void Update()
    {
        // Obtén las entradas de movimiento del jugador
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        // Calcula la dirección de movimiento en relación con la cámara
        Vector3 cameraForward = mainCamera.transform.forward;
        Vector3 cameraRight = mainCamera.transform.right;
        cameraForward.y = 0f; // Mantén la dirección horizontal
        cameraRight.y = 0f;   // Mantén la dirección horizontal
        Vector3 moveDirection = cameraForward.normalized * verticalInput + cameraRight.normalized * horizontalInput;

        // Mueve al jugador en la dirección calculada
        player.Move(moveDirection.normalized * walkSpeed * Time.deltaTime);
    }
       
}