using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    [SerializeField] private SceneTransition _sceneTransition;
    [SerializeField] Scene MaineMenuScene;
    [SerializeField] TextMeshProUGUI GameplayTimer;
    [SerializeField] TextMeshProUGUI CorrectAnswers;
    [SerializeField] TextMeshProUGUI EnemyDefeated;
    [SerializeField] TextMeshProUGUI InformationHeader;
    [SerializeField] TextMeshProUGUI Information;
    [SerializeField] List<Ciekawostki> ciekawostka;


    private void Start()
    {
        GameplayTimer.text = "Czas gry: " + PlayerPrefs.GetFloat("TimePlaying", 1f).ToString() + "s.";
        CorrectAnswers.text = "Poprawne odpowiedzi: " + PlayerPrefs.GetInt("CorrectAnswers", 0).ToString();
        EnemyDefeated.text = "Pokonani przeciwnicy: " + PlayerPrefs.GetInt("EnemyDefeated", 0).ToString();

        if (ciekawostka != null && ciekawostka.Count > 0)
        {
            int randomIndex = Random.Range(0, ciekawostka.Count);
            Information.text = ciekawostka[randomIndex].trescCiekawostki;
        }
    }


    private bool isRunning = false;

    public async void Exit()
    {
        if (isRunning) return;
        isRunning = true;
        
        await _sceneTransition.EndTransition();
        SceneManager.LoadScene("MainMenu", LoadSceneMode.Single);
    }


}
