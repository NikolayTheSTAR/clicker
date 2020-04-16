using UnityEngine;
using System.Collections;

public class BonusController : MonoBehaviour, IBonusController
{
    #region Private

    private Bonus bonus;
    private IServiceLocator ServiceLocator { get; set; }
    private IDataController dataController { get; set; }

    #endregion // Private

    public void DoBonus(BonusTypes bonusType, int value)
    {
        switch (bonusType)
        {
            case BonusTypes.AddCurrency:
                bonus.AddCurrency(value);
                break;
            case BonusTypes.Multiplier:
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
            case BonusTypes.Multiplier:
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
            case BonusTypes.Multiplier:
                bonus.RemoveMultiplier((AddCurrencyButton)changeObject);
                break;
        }
    }

    public void Init(IServiceLocator serviceLocator)
    {
        ServiceLocator = serviceLocator;
        dataController = ServiceLocator.GetService<IDataController>();

        bonus = new Bonus((DataController)dataController);

        DontDestroyOnLoad(this);
    }
}

public class Bonus
{
    private DataController dataController;

    public Bonus(IDataController dataController)
    {
        this.dataController = (DataController)dataController;
    }

    public void AddCurrency(int value)
    {
        dataController.SoftCurrency += value;
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
    Multiplier
}