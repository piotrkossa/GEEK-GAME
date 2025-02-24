using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    [SerializeField] Scene MaineMenuScene;
    [SerializeField] TextMeshProUGUI GameplayTimer;
    [SerializeField] TextMeshProUGUI CorrectAnswers;
    [SerializeField] TextMeshProUGUI EnemyDefeated;
    [SerializeField] TextMeshProUGUI Information;
    [SerializeField] List<Ciekawostki> ciekawostka;


    private void Start()
    {
        GameplayTimer.text = "Czas gry: " + PlayerPrefs.GetFloat("TimePlaying").ToString();
        CorrectAnswers.text = "Liczba poprawnych odpowiedzi: " + PlayerPrefs.GetInt("CorrectAnswers").ToString();
        EnemyDefeated.text = "Liczba pokonanych przeciwników: " + PlayerPrefs.GetInt("EnemyDefeated").ToString();

        if (ciekawostka != null && ciekawostka.Count > 0)
        {
            int randomIndex = Random.Range(0, ciekawostka.Count);
            Information.text = ciekawostka[randomIndex].trescCiekawostki;
        }
    }



    public void Exit()
    {
        SceneManager.LoadScene("MainMenu", LoadSceneMode.Single);
    }


}
