public interface IButton
{
    bool Pressed { get; set; }
    bool Locked { get; set; }

    Timer LockTimer { get; set; }

    void Event();
}