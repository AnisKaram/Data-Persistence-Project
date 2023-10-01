using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuUIManager : MonoBehaviour
{
    #region Fields
    [Header("GameObjects")]
    [SerializeField] private GameObject _nameIsRequiredObject;

    [Header("Buttons")]
    [SerializeField] private Button _startButton;

    [Header("Texts - TextMeshPro")]
    [SerializeField] private TextMeshProUGUI _nameInputText;
    [SerializeField] private TextMeshProUGUI _bestScoreText;

    [Header("Input Fields - TextMeshPro")]
    [SerializeField] private TMP_InputField _nameInputField;
    
    private UnityAction _startAction;
    #endregion

    #region Unity Methods
    private void Awake()
    {
        CreateButtonActions();
    }

    private void Start()
    {
        SetBestScoreText();
    }
    #endregion

    #region Private Methods
    private void CreateButtonActions()
    {
        _startAction = new UnityAction(() => { EnterScene(1); });
        _startButton.onClick.AddListener(_startAction);
    }

    private void EnterScene(int sceneIndex)
    {
        Debug.Log($"Name length {_nameInputField.text.Length}");

        if (IsNameInputFieldEmpty())
        {
            _nameIsRequiredObject.SetActive(true);
            return;
        }

        GameData.GameDataInstance.NameInput = _nameInputField.text;
        SceneManager.LoadSceneAsync(sceneIndex);
    }

    private void SetBestScoreText()
    {
        int bestScoreValue = GameData.GameDataInstance.BestScore;
        string bestScoreName = GameData.GameDataInstance.NameToDisplay;

        _bestScoreText.text = $"Best Score: {bestScoreName} : {bestScoreValue}";
        _nameInputField.text = bestScoreName != null ? bestScoreName : null;
    }

    private bool IsNameInputFieldEmpty()
    {
        if (_nameInputField.text.Length > 0)
        {
            return false;
        }
        return true;
    }
    #endregion
}