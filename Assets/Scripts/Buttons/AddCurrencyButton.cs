using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddCurrencyButton : MonoBehaviour, IButton
{
    #region Properties

    private bool pressed;
    public bool Pressed
    {
        get
        {
            return pressed;
        }
        set
        {
            if (anim) anim.SetBool("Pressed", value);

            if (value) Locked = true;

            pressed = value;
        }
    }
    private bool locked;
    public bool Locked
    {
        get
        {
            return locked;
        }
        set
        {
            if (lockTimer)
            {
                if (anim)
                {
                    anim.SetBool("Pressed", false);
                    anim.SetBool("Locked", value);
                }

                if (value) lockTimer.StartTimer();

                locked = value;
            }
        }
    }

    private float multiplierForce = 1f;
    public float MultiplierForce
    {
        get
        {
            return multiplierForce;
        }
        set
        {
            if (value < 1) value = 1;

            multiplierForce = value;
        }
    }

    #endregion // Properties

    #region Private

    [SerializeField] private int addedCurrencyCount;
    [SerializeField] private LockTimer lockTimer;
    [SerializeField] private Animator anim;

    #endregion // Private

    #region Unity Methods

    void OnMouseDown()
    {
        if (!locked)
        {
            Pressed = true;
            AddCurrency();
        }
    }

    void OnMouseUp()
    {
        if (!locked) Pressed = false;
    }

    #endregion // Unity Methods

    private void AddCurrency()
    {
        DataController.dataController.SoftCurrency += (int)(addedCurrencyCount * multiplierForce);
    }
}