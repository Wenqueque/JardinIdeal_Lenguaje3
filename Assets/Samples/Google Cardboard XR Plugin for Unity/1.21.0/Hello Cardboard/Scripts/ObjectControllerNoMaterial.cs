using UnityEngine;
using System.Collections; // Agrega esta l�nea

public class ObjectControllerNoMaterial : MonoBehaviour
{
    public GameObject object1; // Arrastra el primer objeto aqu� desde el Inspector.
    public GameObject object2; // Arrastra el segundo objeto aqu� desde el Inspector.

    private bool _isGazedAt = false;

    private void Update()
    {
        if (_isGazedAt && Input.GetMouseButtonDown(1)) // Cambiamos a Input.GetMouseButtonDown(1) para clic derecho
        {
            Debug.Log("Clic derecho detectado");

            // Comprobar si se est� mirando el objeto con un Raycast
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.gameObject == gameObject)
                {
                    // Solo cambia el objeto si se hizo clic derecho en el objeto
                    StartCoroutine(SwitchObjectsAndBack());
                }
            }
        }
    }

    // Agrega este m�todo para gestionar el cambio entre los objetos.
    private IEnumerator SwitchObjectsAndBack()
    {
        object1.SetActive(false);
        object2.SetActive(true);

        yield return new WaitForSeconds(1.0f); // Espera un segundo antes de cambiar de nuevo.

        object1.SetActive(true);
        object2.SetActive(false);
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
