using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class MainUIManager : MonoBehaviour
{
    #region Fields
    [Header("Buttons")]
    [SerializeField] private Button _backButton;

    [Header("Texts - TextMeshPro")]
    [SerializeField] private TextMeshProUGUI _nameToDisplay;
    [SerializeField] private TextMeshProUGUI _bestScoreToDisplay;

    private UnityAction _backAction;
    #endregion

    #region Unity Methods
    private void Awake()
    {
        MainManager.OnBestScoreChanged += OnBestScoreChangedUpdateUI;
        CreateButtonActions();
    }

    private void Start()
    {
        SetNameToDisplay();
        SetBestScore();
    }

    private void OnDestroy()
    {
        MainManager.OnBestScoreChanged -= OnBestScoreChangedUpdateUI;
    }
    #endregion

    #region Private Methods
    private void CreateButtonActions()
    {
        _backAction = new UnityAction(() => { EnterScene(0); });
        _backButton.onClick.AddListener(_backAction);
    }

    private void SetNameToDisplay()
    {
        string nameString = GameData.GameDataInstance.NameToDisplay;
        _nameToDisplay.text = $"Name: {nameString}";
    }

    private void SetBestScore()
    {
        int bestScore = GameData.GameDataInstance.BestScore;
        _bestScoreToDisplay.text = $"Best Score: {bestScore}";
    }

    private void EnterScene(int sceneIndex)
    {
        SceneManager.LoadSceneAsync(sceneIndex);
    }

    private void OnBestScoreChangedUpdateUI()
    {
        SetNameToDisplay();
        SetBestScore();
    }
    #endregion
}