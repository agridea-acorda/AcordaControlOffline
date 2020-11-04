using CSharpFunctionalExtensions;

namespace Agridea.Acorda.AcordaControlOffline.Shared.ApplicationServices.Commands
{
    public interface ICommand
    {
    }

    public interface ICommandHandler<TCommand>
        where TCommand : ICommand
    {
        Result Handle(TCommand command);
    }
}
