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
            if (anim)
            {
                anim.SetBool("Pressed", false);
                anim.SetBool("Locked", value);
            }

            locked = value;
        }
    }
    [SerializeField] private Timer lockTimer;
    public Timer LockTimer
    {
        get
        {
            return lockTimer;
        }
        set
        {
            lockTimer = value;
        }
    }

    private float multiplierForce = 1;
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
    [SerializeField] private Animator anim;

    #endregion // Private

    #region Unity Methods

    void OnMouseDown()
    {
        if (!locked)
        {
            Pressed = true;
            Event();
        }
    }

    void OnMouseUp()
    {
        if (!locked) Pressed = false;
    }

    #endregion // Unity Methods

    public void Event()
    {
        BonusController.bonusController.DoBonus(BonusTypes.AddCurrency, (int)(addedCurrencyCount * multiplierForce));
        if (lockTimer) LockManager.SetLock(this, true);
    }
}