using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private Slider slider;

    public static float valueAttack = 0f;
    public static float valuePatrouille = 0f;

    public void OnSliderChanged(float v)
    {
        if (v == 1f)
        {
            valueAttack = 0.1f;
            valuePatrouille = 0.5f;
        }
        else if (v ==2f)
        {
            valueAttack = 0.2f;
            valuePatrouille = 0.6f;
        }
        else if (v == 3f)
        {
            valueAttack = 0.3f;
            valuePatrouille = 0.7f;
        }
    }

    void Start()
    {
      
    }


     void Update()
    {
       
    }
    public void PlayGames(){
        SceneManager.LoadScene("SampleScene");
        //  SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex+1);
        //Application.Quit();
    }

    public void MenuGame()
    {
        SceneManager.LoadScene("Menu");
        //  SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex+1);
        //Application.Quit();
    }

    public void quitGame()
    {   //quitte l'editeur de text (visual studio)
        Application.Quit();
    }

  
}
