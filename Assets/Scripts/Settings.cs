using Entitas.CodeGeneration.Attributes;
using UnityEngine;

[CreateAssetMenu]
[Game, Unique]
public class Settings : ScriptableObject {

    public float moveDuration = 0.25f;
    public float removeDuration = 0.5f;
    public float removeScale = 3f;
}
