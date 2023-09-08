using UnityEngine;
using System.Collections.Generic;

public class ObjectsManager : MonoBehaviour
{
    public List<GameObject> gameObjectsToShow = new List<GameObject>();

    // M�todo para mostrar un GameObject espec�fico
    public void MostrarHerramienta(int index)
    {
        if (index >= 0 && index < gameObjectsToShow.Count)
        {
            gameObjectsToShow[index].SetActive(true);
        }
    }

    // M�todo para ocultar un GameObject espec�fico
    public void OcultarHerramienta(int index)
    {
        if (index >= 0 && index < gameObjectsToShow.Count)
        {
            gameObjectsToShow[index].SetActive(false);
        }
    }
}
