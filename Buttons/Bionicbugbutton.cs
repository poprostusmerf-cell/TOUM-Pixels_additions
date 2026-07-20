using MiraAPI.GameOptions;
using MiraAPI.Modifiers;
using MiraAPI.Networking;
using MiraAPI.Utilities.Assets;
using Pixeladditions.assets;
using Pixeladditions.Modifiers;
using Pixeladditions.Options;
using Pixeladditions.Roles;
using Pixeladditions.Roles.Impostor;
using System.Linq;
using TownOfUs.Buttons;
using UnityEngine;
using MiraAPI.Keybinds;

namespace Pixeladditions.Buttons;

public sealed class TrackerTrackButton : TownOfUsRoleButton<BionicBug, PlayerControl>
{
    public override string Name => "Track";

    public override BaseKeybind Keybind => Keybinds.SecondaryAction;

    public override float Cooldown => OptionGroupSingleton<BionicBugoption>.Instance.TrackCooldown;
    public override LoadableAsset<Sprite> Sprite => Pixelassets.BionicBugTargetButton;

    public override PlayerControl? GetTarget()
    {
        var tracked = ModifierUtils.GetPlayersWithModifier<BionicBugarrowmodifier>(
            m => m.Owner == PlayerControl.LocalPlayer).FirstOrDefault();

        return tracked ?? PlayerControl.LocalPlayer.GetClosestLivingPlayer(true, Distance);
    }

    protected override void OnClick()
    {
        if (Target == null)
        {
            Error($"{Name}: Target is null");
            return;
        }

        if (Target.HasModifier<BionicBugarrowmodifier>())
        {

            Target.RemoveModifier<BionicBugarrowmodifier>();
            SetTrackState();
            Target.RpcCustomMurder(Target,
    didSucceed: true,
    resetKillTimer: true,
    createDeadBody: true,   
    teleportMurderer: false, 
    showKillAnim: false, 
    playKillSound: true);
        }
        else
        {

            Target.AddModifier<BionicBugarrowmodifier>(PlayerControl.LocalPlayer);
            SetUntrackState();
        }
    }

    private void SetTrackState()
    {
        OverrideSprite(Pixelassets.BionicBugTargetButton.LoadAsset());
        OverrideName("Track");
    }

    private void SetUntrackState()
    {
        OverrideSprite(Pixelassets.BionicBugKillButton.LoadAsset());
        OverrideName("Execute");
    }
}