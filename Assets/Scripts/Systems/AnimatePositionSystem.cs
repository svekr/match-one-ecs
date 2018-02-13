using System.Collections.Generic;
using UnityEngine;
using Entitas;
using DG.Tweening;

public class AnimatePositionSystem : ReactiveSystem<GameEntity> {

    private GameContext _context;

    public AnimatePositionSystem(Contexts contexts) : base(contexts.game) {
        _context = contexts.game;
    }

    protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context) {
        return context.CreateCollector(GameMatcher.Position);
    }

    protected override bool Filter(GameEntity entity) {
        return entity.hasPosition && entity.hasView && entity.isMovable;
    }

    protected override void Execute(List<GameEntity> entities) {
        float moveDuration = 0.25f;
        if (_context.hasSettings) {
            moveDuration = _context.settings.value.moveDuration;
        }
        int maxY = _context.field.height - 1;
        foreach (GameEntity entity in entities) {
            GameObject view = entity.view.value;
            if (view != null) {
                if (entity.position.y == maxY && entity.isShifted) {
                    view.transform.localPosition = new Vector2(entity.position.x, entity.position.y + 1);
                }
                view.transform.DOLocalMove(new Vector2(entity.position.x, entity.position.y), moveDuration);
            }
        }
    }
}
