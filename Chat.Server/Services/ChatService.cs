using System;
using Chat.Shared;
using MagicOnion;
using MagicOnion.Server;
using Microsoft.Extensions.Logging;

namespace Chat.Server.Services;

public class ChatService : ServiceBase<IChatService>, IChatService
{
    private readonly ILogger logger;

    public ChatService(ILogger<ChatService> logger)
    {
        this.logger = logger;
    }

        
    public UnaryResult GenerateException(ChatMessage message)
    {
        throw new NotImplementedException();
    }

    public UnaryResult SendReportAsync(ChatMessage message)
    {
        logger.LogDebug($"{message}");
        return UnaryResult.CompletedResult;
    }
}