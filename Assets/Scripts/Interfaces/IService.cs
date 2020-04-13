public interface IService
{
    string name { get; set; }
    void Init(IServiceLocator serviceLocator);
}