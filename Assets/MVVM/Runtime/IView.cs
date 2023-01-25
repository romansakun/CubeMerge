namespace MVVM.Runtime
{
    public interface IView<T> where T : IViewModel
    {
        void Init(T viewModel);
    }
}