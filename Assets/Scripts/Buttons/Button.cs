using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button : MonoBehaviour, IButton
{
    #region Private

    [SerializeField] private Animator anim;
    [SerializeField] private bool locking;
    
    #endregion // Private

    #region Properties

    protected bool pressed;
    public bool Pressed
    {
        get
        {
            return pressed;
        }
        set
        {
            if (anim) anim.SetBool("Pressed", value);

            if (value && locking) Locked = true;

            pressed = value;
        }
    }
    protected bool locked;
    public bool Locked
    {
        get
        {
            return locked;
        }
        set
        {
            if (locking)
            {
                if (anim)
                {
                    anim.SetBool("Pressed", false);
                    anim.SetBool("Locked", value);
                }

                locked = value;
            }
        }
    }

    #endregion // Properties

    public virtual ButtonTypes Type { get; set; }

    protected IBonusController bonusController;

    protected void Start()
    {
        bonusController = GameObject.FindObjectOfType<BonusController>().GetComponent<IBonusController>();
    }

    public virtual void Event()
    {

    }

    public virtual void EndEvent()
    {

    }
}

public enum ButtonTypes
{
    AddCurrency100,
    MultiplierX2
}