using Siwei;

namespace Peixi
{
    public interface IInGameUIComponents
    {
        IInventoryGui InventoryGui { get; }
        PlayerPropertyHUD PlayerPropertyHUD { get; }
        IChatBubble ChatBubble { get; }
    }
}
