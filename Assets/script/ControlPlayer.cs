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
    public string etiquetaObjetoMarchito = "Marchito";
    public float limiteXMin = -5f; // Límite mínimo en el eje X
    public float limiteXMax = 5f;  // Límite máximo en el eje X
    public float limiteZMin = -5f; // Límite mínimo en el eje Z
    public float limiteZMax = 5f;  // Límite máximo en el eje Z
    //SONIDO
    public AudioSource pasos;
    private bool Hactivo;
    private bool Vactivo;

    //ILUMINACION
    public GameObject sol; // Referencia al GameObject del sol
    public float tiempoParaDesaparecer = 5f; // Tiempo en segundos para que el sol desaparezca
    private float tiempoInactivo = 0f; // Tiempo en segundos de inactividad

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
        // Buscar objetos con la etiqueta "marchito" en la escena
        GameObject[] objetosMarchitos = GameObject.FindGameObjectsWithTag("Marchito");
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

        // Verificar si el jugador se está moviendo
        if (Mathf.Abs(horizontalInput) > 0.1f || Mathf.Abs(verticalInput) > 0.1f)
        {
            tiempoInactivo = 0f; // Restablecer el tiempo de inactividad
            MostrarSol(true); // Mostrar el sol
        }
        else
        {
            tiempoInactivo += Time.deltaTime; // Incrementar el tiempo de inactividad
            if (tiempoInactivo >= tiempoParaDesaparecer)
            {
                MostrarSol(false); // Ocultar el sol después de 5 segundos de inactividad
            }
        }

        // Obtener la posición actual del jugador
        Vector3 posicionJugador = transform.position;
        // Si hay objetos "marchito" en la escena, aplicar límites de movimiento
        if (objetosMarchitos.Length > 0)
        {
            // Limitar el movimiento en el eje X
            posicionJugador.x = Mathf.Clamp(posicionJugador.x, limiteXMin, limiteXMax);

            // Limitar el movimiento en el eje Z
            posicionJugador.z = Mathf.Clamp(posicionJugador.z, limiteZMin, limiteZMax);

            // Asignar la nueva posición al jugador
            transform.position = posicionJugador;
        }

        //SONIDO-----------------------------------------------------

        if (Input.GetButtonDown("Horizontal"))
        {
            if (Vactivo == false)
            {
                Hactivo = true;
                pasos.Play();
            }
        }

        if (Input.GetButtonDown("Vertical"))
        {
            if (Hactivo == false)
            {
                Vactivo = true;
                pasos.Play();
            }
        }

        if (Input.GetButtonUp("Horizontal"))
        {
            Hactivo = false;
            if (Vactivo == false)
            {
                pasos.Pause();
            }
        }

        if (Input.GetButtonUp("Vertical"))
        {
            Vactivo = false;
            if (Hactivo == false)
            {
                pasos.Pause();
            }
        }
    }

    private void MostrarSol(bool mostrar)
    {
        if (sol != null)
        {
            sol.SetActive(mostrar);
        }
    }

}