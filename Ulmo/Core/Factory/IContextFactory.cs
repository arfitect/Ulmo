using Ulmo.Core.EFContext;

namespace Ulmo.Core.Factory
{
    public interface IContextFactory
    {
        IDatabaseContext DbContext { get; }
    }
}
