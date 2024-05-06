using MagicOnion;

namespace Chat.Shared {

    public interface IChatService : IService<IChatService>
    {
        UnaryResult GenerateException(ChatMessage message);
        UnaryResult SendReportAsync(ChatMessage message);
    }

}