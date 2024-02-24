namespace ChainOfResponsibilityDesingPattern.ChainOfResponsibility
{
    public interface IProcessHandler
    {
        IProcessHandler SetNext(IProcessHandler processHandler);

        Object Handle(Object o);
    }
}
