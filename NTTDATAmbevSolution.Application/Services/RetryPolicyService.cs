using System;
using System.Threading.Tasks;
using NTTDATAAmbev.Application.Interfaces;
using Polly;
using Polly.Retry;

namespace NTTDATAAmbev.Application.Services
{
    public class RetryPolicyService : IRetryPolicyService
    {
        private readonly AsyncRetryPolicy _retryPolicy;

        public RetryPolicyService()
        {
            // Define a política de retry: 3 tentativas com back‑off exponencial
            _retryPolicy = Policy
                .Handle<Exception>()
                .WaitAndRetryAsync(
                    retryCount: 3,
                    sleepDurationProvider: attempt => TimeSpan.FromSeconds(Math.Pow(2, attempt)),
                    onRetry: (exception, timeSpan, retryCount, context) =>
                    {
                        // Aqui você pode logar cada tentativa, se quiser
                        Console.WriteLine($"[RetryPolicy] Tentativa {retryCount} após {timeSpan.TotalSeconds}s devido a: {exception.Message}");
                    });
        }

        public async Task<T> ExecuteWithRetryAsync<T>(Func<Task<T>> action)
        {
            if (action == null) throw new ArgumentNullException(nameof(action));
            return await _retryPolicy.ExecuteAsync(action);
        }

        public async Task ExecuteWithRetryAsync(Func<Task> action)
        {
            if (action == null) throw new ArgumentNullException(nameof(action));
            await _retryPolicy.ExecuteAsync(action);
        }
    }
}
