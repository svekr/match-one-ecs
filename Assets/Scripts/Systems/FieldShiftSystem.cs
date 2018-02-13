using System.Collections.Generic;
using UnityEngine;
using Entitas;

public class FieldShiftSystem : ReactiveSystem<GameEntity> {

    private GameContext _context;
    private List<GameEntity> _shifting = new List<GameEntity>();

    public FieldShiftSystem(Contexts contexts) : base(contexts.game) {
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
        int fieldHeight = _context.field.height;

        for (int i = 0; i < fieldWidth; i++) {
            int j = 0;
            int shift = 0;
            bool blocked = false;
            _shifting.Clear();
            for (j = 0; j < fieldHeight; j++) {
                _shifting.Add(GetEntityAtPos(i, j));
            }
            for (j = 0; j < _shifting.Count; j++) {
                GameEntity entity = _shifting[j];
                if (entity != null) {
                    if (entity.isMovable) {
                        if (!blocked && shift > 0) {
                            entity.isShifted = true;
                            entity.ReplacePosition(entity.position.x, entity.position.y - shift);
                        }
                    } else {
                        shift = 0;
                    }
                    blocked = !entity.isMovable;
                } else {
                    shift++;
                    blocked = false;
                }
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
