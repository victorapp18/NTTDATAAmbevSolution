using System;
using System.Threading.Tasks;

namespace NTTDATAAmbev.Application.Interfaces
{
    public interface IRetryPolicyService
    {
        Task<T> ExecuteWithRetryAsync<T>(Func<Task<T>> action);
        Task ExecuteWithRetryAsync(Func<Task> action);
    }
}
