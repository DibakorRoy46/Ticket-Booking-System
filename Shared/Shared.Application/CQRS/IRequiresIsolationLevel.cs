

using System.Transactions;

namespace SharedKernal.Application.CQRS;

public interface IRequiresIsolationLevel
{
    IsolationLevel IsolationLevel { get; }
}
