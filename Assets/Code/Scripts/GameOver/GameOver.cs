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
        GameplayTimer.text = (PlayerPrefs.GetFloat("BesTime").ToString());
    }

    public void Exit()
    {
        SceneManager.LoadScene("MainMenu", LoadSceneMode.Single);
    }


}
