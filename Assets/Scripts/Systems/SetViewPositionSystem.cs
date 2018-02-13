using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Entitas;

public class SetViewPositionSystem : ReactiveSystem<GameEntity> {

    public SetViewPositionSystem(Contexts contexts) : base(contexts.game) {
    }

    protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context) {
        return context.CreateCollector(GameMatcher.View);
    }

    protected override bool Filter(GameEntity entity) {
        return entity.hasView && entity.hasPosition;
    }

    protected override void Execute(List<GameEntity> entities) {
        foreach (GameEntity entity in entities) {
            entity.view.value.transform.localPosition = new Vector2(entity.position.x, entity.position.y);
        }
    }
}
