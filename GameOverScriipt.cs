using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class GameOverScriipt : MonoBehaviour
{
    // Start is called before the first frame update

   public TextMeshProUGUI textScore;
   public void Setup(int score)
    {
        //gameObject.SetActive(true);
        
        textScore.text = score.ToString() + " POINTS"; 
    }

    public void PlayGames()
    {
        SceneManager.LoadScene("SampleScene");
        //  SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex+1);
        //Application.Quit();
    }

    public void MenuGame()
    {
        SceneManager.LoadScene("Menu");
       // Application.Quit();
    }
    private void Update()
    {
     
    }
}
