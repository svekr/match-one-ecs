using Entitas.CodeGeneration.Attributes;
using UnityEngine;

[CreateAssetMenu]
[Game, Unique]
public class Settings : ScriptableObject {

    public float distance = 1;
}
