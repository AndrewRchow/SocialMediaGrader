namespace Memeni.Test.Core
{
    public abstract class DependencyInjectionServiceLocator<TContainer> : IServiceLocator
    {
        // DI container
        protected TContainer Container { get; private set; }

        protected DependencyInjectionServiceLocator(TContainer container)
        {
            Container = container;
        }

        public virtual T Get<T>()
        {
            return Get<T>(Container);
        }

        // Get service instance based on container specific logic
        protected abstract T Get<T>(TContainer container);
    }
}
