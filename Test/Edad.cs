using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Edad : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void Joven()
    {
        PlayerPrefs.SetString("Edad", "Joven");
    }

    public void Adulto()
    {
        PlayerPrefs.SetString("Edad", "Adulto");
    }

    public void Senior()
    {
        PlayerPrefs.SetString("Edad", "Senior");
    }
}
