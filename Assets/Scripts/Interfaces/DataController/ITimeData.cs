public interface ITimeData
{
    /// <summary>
    /// Время завершения таймера блокировки кнопки +100
    /// </summary>
    string UnlockTimeAdd100 { get; set; }

    /// <summary>
    /// Время завершения таймера блокировки кнопки X2
    /// </summary>
    string UnlockTimeX2 { get; set; }
}