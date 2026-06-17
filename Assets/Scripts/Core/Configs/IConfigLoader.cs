namespace Core.Configs
{
    public interface IConfigLoader
    {
        T Load<T>() where T : new();
    }
}