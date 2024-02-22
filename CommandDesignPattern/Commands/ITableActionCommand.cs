using Microsoft.AspNetCore.Mvc;

namespace CommandDesignPattern.Commands
{
    public interface ITableActionCommand
    {
        IActionResult Execute();
    }
}
