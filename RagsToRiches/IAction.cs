namespace RagsToRiches
{
    public interface IAction<T>
    {
        T Act(T state);
    }
}