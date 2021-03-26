using System;
using UniRx;
using UnityEngine;
namespace Peixi
{
    /// <summary>监视使用消耗品事件，调用UseSupplyService完成完成消耗品效果</summary>
    public class SuppleUseAgent 
    {
        private IObservable<string> onItemUsed;

        public SuppleUseAgent(IObservable<string> itemUsedEvent)
        {
            onItemUsed = itemUsedEvent;
            onItemUsed
                .Subscribe(x =>
                {
                    UseSupplyService.UseApple(InterfaceArichives.Archive.IPlayerPropertySystem);
                });
        }
    }
}
