using SKitLs.Bots.Telegram.Core.Building;
using SKitLs.Bots.Telegram.Core.Interceptors;
using SKitLs.Bots.Telegram.Core.UpdatesCasting.Signed;
using SKitLs.Bots.Telegram.Stateful.Prototype;

namespace SKitLs.Bots.Telegram.BotProcesses.InputForms
{
    public class InputDataInterceptor : OwnedObject, IUpdateInterceptor<SignedMessageTextUpdate>
    {
        private List<IUserState> DefinedStates { get; set; } = [];

        public string? DebugName { get; set; }

        public bool ShouldIntercept(SignedMessageTextUpdate update)
        {
            if (update.Sender is IStatefulUser stateful)
            {
                return DefinedStates.Contains(stateful.State);
            }
            return false;
        }

        public Task<InterceptorReleaseMode> HandleUpdate(SignedMessageTextUpdate update)
        {
            throw new NotImplementedException();
        }

        public override string ToString() => DebugName ?? nameof(InputDataInterceptor);
    }
}
