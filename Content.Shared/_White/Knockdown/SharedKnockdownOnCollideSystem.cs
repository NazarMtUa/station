using Content.Shared._Goobstation.Heretic.Components;
using Content.Shared._Goobstation.Wizard.Mutate;
using Content.Shared.Projectiles;
using Content.Shared.Standing;
using Content.Shared.Throwing;


namespace Content.Shared._White.Knockdown;

public sealed class SharedKnockdownOnCollideSystem : EntitySystem
{
    [Dependency] private readonly SharedHulkSystem _hulk = default!;
    [Dependency] private readonly SharedLayingDownSystem _layingDown = default!;

    public override void Initialize()
    {
        base.Initialize();

        SubscribeLocalEvent<KnockdownOnCollideComponent, ProjectileHitEvent>(OnProjectileHit);
        SubscribeLocalEvent<KnockdownOnCollideComponent, ThrowDoHitEvent>(OnEntityHit);
    }

    private void OnEntityHit(Entity<KnockdownOnCollideComponent> ent, ref ThrowDoHitEvent args)
    {
        ApplyEffects(args.Target, ent.Comp);
    }

    private void OnProjectileHit(Entity<KnockdownOnCollideComponent> ent, ref ProjectileHitEvent args)
    {
        ApplyEffects(args.Target, ent.Comp);
    }

    private void ApplyEffects(EntityUid target, KnockdownOnCollideComponent component)
    {
        if (TryComp(target, out HulkComponent? hulk))
        {
            _hulk.Roar((target, hulk), 1f);
            return;
        }

        if (HasComp<RustbringerComponent>(target))
            return;

        _layingDown.TryLieDown(target, null, null, component.Behavior);
    }
}
