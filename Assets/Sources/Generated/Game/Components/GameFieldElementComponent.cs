//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentEntityGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public partial class GameEntity {

    static readonly FieldElementComponent fieldElementComponent = new FieldElementComponent();

    public bool isFieldElement {
        get { return HasComponent(GameComponentsLookup.FieldElement); }
        set {
            if (value != isFieldElement) {
                var index = GameComponentsLookup.FieldElement;
                if (value) {
                    var componentPool = GetComponentPool(index);
                    var component = componentPool.Count > 0
                            ? componentPool.Pop()
                            : fieldElementComponent;

                    AddComponent(index, component);
                } else {
                    RemoveComponent(index);
                }
            }
        }
    }
}

//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentMatcherGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public sealed partial class GameMatcher {

    static Entitas.IMatcher<GameEntity> _matcherFieldElement;

    public static Entitas.IMatcher<GameEntity> FieldElement {
        get {
            if (_matcherFieldElement == null) {
                var matcher = (Entitas.Matcher<GameEntity>)Entitas.Matcher<GameEntity>.AllOf(GameComponentsLookup.FieldElement);
                matcher.componentNames = GameComponentsLookup.componentNames;
                _matcherFieldElement = matcher;
            }

            return _matcherFieldElement;
        }
    }
}
