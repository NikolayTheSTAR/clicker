using UnityEngine;
using System.Collections;

public class BonusController : MonoBehaviour, IBonusController
{
    #region Static

    public static BonusController bonusController;

    #endregion // Static

    #region Private

    private Bonus bonus;

    #endregion // Private

    public void DoBonus(BonusTypes bonusType, int value)
    {
        switch (bonusType)
        {
            case BonusTypes.AddCurrency:
                bonus.AddCurrency(value);
                break;
            case BonusTypes.SetMultiplier:
                Debug.LogWarning("Button not found");
                break;
        }
    }

    public void DoBonus(BonusTypes bonusType, Object changeObject, int value)
    {
        switch (bonusType)
        {
            case BonusTypes.AddCurrency:
                bonus.AddCurrency(value);
                break;
            case BonusTypes.SetMultiplier:
                bonus.SetMultiplier((AddCurrencyButton)changeObject, value);
                break;
        }
    }

    public void RemoveBonus(BonusTypes bonusType, Object changeObject)
    {
        switch (bonusType)
        {
            case BonusTypes.AddCurrency:
                Debug.Log("Noimpossible remove bonus AddCurrency");
                break;
            case BonusTypes.SetMultiplier:
                bonus.RemoveMultiplier((AddCurrencyButton)changeObject);
                break;
        }
    }

    public void Init(IServiceLocator serviceLocator)
    {
        bonusController = this;

        bonus = new Bonus();

        DontDestroyOnLoad(this);
    }
}

public class Bonus
{
    public void AddCurrency(int value)
    {
        DataController.dataController.SoftCurrency += value;
    }

    public void SetMultiplier(AddCurrencyButton multiplyingButton, float value)
    {
        multiplyingButton.MultiplierForce = value;
    }

    public void RemoveMultiplier(AddCurrencyButton multiplyingButton)
    {
        multiplyingButton.MultiplierForce = 1;
    }
}

public enum BonusTypes
{
    AddCurrency,
    SetMultiplier
}