using System.Collections.Generic;
using UnityEngine;
using Entitas;

public class RemovePieceSystem : ReactiveSystem<GameEntity> {

    public RemovePieceSystem(Contexts contexts) : base(contexts.game) {
    }

    protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context) {
        return context.CreateCollector(GameMatcher.Destroyed);
    }

    protected override bool Filter(GameEntity entity) {
        return entity.isDestroyed;
    }

    protected override void Execute(List<GameEntity> entities) {
        foreach (GameEntity entity in entities) {
            entity.Destroy();
        }
    }
}
