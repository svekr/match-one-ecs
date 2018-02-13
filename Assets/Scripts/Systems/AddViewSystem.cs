using System.Collections.Generic;
using Entitas;
using Entitas.Unity;
using UnityEngine;

public class AddViewSystem : ReactiveSystem<GameEntity> {

    private GameContext _context;

    public AddViewSystem(Contexts contexts) : base(contexts.game) {
        _context = contexts.game;
    }

    protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context) {
        return context.CreateCollector(GameMatcher.Resource);
    }

    protected override bool Filter(GameEntity entity) {
        return entity.hasResource;
    }

    protected override void Execute(List<GameEntity> entities) {
        foreach (GameEntity entity in entities) {
            Util.instance.CreateFieldElementView(entity, _context);
        }
    }
} 
