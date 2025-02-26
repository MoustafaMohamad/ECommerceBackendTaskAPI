
using ECommerceBackendTaskAPI.Common.Enums;

namespace ECommerceBackendTaskAPI.Features.Common.AppLogs.Commands
{
    public record AddLogCommand(LogLevels Level, string Message) : IRequest<bool>;

    public class AddLogCommandHandler : IRequestHandler<AddLogCommand, bool>
    {
        private readonly ILogger<AddLogCommandHandler> _logger;

        public AddLogCommandHandler(ILogger<AddLogCommandHandler> logger)
        {
            _logger = logger;
        }

        public Task<bool> Handle(AddLogCommand request, CancellationToken cancellationToken)
        {
            switch (request.Level)
            {
                case LogLevels.Information:
                    _logger.LogInformation($"[{DateTime.UtcNow}] {request.Message}");
                    break;
                case LogLevels.Warning:
                    _logger.LogWarning($"[{DateTime.UtcNow}] {request.Message}");
                    break;
                case LogLevels.Error:
                    _logger.LogError($"[{DateTime.UtcNow}] {request.Message}");
                    break;
                case LogLevels.Debug:
                    _logger.LogDebug($"[{DateTime.UtcNow}] {request.Message}");
                    break;
                case LogLevels.Fatal:
                    _logger.LogCritical($"[{DateTime.UtcNow}] {request.Message}");
                    break;
            }

            return Task.FromResult(true);
        }
    }
}
