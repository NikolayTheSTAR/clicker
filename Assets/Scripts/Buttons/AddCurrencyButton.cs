using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddCurrencyButton : Button
{
    #region Properties

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

    #endregion // Private

    public override ButtonTypes Type { get; set; } = ButtonTypes.AddCurrency100;

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

    public override void Event()
    {
        bonusController.DoBonus(BonusTypes.AddCurrency, (int)(addedCurrencyCount * multiplierForce));
        LockManager.SetLock(this, true);
    }
}