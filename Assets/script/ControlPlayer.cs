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

    public float limiteXMin = -50f; // Límite mínimo en el eje X
    public float limiteXMax = 50f;  // Límite máximo en el eje X
    public float limiteZMin = -50f; // Límite mínimo en el eje Z
    public float limiteZMax = 50f;  // Límite máximo en el eje Z
    private void Awake()
    {
        player = GetComponent<CharacterController>();
        mainCamera = Camera.main; // Obtén la referencia a la cámara principal
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true; // Evita rotaciones indeseadas
        rb.useGravity = false; // Deshabilita la gravedad del Rigidbody
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

        // Aplica gravedad al jugador usando Rigidbody
        if (!player.isGrounded)
        {
            velocity.y -= gravity * Time.deltaTime;
        }
        else
        {
            velocity.y = -0.5f; // Pequeño ajuste para evitar rebotes al tocar el suelo
        }

        // Aplica la velocidad vertical calculada
        player.Move(velocity * Time.deltaTime);

        // Obtener la posición actual del jugador
        Vector3 posicionJugador = transform.position;

        // Limitar el movimiento en el eje X
        posicionJugador.x = Mathf.Clamp(posicionJugador.x, limiteXMin, limiteXMax);

        // Limitar el movimiento en el eje Z
        posicionJugador.z = Mathf.Clamp(posicionJugador.z, limiteZMin, limiteZMax);

        // Asignar la nueva posición al jugador
        transform.position = posicionJugador;
    }

}