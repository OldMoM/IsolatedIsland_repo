using UniRx;

namespace Peixi
{
    public interface IInGameUIComponentsInterface
    {
        IInventoryGui InventoryGui { get; }
        PlayerPropertyHUD PlayerPropertyHUD { get; }
    }
}
