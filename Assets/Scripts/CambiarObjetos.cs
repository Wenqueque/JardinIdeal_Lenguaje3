using UnityEngine;

public class CambiarObjetos : MonoBehaviour
{
    public GameObject objeto1; // Arrastra el primer objeto al inspector
    public GameObject objeto2; // Arrastra el segundo objeto al inspector

    private bool estaEnObjeto1 = true;

    private void Start()
    {
        // Al inicio, mostramos el objeto1 y ocultamos el objeto2
        objeto1.SetActive(true);
        objeto2.SetActive(false);
    }

    private void Update()
    {
        // Detectar clic izquierdo
        if (Input.GetMouseButtonDown(0))
        {
            CambiarObjetos1A2();
        }

        // Detectar clic derecho
        if (Input.GetMouseButtonDown(1))
        {
            CambiarObjetos2A1();
        }
    }

    private void CambiarObjetos1A2()
    {
        if (estaEnObjeto1)
        {
            objeto1.SetActive(false);
            objeto2.SetActive(true);
            estaEnObjeto1 = false;
        }
    }

    private void CambiarObjetos2A1()
    {
        if (!estaEnObjeto1)
        {
            objeto2.SetActive(false);
            objeto1.SetActive(true);
            estaEnObjeto1 = true;
        }
    }
}
