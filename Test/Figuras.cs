using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Threading;

public class Figuras : MonoBehaviour
{
    public Image Image1;
    public Image Image2;
    public Image Image3;
    public Image Image4;
    public Image Image5;
    public Image Image6;
    public Text texto;
    public Text textoNivel;
    public Text explicacion;
    readonly string[] lista = new string[] {"imag1", "imag2", "imag3", "imag4", "imag5", "imag6" };
    int[] i = new int[6];
    int[] w = new int[] {0, 0, 0, 0, 0, 0};
    float timer;
    bool cierto;
    bool pausado;
    bool empieza;
    float timer2;
    private int umbral;
    bool acierto;
    int nivel;
    bool fin;
    float tiempoDeRespuesta;
    public Text TResponse;

    void Start()
    {
        empieza = false;
        string edad = PlayerPrefs.GetString("Edad", null);
        if (edad.Equals("Joven"))
        {
            tiempoDeRespuesta = 60f;
        }
        else if (edad.Equals("Adulto"))
        {
            tiempoDeRespuesta = 70f;
        }
        else
        {
            tiempoDeRespuesta = 90f;
        }
        Image1 = GameObject.Find("Image 1").GetComponent<Image>();
        Image2 = GameObject.Find("Image 2").GetComponent<Image>();
        Image3 = GameObject.Find("Image 3").GetComponent<Image>();
        Image4 = GameObject.Find("Image 4").GetComponent<Image>();
        Image5 = GameObject.Find("Image 5").GetComponent<Image>();
        Image6 = GameObject.Find("Image 6").GetComponent<Image>();
        timer = 0;
        cierto = false;
        pausado = false;
        umbral = 3;
        acierto = false;
        nivel = 1;
        fin = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (empieza)
        {
            TResponse.text = "Admite un tiempo de reacción de " + tiempoDeRespuesta / 100f + " seg";
            textoNivel.text = "Nivel " + nivel;
            timer += Time.deltaTime;
            if (timer >= umbral && pausado == false)
            {
                texto.text = null;
                cierto = false;
                for (int j = 0; j < 6; j++)
                {
                    i[j] = Random.Range(0, 5);
                }
                comprobacion();
                Image1.sprite = Resources.Load<Sprite>("Imagenes/" + lista[i[0]]);
                Image2.sprite = Resources.Load<Sprite>("Imagenes/" + lista[i[1]]);
                Image3.sprite = Resources.Load<Sprite>("Imagenes/" + lista[i[2]]);
                Image4.sprite = Resources.Load<Sprite>("Imagenes/" + lista[i[3]]);
                Image5.sprite = Resources.Load<Sprite>("Imagenes/" + lista[i[4]]);
                Image6.sprite = Resources.Load<Sprite>("Imagenes/" + lista[i[5]]);
                timer = 0;
            }

            if (cierto == true)
            {
                timer2 += Time.deltaTime;
            }

            if ((Input.touchCount == 1 || Input.GetButtonDown("space")) && cierto == true && pausado == false)
            {
                empieza = false;
                pausado = true;
                texto.text = "Has ganado";
                float durtim = timer2;
                if (durtim < tiempoDeRespuesta / 100)
                {
                    acierto = true;
                    texto.text = "Has tardado: " + durtim.ToString("F2") + " seg \n Pulsa con 2 dedos en la pantalla para pasar al siguiente nivel.";
                    texto.color = new Color(0, 255, 0);
                    if (nivel == 3)
                    {
                        fin = true;
                        texto.text = "Has tardado: " + durtim.ToString("F2") + " seg \n Has alcanzado el nivel máximo, ¡enhorabuena!";
                        texto.color = new Color(255, 255, 0);
                    }
                }
                else
                {
                    texto.text = "Has tardado: " + durtim.ToString("F2") + " seg. \n Pulsa con 2 dedos en la pantalla para volverlo a intentar.";
                    texto.color = new Color(255, 0, 0);
                }

                // Set the score
                float highscore = PlayerPrefs.GetFloat("HighScore_Figuras", 0);
                if (durtim < highscore || highscore == 0)
                {
                    PlayerPrefs.SetFloat("HighScore_Figuras", durtim);
                }
            }
            else if ((Input.touchCount == 1 || Input.GetButtonDown("space")) && cierto == false)
            {
                texto.text = "¡NO!";
                texto.color = new Color(255, 0, 0);
            }
        }
        

        if(empieza == false && (Input.touchCount > 1 || Input.GetButtonDown("a")))
        {
            if (!fin)
            {
                Image1.color = new Color(255, 255, 255, 255);
                Image2.color = new Color(255, 255, 255, 255);
                Image3.color = new Color(255, 255, 255, 255);
                Image4.color = new Color(255, 255, 255, 255);
                Image5.color = new Color(255, 255, 255, 255);
                Image6.color = new Color(255, 255, 255, 255);
                explicacion.text = null;
                if (acierto)
                {
                    nivel++;
                    umbral -= 1;
                }
                acierto = false;
                pausado = false;
                texto.text = null;
                timer2 = 0;
                timer = 0;
                cierto = false;
                empieza = true;
                Thread.Sleep(200);
            }
            
        }
    }

    bool comprobacion()
    {
        for(int k = 0; k < 6; k++)
        {
            for(int l = 0; l < 6; l++)
            {
                if (k != l)
                {
                    if (i[k] == i[l]) w[k]++;
                    if (w[k] >= 2) cierto = true;
                    
                }
            }
            w[k] = 0;
        }
        print(cierto);
        timer2 = 0;
        return cierto;
    }
}
