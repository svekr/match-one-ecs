using System.Collections.Generic;
using Entitas;
using Entitas.Unity;
using UnityEngine;

public class AddViewSystem : ReactiveSystem<GameEntity> {

    private Contexts _contexts;

    public AddViewSystem(Contexts contexts) : base(contexts.game) {
        _contexts = contexts;
    }

    protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context) {
        return context.CreateCollector(GameMatcher.Resource);
    }

    protected override bool Filter(GameEntity entity) {
        return entity.hasResource;
    }

    protected override void Execute(List<GameEntity> entities) {
        foreach (GameEntity entity in entities) {
            GameObject prefab = Resources.Load<GameObject>(entity.resource.value);
            if (prefab != null) {
                GameObject view = Object.Instantiate(prefab);
                if (view != null) {
                    entity.AddView(view);
                    view.Link(entity, _contexts.game);
                }
            }
        }
    }
} 
