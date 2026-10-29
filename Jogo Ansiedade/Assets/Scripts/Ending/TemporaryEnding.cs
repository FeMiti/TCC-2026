using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Text;
using System.Collections.Generic;

public class TemporaryEnding : MonoBehaviour
{

    [SerializeField] private GameObject endingObjects;
    [SerializeField] private GameObject scoreObjects;

    [SerializeField] private TMP_Text score;

    private void Start()
    {
        StringBuilder scoreText = new StringBuilder("");

        List<GameData> data = DataCollector.Instance.gameDatas;

        for(int i = 0; i < DataCollector.Instance.gameDatas.Count; i++)
        {
            scoreText.Append($"{i}: Nome = {data[i].taskName} - Tempo p/ chegar = {data[i].timeAtReach-data[i].timeStarted:F2} - Tempo p/ concluir = {data[i].timeAtConclusion-data[i].timeAtReach:F2} - Tempo total = {data[i].timeAtConclusion-data[i].timeStarted:F2} - N de erros = {data[i].numberOfMistakes} - Diff de ansiedade = {data[i].anxietyAfter-data[i].anxietyBefore} \n");
        }

        scoreText.Append($"\n");

        List<RelaxData> relax = DataCollector.Instance.relaxDatas;

        for(int i = 0; i < DataCollector.Instance.relaxDatas.Count; i++)
        {
            scoreText.Append($"{i}: Na task = {relax[i].taskNumber} - Tempo p/ concluir = {relax[i].timeAtConclusion-relax[i].timeStarted:F2} - N de erros = {relax[i].numberOfMistakes} - Com ansiedade = {relax[i].anxiety}\n");
        }

        scoreText.Append($"\n");

        scoreText.Append($"Final obtido = {DataCollector.Instance.ending}");

        score.text = scoreText.ToString();
    }

    public void ShowScore()
    {
        endingObjects.SetActive(false);
        scoreObjects.SetActive(true);
    }

    public void ReturnEnding()
    {
        scoreObjects.SetActive(false); 
        endingObjects.SetActive(true);
    }

    public void RestartGame()
    {
        SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
