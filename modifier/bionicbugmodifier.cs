using MiraAPI.Modifiers.Types;
using Pixeladditions.Patches;
using UnityEngine;

namespace Pixeladditions.Modifiers;

public sealed class BionicBugarrowmodifier(PlayerControl owner) : TimedModifier
{
    public override float Duration => 1f;
    public override bool AutoStart => false;
    public override string ModifierName => "Tracker Map";
    public override bool Unique => false;
    public override bool HideOnUi => true;

    public PlayerControl Owner { get; set; } = owner;
    public Vector3 LastKnownPosition { get; private set; }

    public override void OnActivate()
    {
        LastKnownPosition = Player.transform.position;
    }

    public override void OnDeath(DeathReason reason)
    {
        ModifierComponent!.RemoveModifier(this);
    }

    public override void FixedUpdate()
    {
        base.FixedUpdate();

        if (!Player)
        {
            ModifierComponent!.RemoveModifier(this);
            return;
        }

        LastKnownPosition = Player.transform.position;
    

        if (Owner.AmOwner && MapBehaviour.Instance && MapBehaviour.Instance.gameObject.activeSelf &&
            TrackerMapPatch.TrackerIcons.TryGetValue(Player.PlayerId, out var icon) && icon != null)
        {
            var location = LastKnownPosition / ShipStatus.Instance.MapScale;
            location.z = -1.99f;
            icon.transform.localPosition = location;
        }
    }
}