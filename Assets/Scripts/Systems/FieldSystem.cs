using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Entitas;

public class FieldSystem : ReactiveSystem<GameEntity>, IInitializeSystem {

    public static readonly int FIELD_WIDTH = 8;
    public static readonly int FIELD_HEIGHT = 8;

    private GameContext _context;

    public FieldSystem(Contexts contexts) : base(contexts.game) {
        _context = contexts.game;
    }

    public void Initialize() {
        GameEntity fieldEntity = _context.CreateEntity();
        fieldEntity.AddField(FIELD_WIDTH, FIELD_HEIGHT);

        for (int i = 0; i < FIELD_WIDTH; i++) {
            for (int j = 0; j < FIELD_HEIGHT; j++) {
                CreatePiece(i, j, 7);
            }
        }
    }

    private GameEntity CreatePiece(int x, int y, int colorRange) {
        int colorId = Random.Range(0, colorRange + 1);
        bool isPiece = colorId < 6;
        string resourceName;
        if (isPiece) {
            resourceName = "Piece" + colorId.ToString();
        } else {
            resourceName = "Blocker";
        }
        GameEntity entity = _context.CreateEntity();
        entity.isMovable = isPiece;
        entity.AddPosition(x, y);
        entity.AddResource(resourceName);
        return entity;
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
