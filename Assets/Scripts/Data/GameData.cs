using System.IO;
using UnityEngine;

public class GameData : MonoBehaviour
{
    #region Fields
    private static GameData _gameDataInstance;
    
    private string _nameToDisplay;
    private string _nameInput;

    private int _bestScore;
    private int _previousBestScore;
    #endregion

    #region Properties
    public static GameData GameDataInstance
    {
        get { return _gameDataInstance; }
    }

    public string NameToDisplay
    {
        get { return _nameToDisplay; }
        set { _nameToDisplay = value; }
    }

    public string NameInput
    {
        get { return _nameInput; }
        set { _nameInput = value; }
    }

    public int BestScore
    {
        get { return _bestScore; }
        set { _bestScore = value; }
    }

    public int PreviousBestScore
    {
        get { return _previousBestScore; }
        set { _previousBestScore = value; }
    }
    #endregion

    #region Unity Methods
    private void Awake()
    {
        if (_gameDataInstance != null)
        {
            Destroy(gameObject);
            return;
        }

        _gameDataInstance = this;
        DontDestroyOnLoad(gameObject);

        LoadData();
    }
    #endregion

    #region Public Methods
    public void SaveData()
    {
        PlayerData playerData = new PlayerData();
        playerData.BestScore = _bestScore;
        playerData.Name = _nameToDisplay;

        string json = JsonUtility.ToJson(playerData);
        string path = Application.persistentDataPath + "/savefile.json";
        File.WriteAllText(path, json);
    }
    #endregion

    #region Private Methods
    private void LoadData()
    {
        string path = Application.persistentDataPath + "/savefile.json";
        
        if (File.Exists(path))
        {
            LoadSavedPlayerData(path);
            return;
        }

        LoadDefaultPlayerData();
    }

    private void LoadSavedPlayerData(string path)
    {
        string json = File.ReadAllText(path);
        PlayerData playerData = JsonUtility.FromJson<PlayerData>(json);
        _bestScore = playerData.BestScore;
        _previousBestScore = _bestScore;
        _nameToDisplay = playerData.Name;
    }

    private void LoadDefaultPlayerData()
    {
        _bestScore = 0;
        _previousBestScore = _bestScore;
        _nameToDisplay = null;
    }
    #endregion
}