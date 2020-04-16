public interface IButton
{
    ButtonTypes Type { get; set; }

    bool Pressed { get; set; }
    bool Locked { get; set; }

    void Event();
}