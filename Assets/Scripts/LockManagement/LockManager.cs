public abstract class LockManager
{
    public static void SetLock(Button button, bool value)
    {
        Timer lockTimer = button.transform.GetComponentInChildren<Timer>();

        if (value && lockTimer)
        {
            lockTimer.StartTimer();
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