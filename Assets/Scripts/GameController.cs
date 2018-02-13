using UnityEngine;
using Entitas;

public class GameController : MonoBehaviour {

    public Settings settings;
    public int fieldWidth = 8;
    public int fieldHeight = 8;
    public Transform fieldParent;

    private Contexts _contexts;
    private Systems _systems;

    private void Start() {
        Util.instance.SetupFieldParams(fieldWidth, fieldHeight, fieldParent);
        _contexts = new Contexts();
        _systems = CreateSystems(_contexts);
        _contexts.game.SetSettings(settings);
        _systems.Initialize();
    }

    private void Update() {
        _systems.Execute();
    }

    private Systems CreateSystems(Contexts contexts) {
        return new Feature("Game")
            .Add(new FieldSystem(contexts))
            .Add(new FieldShiftSystem(contexts))
            .Add(new AddPieceSystem(contexts))
            .Add(new AddViewSystem(contexts))
            .Add(new SetViewPositionSystem(contexts))
            .Add(new AnimatePositionSystem(contexts))
            .Add(new InputSystem(contexts))
            .Add(new InputReactSystem(contexts))
            .Add(new AnimateRemoveSystem(contexts))
            .Add(new RemovePieceSystem(contexts))
            ;
    }
}
