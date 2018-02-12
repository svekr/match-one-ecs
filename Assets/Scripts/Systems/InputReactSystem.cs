using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Entitas;

public class InputReactSystem : ReactiveSystem<GameEntity> {

    private GameContext _context;

    public InputReactSystem(Contexts contexts) : base(contexts.game) {
        _context = contexts.game;
    }

    protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context) {
        return context.CreateCollector(GameMatcher.Input);
    }

    protected override bool Filter(GameEntity entity) {
        return entity.hasInput;
    }

    protected override void Execute(List<GameEntity> entities) {
        InputComponent input = null;
        foreach (GameEntity inputEntity in _context.GetEntities(GameMatcher.Input)) {
            input = inputEntity.input;
        }
        int dx = (int)(_context.field.width * 0.5f);
        int dy = (int)(_context.field.height * 0.5f);
        Vector2 v = new Vector2(input.x + dx, input.y + dy);

        foreach (GameEntity entity in _context.GetEntities(GameMatcher.Position)) {
            if (entity.hasPosition) {
                if (entity.position.x == v.x && entity.position.y == v.y) {
                    entity.isDestroyed = true;
                }
            }
        }
    }
}
