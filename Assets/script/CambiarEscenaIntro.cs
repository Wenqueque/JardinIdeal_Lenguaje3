using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CambiarEscenaIntro : MonoBehaviour
{
    // Llama a esta funci�n cuando se presione la tecla "P".
    void Update()
    {
        if (Input.GetAxis("Cortar") > 0)
        //if (Input.GetKeyDown(KeyCode.P))
        {
            // Cambia a la escena "SampleScene".
            SceneManager.LoadScene("SampleScene");
        }
    }
}
