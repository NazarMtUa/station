using Content.Server.Ghost.Roles.Components;
using Content.Server.Humanoid.Systems;
using Content.Server.RandomMetadata;
using Content.Server.RoundEnd;
using Content.Shared.Mind;
using Content.Shared.Players;
using Content.Shared.Shuttles.Components;
using Robust.Server.GameObjects;
using Robust.Server.Maps;
using Robust.Server.Player;
using Robust.Shared.Map;
using Robust.Shared.Prototypes;
using Robust.Shared.Utility;

namespace Content.Server._Pirate.Mercenary;

public sealed class MakeAMercSystem : EntitySystem
{
    private const string MapPath = "Maps/Shuttles/Qazmlp/st_merc.yml";
    [ValidatePrototypeId<EntityPrototype>] private const string SpawnerPrototypeId = "Mercenary";
    [Dependency] private readonly IEntityManager _entManager = default!;
    [Dependency] private readonly MapLoaderSystem _map = default!;
    [Dependency] private readonly IMapManager _mapManager = default!;
    [Dependency] private readonly IPlayerManager _playerManager = default!;
    [Dependency] private readonly RandomHumanoidSystem _randomHumanoidSystem = default!;
    [Dependency] private readonly RandomMetadataSystem _randomMetadataSystem = default!;
    [Dependency] private readonly RoundEndSystem _roundEndSystem = default!;

    public void MakeAnMerc(EntityUid entity)
    {
        _playerManager.TryGetSessionByEntity(entity, out var session);

        if (session is null)
            return;

        var playerCData = session.ContentData();
        if (playerCData == null)
            return;

        var mindSystem = _entManager.System<SharedMindSystem>();
        var metadata = _entManager.GetComponent<MetaDataComponent>(entity);
        var mind = playerCData.Mind ?? mindSystem.CreateMind(session.UserId, metadata.EntityName);

        var shuttleMap = _mapManager.CreateMap();
        var options = new MapLoadOptions
        {
            LoadMap = true
        };

        if (!_map.TryLoad(shuttleMap, MapPath, out var grids, options))
            return;

        var shuttleMapId = _mapManager.GetMapEntityId(shuttleMap);
        var shuttleMapComponent = EnsureComp<FTLDestinationComponent>(shuttleMapId);
        shuttleMapComponent.Enabled = true;
        // shuttleMapComponent.RequireCoordinateDisk = true;
        _entManager.Dirty(shuttleMapId, shuttleMapComponent);

        var shuttle = grids.FirstOrNull();
        if (shuttle == null)
            return;

        var spawn = Transform(shuttle.Value).Coordinates;
        var uid = _randomHumanoidSystem.SpawnRandomHumanoid(SpawnerPrototypeId, spawn, metadata.EntityName);
        RemComp<GhostRoleComponent>(uid);
        mindSystem.TransferTo(mind, uid, true);
    }
}