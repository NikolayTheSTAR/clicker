using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Класс представляющий таймер
/// </summary>
public class Timer : MonoBehaviour, ITimer
{
    #region Protepties

    private bool working;
    public bool Working
    {
        get
        {
            return working;
        }
        set
        {
            working = value;
        }
    }

    private int hour;
    public int Hour
    {
        get
        {
            return hour;
        }
        set
        {
            hour = value;
        }
    }
    private int minute;
    public int Minute
    {
        get
        {
            return minute;
        }
        set
        {
            minute = value;
        }
    }
    private int second;
    public int Second
    {
        get
        {
            return second;
        }
        set
        {
            second = value;
        }
    }

    private float millisecond = 1f;
    public float Millisecond
    {
        get
        {
            return millisecond;
        }
        set
        {
            millisecond = value;
        }
    }

    #endregion // Properties

    #region Private

    private Text timerText;
    private ITimerController timerController;

    [SerializeField, Range(0, 59)]
    private sbyte lockTimeHour, lockTimeMinute, lockTimeSecond;

    #endregion // Private

    #region Unity Methods

    private void Awake()
    {
        timerText = GetComponent<Text>();
    }

    private void Start()
    {
        timerController = FindObjectOfType<TimerController>();
    }

    #endregion // Unity Methods

    #region Timer

    public void StartTimer()
    {
        if (!Working)
        {
            SetTime(lockTimeHour, lockTimeMinute, lockTimeSecond);

            UpdateTimerText();
            timerText.enabled = true;

            timerController.SaveUnlockTime(this);

            Working = true;
        }
    }

    public void StartTimer(TimeSpan customLockTime)
    {
        SetTime(customLockTime.Hours, customLockTime.Minutes, customLockTime.Seconds, (float)customLockTime.Milliseconds / 1000);

        UpdateTimerText();
        timerText.enabled = true;

        Working = true;

        LockManager.SetLock(this, true);
    }

    public void EndTimer()
    {
        timerText.text = "";

        timerText.enabled = false;
        LockManager.SetLock(this, false);

        Working = false;
    }

    private void SetTime(int new_hour, int new_minute, int new_second)
    {
        Hour = new_hour;
        Minute = new_minute;
        Second = new_second;
        Millisecond = 1f;
    }

    private void SetTime(int new_hour, int new_minute, int new_second, float new_millisecond)
    {
        Hour = new_hour;
        Minute = new_minute;
        Second = new_second;
        Millisecond = (float)Math.Round(new_millisecond);
    }

    public TimeSpan GetTime()
    {
        return new TimeSpan(Hour, Minute, Second);
    }

    public void TimerTick()
    {
        Millisecond = 1f;
        Second -= 1;

        if (Second < 0)
        {
            Second += 60;
            Minute -= 1;

            if (Minute < 0)
            {
                Minute += 60;
                Hour -= 1;

                if (Hour < 0)
                {
                    EndTimer();
                    return;
                }
            }
        }

        UpdateTimerText();
    }

    private void UpdateTimerText()
    {
        if (timerText) timerText.text = string.Format("{0:D2}:{1:D2}:{2:D2}", Hour, Minute, Second);
    }

    #endregion // Timer
}