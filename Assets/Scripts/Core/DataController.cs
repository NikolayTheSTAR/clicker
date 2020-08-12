using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using Utility;

public class DataController : MonoBehaviour, IDataController, ICurrencyData, ITimeData, ISaveManager
{
    #region Static

    public static bool Started;

    #endregion //Static

    #region Properties

    private long softCurrency;
    public long SoftCurrency
    {
        get
        {
            return softCurrency;
        }
        set
        {
            softCurrency = value;
            uiController.UpdateCurrency(this);
        }
    }

    private long donateCurrency;
    public long DonateCurrency
    {
        get
        {
            return donateCurrency;
        }
        set
        {
            donateCurrency = value;
            uiController.UpdateCurrency(this);
        }
    }

    private string unlockTimeAdd100;
    public string UnlockTimeAdd100
    {
        get
        {
            return unlockTimeAdd100;
        }
        set
        {
            unlockTimeAdd100 = value;
        }
    }

    private string unlockTimeX2;
    public string UnlockTimeX2
    {
        get
        {
            return unlockTimeX2;
        }
        set
        {
            unlockTimeX2 = value;
        }
    }


    #endregion //Properties

    #region Private

    private IServiceLocator ServiceLocator { get; set; }
    private IUIController uiController { get; set; }

    private int currentScene;

    #endregion //Private

    #region Unity Methods

    private void OnApplicationPause(bool pause)
    {
        if (Started && pause)
        {
            SaveGameState();
        }
    }

    private void OnApplicationQuit()
    {
        SaveGameState();
    }

    #endregion //Unity Methods

    public void Init(IServiceLocator serviceLocator)
    {
        ServiceLocator = serviceLocator;
        uiController = ServiceLocator.GetService<IUIController>();

        currentScene = 0;

        DontDestroyOnLoad(this);

        Application.targetFrameRate = 100;
        SceneManager.sceneLoaded += OnLevelLoaded;

        TryLoadData();

        LoadGame();

        Started = true;
    }

    public void LoadGame()
    {
        LoadScene(((SceneNames)currentScene).ToString());
    }

    private void OnLevelLoaded(Scene scene, LoadSceneMode mode) { }
    
    #region Scene Management

    private void LoadScene(string sceneName)
    {
        SceneManager.LoadSceneAsync(sceneName);
    }

    #endregion //Scene Management

    #region Save/Load

    public void SaveGameState()
    {
        SaveData data = new SaveData();

        data.Add("SoftCurrency", SoftCurrency);
        data.Add("DonateCurrency", DonateCurrency);
        data.Add("UnlockTimeAdd100", UnlockTimeAdd100);
        data.Add("UnlockTimeX2", UnlockTimeX2);

        if (Serialization.SaveToBinnary<SaveData>(data)) Debug.Log("Game saved succesfuly");
        else Debug.Log("Game not saved");
    }

    private void TryLoadData()
    {
        SaveData data;
        if (!Serialization.LoadFromBinnary<SaveData>(out data)) return;

        SoftCurrency = data.Get<long>("SoftCurrency");
        DonateCurrency = data.Get<long>("DonateCurrency");
        UnlockTimeAdd100 = data.Get<string>("UnlockTimeAdd100");
        UnlockTimeX2 = data.Get<string>("UnlockTimeX2");
    }

    #endregion //Save/Load
}

[System.Serializable]
public class SaveData
{
    private Dictionary<string, object> container;

    public SaveData()
    {
        container = new Dictionary<string, object>();
    }

    public void Add(string name, object obj)
    {
        container.Add(name, obj);
    }

    public T Get<T>(string name)
    {
        try
        {
            return (T)container[name];
        }
        catch
        {
            return default;
        }
    }
}

public enum SceneNames
{
    Game
}