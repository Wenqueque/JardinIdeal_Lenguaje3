using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
[RequireComponent(typeof(Rigidbody))]
public class ControlPlayer : MonoBehaviour
{
    public float walkSpeed = 5f;
    public float gravity = 9.81f; // Gravedad
    private CharacterController player;
    private Camera mainCamera;
    private Rigidbody rb;

    private Vector3 velocity; // Velocidad vertical

    private void Awake()
    {
        player = GetComponent<CharacterController>();
        mainCamera = Camera.main; // Obt�n la referencia a la c�mara principal
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true; // Evita rotaciones indeseadas
        rb.useGravity = false; // Deshabilita la gravedad del Rigidbody
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

        // Aplica gravedad al jugador usando Rigidbody
        if (!player.isGrounded)
        {
            velocity.y -= gravity * Time.deltaTime;
        }
        else
        {
            velocity.y = -0.5f; // Peque�o ajuste para evitar rebotes al tocar el suelo
        }

        // Aplica la velocidad vertical calculada
        player.Move(velocity * Time.deltaTime);
    }

}
