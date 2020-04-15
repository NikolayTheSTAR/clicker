using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetMultiplierButton : MonoBehaviour, IButton
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
                else BonusController.bonusController.RemoveBonus(BonusTypes.SetMultiplier, multiplyingButton);

                locked = value;
            }
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

    #endregion // Properties

    #region Private

    [SerializeField] private float multiplierForce;
    [SerializeField] private AddCurrencyButton multiplyingButton;
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
        BonusController.bonusController.DoBonus(BonusTypes.SetMultiplier, multiplyingButton, (int)multiplierForce);
    }
}