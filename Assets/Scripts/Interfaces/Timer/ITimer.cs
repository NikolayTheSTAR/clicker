public interface ITimer
{
    bool Working { get; set; }

    int Hour { get; set; }
    int Minute { get; set; }
    int Second { get; set; }

    float Millisecond { get; set; }

    void StartTimer();

    void EndTimer();
}