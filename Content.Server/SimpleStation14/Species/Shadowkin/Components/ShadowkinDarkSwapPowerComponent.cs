﻿using Content.Server.NPC.Components;
using Robust.Shared.Serialization.TypeSerializers.Implementations.Custom.Prototype.List;
using Robust.Shared.Prototypes;

namespace Content.Server.SimpleStation14.Species.Shadowkin.Components;

[RegisterComponent]
public sealed partial class ShadowkinDarkSwapPowerComponent : Component
{
    [DataField]
    public EntProtoId DarkSwapAction = "ShadowkinDarkSwap";

    [DataField("DarkSwapActionEntity"), AutoNetworkedField]
    public EntityUid? DarkSwapActionEntity;

    /// <summary>
    ///     Factions temporarily deleted from the entity while swapped
    /// </summary>
    [DataField]
    public List<ProtoId<NpcFactionPrototype>> SuppressedFactions = new();

    // /// <summary>
    // ///     Factions temporarily added to the entity while swapped
    // /// </summary>
    // [DataField("factions", customTypeSerializer: typeof(PrototypeIdListSerializer<NpcFactionPrototype>))]
    // public List<ProtoId<NpcFactionPrototype>> AddedFactions = new() { "ShadowkinDarkFriendly" };

    /// <summary>
    ///     If the entity should be sent to the dark
    /// </summary>
    [DataField("invisible")]
    public bool Invisible = true;

    /// <summary>
    ///     If it should be pacified
    /// </summary>
    [DataField("pacify")]
    public bool Pacify = true;

    /// <summary>
    ///     If the entity should dim nearby lights when swapped
    /// </summary>
    [DataField("darken"), ViewVariables(VVAccess.ReadWrite)]
    public bool Darken = true;

    /// <summary>
    ///     How far to dim nearby lights
    /// </summary>
    [DataField("range"), ViewVariables(VVAccess.ReadWrite)]
    public float DarkenRange = 5f;

    /// <summary>
    ///     How fast to refresh nearby light dimming in seconds
    ///     Without this performance would be significantly worse
    /// </summary>
    [ViewVariables(VVAccess.ReadWrite)]
    public float DarkenRate = 0.084f; // 1/12th of a second

}
