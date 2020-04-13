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
                else RemoveMulti();

                locked = value;
            }
        }
    }

    #endregion // Properties

    #region Private

    [SerializeField] private float multiplierForce;
    [SerializeField] private AddCurrencyButton multiplyingButton;
    [SerializeField] private LockTimer lockTimer;
    [SerializeField] private Animator anim;

    #endregion // Private

    #region Unity Methods

    void OnMouseDown()
    {
        if (!locked)
        {
            Pressed = true;
            SetMulti();
        }
    }

    void OnMouseUp()
    {
        if (!locked) Pressed = false;
    }

    #endregion // Unity Methods

    private void SetMulti()
    {
        multiplyingButton.MultiplierForce = multiplierForce;
    }

    private void RemoveMulti()
    {
        multiplyingButton.MultiplierForce = 1;
    }
}