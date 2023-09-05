using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{


    // Start is called before the first frame update
    void Start()
    {


    }

    // Update is called once per frame
    void Update()
    {

    }

    public void EmpezarJuego()
    {
        SceneManager.LoadScene("HelloCardboard");
    }

    public void Opciones()
    {
        SceneManager.LoadScene("SampleScene");
    }

    public void VolverMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void SalirJuego()
    {
        Application.Quit();
    }
}
