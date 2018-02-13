using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Entitas;
using Entitas.Unity;
using DG.Tweening;

public class AnimateRemoveSystem : ReactiveSystem<GameEntity> {

    private GameContext _context;

    public AnimateRemoveSystem(Contexts contexts) : base(contexts.game) {
        _context = contexts.game;
    }

    protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context) {
        return context.CreateCollector(GameMatcher.Destroyed);
    }

    protected override bool Filter(GameEntity entity) {
        return entity.hasView;
    }

    protected override void Execute(List<GameEntity> entities) {
        float removeDuration = 0.5f;
        float removeScale = 3f;
        if (_context.hasSettings) {
            removeDuration = _context.settings.value.removeDuration;
            removeScale = _context.settings.value.removeScale;
        }

        foreach (GameEntity entity in entities) {
            GameObject view = entity.view.value;
            if (view != null) {
                CircleCollider2D circleCollider2D = view.GetComponent<CircleCollider2D>();
                if (circleCollider2D != null) {
                    circleCollider2D.enabled = false;
                }
                SpriteRenderer spriteRenderer = view.GetComponent<SpriteRenderer>();
                if (spriteRenderer != null) {
                    spriteRenderer.DOFade(0f, removeDuration);
                    spriteRenderer.sortingOrder = 2;
                }
                view.transform.DOScale(removeScale, removeDuration);
                view.Unlink();
                GameObject.Destroy(view, removeDuration);
            }
        }
    }
}
