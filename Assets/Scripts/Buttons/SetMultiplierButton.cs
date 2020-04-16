using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetMultiplierButton : Button
{
    #region Private

    [SerializeField] private float multiplierForce;
    [SerializeField] private AddCurrencyButton multiplyingButton;

    #endregion // Private

    public override ButtonTypes Type { get; set; } = ButtonTypes.MultiplierX2;

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
        bonusController.DoBonus(BonusTypes.Multiplier, multiplyingButton, (int)multiplierForce);
        LockManager.SetLock(this, true);
    }

    public override void EndEvent()
    {
        bonusController.RemoveBonus(BonusTypes.Multiplier, multiplyingButton);
    }
}