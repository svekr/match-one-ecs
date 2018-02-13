using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Entitas;
using Entitas.Unity;

public class Util {

    private static Util _instance;

    private int _fieldWidth = 8;
    private int _fieldHeight = 8;
    private Transform _fieldParentTransform;

    Util() {
        if (_instance != null) {
            throw new Exception("Double insantiation of singleton.");
        }
        _instance = this;
    }

    public static Util instance {
        get {
            if (_instance == null) {
                new Util();
            }
            return _instance;
        }
    }

    public void SetupFieldParams(int fieldWidth, int fieldHeight, Transform fieldParent) {
        _fieldWidth = fieldWidth;
        _fieldHeight = fieldHeight;
        _fieldParentTransform = fieldParent;
        if (_fieldParentTransform != null) {
            _fieldParentTransform.position = new Vector3(
                _fieldParentTransform.position.x - _fieldWidth * 0.5f,
                _fieldParentTransform.position.y - _fieldHeight * 0.5f,
                _fieldParentTransform.position.z
                );
        }
    }

    public int fieldWidth {
        get {
            return _fieldWidth;
        }
    }

    public int fieldHeight {
        get {
            return _fieldHeight;
        }
    }

    public Transform fieldParentTransform {
        get {
            return _fieldParentTransform;
        }
    }

    public GameObject CreateFieldElementView(GameEntity entity, GameContext context) {
        GameObject result = null;
        if (entity != null && entity.hasResource) {
            try {
                GameObject resource = Resources.Load<GameObject>(entity.resource.value);
                if (resource != null) {
                    result = GameObject.Instantiate(resource);
                    if (result != null && _fieldParentTransform != null) {
                        result.transform.SetParent(_fieldParentTransform, false);
                    }
                }
            } catch (Exception) {
            }
            if (result != null) {
                entity.AddView(result);
                result.Link(entity, context);
            }
        }
        return result;
    }

    public GameEntity CreatePiece(GameContext context, int x, int y) {
        int colorId = UnityEngine.Random.Range(0, 6);
        string resourceName = "Piece" + colorId.ToString();
        GameEntity entity = CreateFieldElement(context, resourceName, x, y);
        entity.isMovable = true;
        entity.isShifted = false;
        return entity;
    }

    public GameEntity CreateBlocker(GameContext context, int x, int y) {
        GameEntity entity = CreateFieldElement(context, "Blocker", x, y);
        entity.isMovable = false;
        return entity;
    }

    private GameEntity CreateFieldElement(GameContext context, string resourceName, int x, int y) {
        GameEntity entity = context.CreateEntity();
        entity.isFieldElement = true;
        entity.AddPosition(x, y);
        entity.AddResource(resourceName);
        return entity;
    }

}
