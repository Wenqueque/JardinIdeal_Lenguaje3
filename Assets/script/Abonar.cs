using UnityEngine;

public class Abonar : MonoBehaviour
{
    public GameObject object1; // Objeto 1
    public GameObject object2; // Objeto 2

    private bool _isGazedAt = false;

    private void Start()
    {
        // Desactiva el objeto 2 al inicio
        object2.SetActive(false);
    }

    private void Update()
    {
        // Si estamos mirando el objeto con _isGazed y se presiona el bot�n derecho del mouse
        if (_isGazedAt && Input.GetAxis("Abonar") > 0) //JOYSTICK
        //if (_isGazedAt && Input.GetMouseButtonDown(1)) //TECLADO
        {
            // Cambia al objeto 2
            object1.SetActive(false);
            object2.SetActive(true);
        }
    }

    // Este m�todo se llama cuando el objeto est� siendo mirado.
    public void OnPointerEnter()
    {
        _isGazedAt = true;
    }

    // Este m�todo se llama cuando el objeto ya no est� siendo mirado.
    public void OnPointerExit()
    {
        _isGazedAt = false;
    }
}
