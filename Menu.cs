using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.IO;
using UnityEngine.SceneManagement;
public class Menu : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SeleccionarEscena(string Nombre_escena)
    {
        SceneManager.LoadScene(Nombre_escena);
    }

    public void Quit()
    {
        Application.Quit();
    }


}