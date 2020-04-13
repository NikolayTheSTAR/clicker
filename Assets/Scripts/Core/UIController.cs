using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour, IUIController
{
    [SerializeField] private Text softCurrency;
    [SerializeField] private Text donateCurrency;

    private IServiceLocator ServiceLocator { get; set; }
    
    public void Init(IServiceLocator serviceLocator)
    {
        ServiceLocator = serviceLocator;
        DontDestroyOnLoad(this);
    }

    public void UpdateCurrency(ICurrencyData data)
    {
        if (softCurrency) softCurrency.text = data.SoftCurrency.ToString();
        if (donateCurrency) donateCurrency.text = data.DonateCurrency.ToString();
    }
}