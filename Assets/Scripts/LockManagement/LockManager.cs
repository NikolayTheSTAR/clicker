public abstract class LockManager
{
    public static void SetLock(IButton button, bool value)
    {
        if (value && button.LockTimer)
        {
            button.LockTimer.StartTimer();
            button.Locked = true;
        }
    }

    public static void SetLock(Timer timer, bool value)
    {
        IButton button;

        button = timer.GetComponentInParent<IButton>();
        button.Locked = value;
    }
}