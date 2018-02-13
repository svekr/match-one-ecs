using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Entitas;

public class AddPieceSystem : ReactiveSystem<GameEntity> {

    private GameContext _context;

    public AddPieceSystem(Contexts contexts) : base(contexts.game) {
        _context = contexts.game;
    }

    protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context) {
        return context.CreateCollector(GameMatcher.Destroyed);
    }

    protected override bool Filter(GameEntity entity) {
        return true;
    }

    protected override void Execute(List<GameEntity> entities) {
        int fieldWidth = _context.field.width;
        int maxIndexY = _context.field.height - 1;
        for (int i = 0; i < fieldWidth; i++) {
            if (GetEntityAtPos(i, maxIndexY) == null) {
                Util.instance.CreatePiece(_context, i, maxIndexY).isShifted = true;
            }
        }
    }

    private GameEntity GetEntityAtPos(int x, int y) {
        foreach (GameEntity entity in _context.GetEntities(GameMatcher.AllOf(GameMatcher.Position, GameMatcher.FieldElement))) {
            if (entity.position.x == x && entity.position.y == y) {
                return entity;
            }
        }
        return null;
    }
}
