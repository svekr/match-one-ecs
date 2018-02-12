using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using Entitas;

public class FieldShiftSystem : ReactiveSystem<GameEntity> {

    private GameContext _context;

    public FieldShiftSystem(Contexts contexts) : base(contexts.game) {
        _context = contexts.game;
    }

    protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context) {
        //return context.CreateCollector(GameMatcher.FieldElement.Removed);
        return context.CreateCollector(GameMatcher.FieldElement);
    }

    protected override bool Filter(GameEntity entity) {
        return true;
    }

    protected override void Execute(List<GameEntity> entities) {
        FieldComponent field = _context.field;
        for (int i = 0; i < field.width; i++) {
            for (int j = 0; j < field.height; j++) {
                foreach (GameEntity entity in _context.GetEntities(GameMatcher.Position)) {
                    if (entity.isMovable) {
                        int nextPos = GetNextPos(entity.position.x, entity.position.y);
                        if (nextPos != entity.position.y) {
                            entity.ReplacePosition(entity.position.x, nextPos);
                        }
                    }
                }
            }
        }
    }

    private int GetNextPos(int x, int y) {
        int r = y - 1;
        while (r >= 0 && GetMatchPosEntitiesCount(x, y) == 0) {
            r--;
        }
        return (r + 1);
    }

    private int GetMatchPosEntitiesCount(int x, int y) {
        int result = 0;
        foreach (GameEntity entity in _context.GetEntities(GameMatcher.Position)) {
            if (entity.position.x == x && entity.position.y == y) {
                result++;
            }
        }
        return result;
    }
}
