using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Entitas;

public class SetViewPositionSystem : ReactiveSystem<GameEntity> {

    private GameContext _context;

    public SetViewPositionSystem(Contexts contexts) : base(contexts.game) {
        _context = contexts.game;
    }

    protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context) {
        return context.CreateCollector(GameMatcher.View);
    }

    protected override bool Filter(GameEntity entity) {
        return entity.hasView && entity.hasPosition;
    }

    protected override void Execute(List<GameEntity> entities) {
        int dx = (int)(_context.field.width * 0.5f);
        int dy = (int)(_context.field.height * 0.5f);
        foreach (GameEntity entity in entities) {
            entity.view.value.transform.position = new Vector2(entity.position.x - dx, entity.position.y - dy);
        }
    }
}
