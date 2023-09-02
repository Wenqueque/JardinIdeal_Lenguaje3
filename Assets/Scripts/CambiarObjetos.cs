using UnityEngine;

public class CambiarObjetos : MonoBehaviour
{
    public GameObject esfera; // Arrastra el primer objeto al inspector
    public GameObject cubo; // Arrastra el segundo objeto al inspector

    private bool estaEnObjeto1 = true;

    private void Start()
    {
        // Al inicio, mostramos el objeto1 y ocultamos el objeto2
        esfera.SetActive(true);//esfera =objeto1 visible
        cubo.SetActive(false);//cubo =objeto2 no visible
    }

    private void Update()
    {
        // Detectar clic izquierdo
        if (Input.GetMouseButtonDown(0))// 0 es el boton izquierdo
        {
            Debug.Log("Clic izquierdo detectado");
            
            if (cubo == true) { cubo.SetActive(false); esfera.SetActive(true);  }
            
        }

        // Detectar clic derecho
        if (Input.GetMouseButtonDown(1))// 1 es el boton derecho
        {
            Debug.Log("Clic derecho detectado");
            if (esfera == true) { esfera.SetActive(false); cubo.SetActive(true); } 
        }
    }
}
