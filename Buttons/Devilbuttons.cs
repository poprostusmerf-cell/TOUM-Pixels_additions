using MiraAPI.GameOptions;
using MiraAPI.Networking;
using MiraAPI.Utilities.Assets;
using MiraAPI.Hud;
using Pixeladditions.assets;
using static Pixeladditions.networking.RpcBroadcastModules;
using Pixeladditions.Options;
using Pixeladditions.Roles;
using TownOfUs.Buttons;
using TownOfUs.Networking;
using UnityEngine;
using System.Linq;
using MiraAPI.Keybinds;

namespace Pixeladditions.Buttons;

public sealed class DevourKillButton : TownOfUsKillRoleButton<Devil, PlayerControl>, IKillButton
{
    public override string Name => "Devour";
    public override float Cooldown => OptionGroupSingleton<Deviloptions>.Instance.Devourcooldown;
    public override BaseKeybind Keybind => Keybinds.PrimaryAction;
    public override LoadableAsset<Sprite> Sprite => Pixelassets.DevilDevourButton;

    public override bool CanUse()
    {
        // gate the entire button behind the unlock condition, same shape as
        // ArsonistIgniteButton's real "count > 0" gate
        return base.CanUse() && Role != null && Role.Unlocked;
    }

    public override PlayerControl? GetTarget()
    {
        return PlayerControl.LocalPlayer.GetClosestLivingPlayer(true, Distance);
    }

    protected override void OnClick()
    {
        if (Target == null)
        {
            Error($"{Name}: Target is null");
            return;
        }

        PlayerControl.LocalPlayer.RpcCustomMurder(Target, MeetingCheck.OutsideMeeting);
        CustomButtonSingleton<DevilLeapButton>.Instance?.ResetCooldownAndOrEffect();
    }
}

public sealed class DevilLeapButton : TownOfUsRoleButton<Devil>, IKillButton
{
    public override string Name => "Leap";
    public override float Cooldown => OptionGroupSingleton<Deviloptions>.Instance.Leapcooldown;
    public override BaseKeybind Keybind => Keybinds.SecondaryAction;
    public override LoadableAsset<Sprite> Sprite => Pixelassets.DevilLeapButton;

    private bool _aiming;
    public override bool CanUse()
    {
        return base.CanUse() && Role != null && Role.Unlocked;
    }

    protected override void OnClick()
    {
        _aiming = !_aiming;

        if (_aiming)
        {
            SetAimingState();
        }
        else
        {
            SetIdleState();
        }
    }

    protected override void FixedUpdate(PlayerControl playerControl)
    {
        base.FixedUpdate(playerControl);

        if (!_aiming || Camera.main == null)
        {
            return;
        }

        if (Input.GetMouseButtonDown(0))
        {
            var worldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            var target = PlayerControl.AllPlayerControls.ToArray()
                .Where(p => p != PlayerControl.LocalPlayer && !p.Data.IsDead && !p.Data.Disconnected)
                .OrderBy(p => Vector2.Distance(worldPos, p.GetTruePosition()))
                .FirstOrDefault(p => Vector2.Distance(worldPos, p.GetTruePosition()) < 2f); // click-tolerance radius

            _aiming = false;
            SetIdleState();

            if (target == null)
            {
                return; // clicked empty space, cancel aim without killing
            }

            PlayerControl.LocalPlayer.RpcCustomMurder(target, MeetingCheck.OutsideMeeting,     createDeadBody: true,   
            teleportMurderer: true);
            Timer = Cooldown;
        }
    }

    private void SetAimingState()
    {
        OverrideSprite(Pixelassets.DevilLeapButton.LoadAsset());
        OverrideName("Cancel");
    }

    private void SetIdleState()
    {
        OverrideSprite(Pixelassets.DevilLeapButton.LoadAsset());
        CustomButtonSingleton<DevourKillButton>.Instance?.ResetCooldownAndOrEffect();
        OverrideName("Leap");
    }
}

public sealed class Enrage : TownOfUsRoleButton<Devil>
{
    public override string Name => "Enrage";
    public override float Cooldown => 5f;
    public override LoadableAsset<Sprite> Sprite => Pixelassets.DevilEnrageButton;
    public override bool Enabled(RoleBehaviour? role)
    {

    return role is Devil && !Role.Unlocked;
    }

    protected override void OnClick()
    {

    //RpcBroadcastNotification(PlayerControl.LocalPlayer, "The devil has been enraged! \nHe can kill now but he will die in 3 meetings", 1);
    Role.Unlocked = true;
    SetActive(false, PlayerControl.LocalPlayer.Data.Role);
    }
}