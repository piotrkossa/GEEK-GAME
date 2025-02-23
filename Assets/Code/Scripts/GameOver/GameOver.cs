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

    public void Exit()
    {
        SceneManager.LoadScene("MainMenu", LoadSceneMode.Single);
    }
}
