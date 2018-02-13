using Entitas;
using Entitas.CodeGeneration.Attributes;

[Game, Unique]
public class FieldComponent : IComponent {
    public int width;
    public int height;
}
