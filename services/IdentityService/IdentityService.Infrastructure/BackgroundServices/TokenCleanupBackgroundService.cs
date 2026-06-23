using IdentityService.Application.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace IdentityService.Infrastructure.BackgroundServices;

public class TokenCleanupBackgroundService(
    IServiceScopeFactory serviceScopeFactory,
    ILogger<TokenCleanupBackgroundService> logger) : BackgroundService
{
    readonly IServiceScopeFactory  _serviceScopeFactory = serviceScopeFactory;
    readonly ILogger<TokenCleanupBackgroundService> _logger = logger;
    private readonly TimeSpan _period = TimeSpan.FromDays(1);

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        _logger.LogInformation("Фоновой сервис очистки токенов запущен");
        
        using var timer = new PeriodicTimer(_period);

        while (await timer.WaitForNextTickAsync(stoppingToken) && !stoppingToken.IsCancellationRequested)
        {
            try
            {
                using (var scope = _serviceScopeFactory.CreateScope())
                {
                    var tokenRepository = scope.ServiceProvider.GetRequiredService<IRefreshTokenRepository>();
                    await tokenRepository.DeleteExpiredRefreshTokensAsync(stoppingToken);
                }

                _logger.LogInformation("Удаление просроченных токенов успешно завершено");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message, "Произошла ошибка при удлении просреоченнх токенов");
            }
        }
    }
}