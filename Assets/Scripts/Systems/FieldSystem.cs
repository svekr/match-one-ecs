using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Entitas;

public class FieldSystem : ReactiveSystem<GameEntity>, IInitializeSystem {

    private GameContext _context;

    public FieldSystem(Contexts contexts) : base(contexts.game) {
        _context = contexts.game;
    }

    public void Initialize() {
        GameEntity fieldEntity = _context.CreateEntity();
        fieldEntity.AddField(Util.instance.fieldWidth, Util.instance.fieldHeight);

        for (int i = 0; i < Util.instance.fieldWidth; i++) {
            for (int j = 0; j < Util.instance.fieldHeight; j++) {
                bool isPiece = Random.Range(0f, 1f) > 0.15f;
                if (isPiece) {
                    Util.instance.CreatePiece(_context, i, j);
                } else {
                    Util.instance.CreateBlocker(_context, i, j);
                }
            }
        }
    }

    protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context) {
        return context.CreateCollector(GameMatcher.Field);
    }

    protected override bool Filter(GameEntity entity) {
        return entity.hasField;
    }

    protected override void Execute(List<GameEntity> entities) {
    }
}
