using System;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Класс представляющий объект обновляющий таймеры
/// </summary>
public class TimerUpdater : MonoBehaviour, ITimerUpdater
{
    #region Static

    public static ITimerUpdater timerUpdater;

    #endregion

    #region Protepties

    [SerializeField]
    private LockTimer add100Timer;
    [SerializeField]
    private LockTimer x2Timer;

    #endregion // Properties

    #region Private

    private List<LockTimer> timers = new List<LockTimer>();

    #endregion // Private

    #region Unity Methods

    private void Start()
    {
        timerUpdater = this;        

        timers.Add(add100Timer);
        timers.Add(x2Timer);

        LoadTime();
    }

    private void OnApplicationFocus(bool focus)
    {
        if (focus) LoadTime();
    }

    private void FixedUpdate()
    {
        foreach (LockTimer t in timers)
        {
            if (t.Working)
            {
                t.Millisecond -= Time.deltaTime;
                t.Millisecond = (float)Math.Round(t.Millisecond, 2);

                if (t.Millisecond < 0) t.TimerTick();
            }
        }
    }

    #endregion // Unity Methods

    #region Save/Load

    private void LoadTime()
    {
        DateTime unlockTime;

        try
        {
            unlockTime = Convert.ToDateTime(DataController.dataController.UnlockTimeAdd100);

            if (unlockTime > DateTime.Now)
            {
                var deltaUnlockTime = (unlockTime - DateTime.Now);
                add100Timer.StartTimer(deltaUnlockTime);
            }

            unlockTime = Convert.ToDateTime(DataController.dataController.UnlockTimeX2);

            if (unlockTime > DateTime.Now)
            {
                var deltaUnlockTime = (unlockTime - DateTime.Now);
                x2Timer.StartTimer(deltaUnlockTime);
            }
        }
        catch
        {
            Debug.Log("Time not loaded");
        }
    }

    public void SaveUnlockTime(LockTimer lockTimer)
    {
        DateTime unlockTime = DateTime.Now.Add(lockTimer.GetTime());

        if (lockTimer.Equals(add100Timer))
        {
            DataController.dataController.UnlockTimeAdd100 = Convert.ToString(unlockTime);
        }
        else if (lockTimer.Equals(x2Timer))
        {
            DataController.dataController.UnlockTimeX2 = Convert.ToString(unlockTime);
        }
    }

    #endregion // Save/Load
}