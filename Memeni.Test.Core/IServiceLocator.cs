namespace Memeni.Test.Core
{
    // Common service locator interface
    public interface IServiceLocator
    {
        T Get<T>();
    }
}
