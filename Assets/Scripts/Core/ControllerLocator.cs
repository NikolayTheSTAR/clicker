using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Object = UnityEngine.Object;

public class ControllerLocator : MonoBehaviour, IServiceLocator
{
    private Dictionary<Type, IService> container;

    public T GetService<T>()
    {
        try
        {
            return (T)container[typeof(T)];
        }
        catch (KeyNotFoundException)
        {
            Debug.LogFormat("Service {0} is not registered", typeof(T).ToString());
            Debug.LogFormat("Creating {0}...", typeof(T).ToString());
            return (T)AddService<T>();
        }
    }

    private IService AddService<T>()
    {
        try
        {
            IServiceBuilder serviceBuilder = new ServiceBuilder();
            container.Add(typeof(T), serviceBuilder.Create<T>());
        }
        catch (Exception e)
        {
            throw e;
        }
        return container[typeof(T)];
    }

    private void Start()
    {
        DontDestroyOnLoad(this);

        container = new Dictionary<Type, IService>();

        try
        {
            AddService<IUIController>();
            AddService<IDataController>();
            AddService<ITimerController>();
            AddService<IBonusController>();

            foreach (var service in container)
                Debug.LogFormat("Service {0} successfuly created", service.Value.name);
        }
        catch (Exception e)
        {
            throw e;
        }
        
        foreach (var service in container)
            service.Value.Init(this);
    }
}

[System.Serializable]
public class ServiceBuilder : IServiceBuilder
{
    public IService Create<T>()
    {
        object service = typeof(T);
        GameObject uiController = Object.Instantiate(Resources.Load<GameObject>("Services/" + service.ToString()));
        uiController.name = service.ToString();
        return uiController.GetComponent<IService>();
    }
}