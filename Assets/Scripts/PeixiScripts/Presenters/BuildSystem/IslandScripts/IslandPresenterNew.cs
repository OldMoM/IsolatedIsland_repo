using UniRx;
using System;
using UnityEngine;

namespace Peixi
{
    public static class IslandPresenterNew
    {
        public static void Init(ref IslandModel model)
        {
            var data = model;
            data.durability.Value = 100;

            RegisterEntity(ref model);

            IDisposable[] releaseSequences = new IDisposable[3];

            releaseSequences[0] =
            Observable.Interval(TimeSpan.FromSeconds(1))
                .Where(_ => data.isActive)
                .Subscribe(x =>
                {
                    var durability = data.durability.Value;
                    durability -= 1;
                    durability = Mathf.Clamp(durability, 0, 100);

                    data.durability.Value = durability;
                }).AddTo(model.attachedObject);

            model.durability
                .Where(x=>x == 0)
                .First()
                .Subscribe(x =>
                {
                    data.onIslandDestoryed.OnNext(data.positionInGrid);
                    ReleaseAllSequences(releaseSequences);
                    UnregisterEntity(ref data);
                });

            releaseSequences[1] =
            model.onDayStart.Subscribe(_ => data.isActive = true).AddTo(model.attachedObject);
            releaseSequences[2] =
            model.onDayEnd.Subscribe(_ => data.isActive = false).AddTo(model.attachedObject);

            var onGamePaused = Entity.gameTriggers["onGamePaused"];
            onGamePaused.Subscribe(x =>
            {
                data.isActive = false;
            });

            var onGameResumed = Entity.gameTriggers["onGameResumed"];
            onGameResumed.Subscribe(x =>
            {
                data.isActive = true;
            });
        }

        private static void ReleaseAllSequences(IDisposable[] sequences)
        {
            foreach (var item in sequences)
            {
                item.Dispose();
            }
        }

        private static void RegisterEntity(ref IslandModel model)
        {
            Entity.islandModels.Add(model.positionInGrid, model);
        }
        public static void UnregisterEntity(ref IslandModel model)
        {
            Entity.islandModels.Remove(model.positionInGrid);
        }
    }
}
