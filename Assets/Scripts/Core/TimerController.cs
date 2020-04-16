using System;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Класс представляющий объект управляющий таймерами
/// </summary>
public class TimerController : MonoBehaviour, ITimerController
{
    #region Private

    private List<Timer> timers;

    private IServiceLocator ServiceLocator { get; set; }
    private IDataController dataController { get; set; }
    private ITimeData timeData { get; set; }

    #endregion // Private

    #region Unity Methods

    private void OnApplicationFocus(bool focus)
    {
        if (focus) LoadTime(((DataController)dataController).GetComponent<ITimeData>());
    }

    private void FixedUpdate()
    {
        foreach (Timer t in timers)
        {
            if (t.Working)
            {
                t.Millisecond -= Time.fixedDeltaTime;
                t.Millisecond = (float)Math.Round(t.Millisecond, 2);

                if (t.Millisecond < 0) t.TimerTick();
            }
        }
    }

    #endregion // Unity Methods

    public void Init(IServiceLocator serviceLocator)
    {
        ServiceLocator = serviceLocator;
        dataController = ServiceLocator.GetService<IDataController>();
        timeData = ((DataController)dataController).GetComponent<ITimeData>();

        timers = new List<Timer>(GameObject.FindObjectsOfType<Timer>());

        DontDestroyOnLoad(this);

        LoadTime(timeData);
    }

    #region Save/Load

    private void LoadTime(ITimeData data)
    {
        DateTime unlockTime;

        try
        {
            foreach (Timer timer in timers)
            {
                switch (timer.GetComponentInParent<IButton>().Type)
                {
                    case ButtonTypes.AddCurrency100:
                        unlockTime = Convert.ToDateTime(data.UnlockTimeAdd100);
                        break;

                    case ButtonTypes.MultiplierX2:
                        unlockTime = Convert.ToDateTime(data.UnlockTimeX2);
                        break;

                    default:
                        unlockTime = Convert.ToDateTime(data.UnlockTimeAdd100);
                        break;
                }

                if (unlockTime > DateTime.Now)
                {
                    var deltaUnlockTime = (unlockTime - DateTime.Now);
                    timer.StartTimer(deltaUnlockTime);
                }
            }

            Debug.Log("Time loaded");
        }
        catch
        {
            Debug.Log("Time not loaded");
        }
    }

    public void SaveUnlockTime(Timer lockTimer)
    {
        DateTime unlockTime = DateTime.Now.Add(lockTimer.GetTime());

        try
        {
            switch (lockTimer.GetComponentInParent<IButton>().Type)
            {
                case ButtonTypes.AddCurrency100:
                    timeData.UnlockTimeAdd100 = Convert.ToString(unlockTime);
                    break;
                case ButtonTypes.MultiplierX2:
                    timeData.UnlockTimeX2 = Convert.ToString(unlockTime);
                    break;
            }

            Debug.Log("Time saved");
        }
        catch
        {
            Debug.Log("Time not saved");
        }
    }

    #endregion // Save/Load
}