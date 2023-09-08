using UnityEngine;
using System.Collections.Generic;

public class ObjectsManager : MonoBehaviour
{
    public List<GameObject> gameObjectsToShow = new List<GameObject>();

    // Método para mostrar un GameObject específico
    public void MostrarHerramienta(int index)
    {
        if (index >= 0 && index < gameObjectsToShow.Count)
        {
            gameObjectsToShow[index].SetActive(true);
        }
    }

    // Método para ocultar un GameObject específico
    public void OcultarHerramienta(int index)
    {
        if (index >= 0 && index < gameObjectsToShow.Count)
        {
            gameObjectsToShow[index].SetActive(false);
        }
    }
}
