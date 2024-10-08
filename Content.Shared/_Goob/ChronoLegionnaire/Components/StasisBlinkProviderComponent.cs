using Content.Shared.Actions;
using Robust.Shared.GameStates;
using Robust.Shared.Prototypes;
using Robust.Shared.Serialization.TypeSerializers.Implementations.Custom.Prototype;

namespace Content.Shared.Goobstation.ChronoLegionnaire.Components
{
    /// <summary>
    /// Marks an clothing that will give stasis blink ability to wearer
    /// </summary>
    [RegisterComponent, NetworkedComponent, Access(typeof(SharedStasisBlinkProviderSystem)), AutoGenerateComponentState]
    public sealed partial class StasisBlinkProviderComponent : Component
    {
        /// <summary>
        /// The action blink id.
        /// </summary>
        [DataField]
        public EntProtoId BlinkAction = "ActionChronoBlink";

        [DataField, AutoNetworkedField]
        public EntityUid? BlinkActionEntity;
    }
}
