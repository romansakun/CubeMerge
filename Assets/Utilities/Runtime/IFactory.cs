namespace Utilities.Runtime
{
    public interface IFactory<T>
    {
        T Create(); 
    }

    // public class Factory<T> : IFactory<T>
    // {
    // }
}