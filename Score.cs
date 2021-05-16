using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
	public Text scoreConduccion;
    public Text scoreColores;
    public Text scoreFiguras;
    public Text scoreCuentaAtras;
    public Text scoreBeep;
    public Text scoreMusic;
    public Text scoreCeleridad;
    public Text edad;

    float score;
    float scorecolores;
    float scorefiguras;
    float scorebeep;
    float scoremusic;
    float scoreceleridad;
    float scoreSeg;
    float scoreMill;

    // Start is called before the first frame update
    void Start()
    {
        edad.text = PlayerPrefs.GetString("Edad", null);
        score = PlayerPrefs.GetFloat("HighScore", 0);
		scoreConduccion.text = "Mejor puntuación conducción: " + score.ToString("F2") + " seg.";
        scorecolores = PlayerPrefs.GetFloat("HighScore_Colores", 0);
        scoreColores.text = "Mejor puntuación colores: " + scorecolores.ToString("F2") + " seg.";
        scorefiguras = PlayerPrefs.GetFloat("HighScore_Figuras", 0);
        scoreFiguras.text = "Mejor puntuación figuras: " + scorefiguras.ToString("F2") + " seg.";
        scorebeep = PlayerPrefs.GetFloat("HighScore_Beep", 0);
        scoreBeep.text = "Mejor puntuación pitido: " + scorebeep.ToString("F2") + " seg.";
        scoremusic = PlayerPrefs.GetFloat("HighScore_Music", 0);
        scoreMusic.text = "Mejor puntuación música: " + scoremusic.ToString("F2") + " seg.";
        scoreceleridad = PlayerPrefs.GetFloat("HighScore_Celeridad", 0);
        scoreCeleridad.text = "Mejor puntuación celeridad: " + scoreceleridad.ToString("F0");
        scoreSeg = PlayerPrefs.GetFloat("HighScore_CuentaAtrasSeg", 0);
        scoreMill = PlayerPrefs.GetFloat("HighScore_CuentaAtrasMill", 0);
        scoreCuentaAtras.text = "Mejor puntuación cuenta atrás: " + (scoreSeg+scoreMill/100f).ToString("F2") + " seg.";

    }

    // Update is called once per frame
    void Update()
    {
        edad.text = PlayerPrefs.GetString("Edad", null);
    }

    public void Borrar()
    {
        PlayerPrefs.SetFloat("HighScore", 0);
        PlayerPrefs.SetFloat("HighScore_Colores", 0);
        PlayerPrefs.SetFloat("HighScore_Figuras", 0);
        PlayerPrefs.SetFloat("HighScore_Beep", 0);
        PlayerPrefs.SetFloat("HighScore_Music", 0);
        PlayerPrefs.SetFloat("HighScore_Celeridad", 0);
        PlayerPrefs.SetFloat("HighScore_CuentaAtrasSeg", 0);
        PlayerPrefs.SetFloat("HighScore_CuentaAtrasMill", 0);
        

        score = PlayerPrefs.GetFloat("HighScore", 0);
        scoreConduccion.text = "Mejor puntuación conducción: " + score.ToString("F2") + " seg.";
        scorecolores = PlayerPrefs.GetFloat("HighScore_Colores", 0);
        scoreColores.text = "Mejor puntuación colores: " + scorecolores.ToString("F2") + " seg.";
        scorefiguras = PlayerPrefs.GetFloat("HighScore_Figuras", 0);
        scoreFiguras.text = "Mejor puntuación figuras: " + scorefiguras.ToString("F2") + " seg.";
        scorebeep = PlayerPrefs.GetFloat("HighScore_Beep", 0);
        scoreBeep.text = "Mejor puntuación pitido: " + scorebeep.ToString("F2") + " seg.";
        scoremusic = PlayerPrefs.GetFloat("HighScore_Music", 0);
        scoreMusic.text = "Mejor puntuación música: " + scoremusic.ToString("F2") + " seg.";
        scoreceleridad = PlayerPrefs.GetFloat("HighScore_Celeridad", 0);
        scoreCeleridad.text = "Mejor puntuación celeridad: " + scoreceleridad.ToString("F0");
        scoreSeg = PlayerPrefs.GetFloat("HighScore_CuentaAtrasSeg", 0);
        scoreMill = PlayerPrefs.GetFloat("HighScore_CuentaAtrasMill", 0);
        scoreCuentaAtras.text = "Mejor puntuación cuenta atrás: " + (scoreSeg + scoreMill / 100f).ToString("F2") + " seg.";
    }
}
