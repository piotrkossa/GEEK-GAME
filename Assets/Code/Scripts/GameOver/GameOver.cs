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

    private void Start()
    {
        GameplayTimer.text = "Czas gry: "; PlayerPrefs.GetFloat("TimePlaying").ToString();
        CorrectAnswers.text = "Liczba poprawnych odpowiedzi: "; PlayerPrefs.GetInt("CorrectAnswers").ToString();
        EnemyDefeated.text = "Liczba pokonanych przeciwników: "; PlayerPrefs.GetInt("EnemyDefeated").ToString();
    }

    public void Exit()
    {
        SceneManager.LoadScene("MainMenu", LoadSceneMode.Single);
    }


}
