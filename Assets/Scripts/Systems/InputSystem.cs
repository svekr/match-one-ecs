using UnityEngine;
using Entitas;

public class InputSystem : IExecuteSystem {

    private GameContext _context;

    public InputSystem(Contexts contexts) {
        _context = contexts.game;
    }

    public void Execute() {
        if (Input.GetMouseButtonDown(0)) {
            RaycastHit2D raycast = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero, 100);
            if (raycast != null && raycast.collider != null && raycast.collider.transform != null) {
                Vector3 raycastPosition = raycast.collider.transform.position;
                var entity = _context.CreateEntity();
                entity.AddInput((int)Mathf.Round(raycastPosition.x), (int)Mathf.Round(raycastPosition.y));
            }
        }
    }
}
