using System.Linq.Expressions;

namespace SharedKernal.Application.Abstractions;

public interface IBackgroundJobService
{
    // Fire and forget
    string Enqueue(Expression<Func<Task>> methodCall);

    // Delayed execution
    string Schedule(Expression<Func<Task>> methodCall, TimeSpan delay);

    // Recurring job
    void AddOrUpdateRecurring(string jobId, Expression<Func<Task>> methodCall, string cronExpression);
}
