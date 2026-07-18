using AmongUs.GameOptions;
using System;
using TownOfUs.Modules;
using Il2CppInterop.Runtime.Attributes;
using MiraAPI.GameOptions;
using MiraAPI.Roles;
using MiraAPI.Utilities;
using Reactor.Networking.Attributes;
using TownOfUs.Roles;
using TownOfUs.Utilities;
using UnityEngine;
using TownOfUs.Modifiers.Crewmate;
using MiraAPI.Modifiers;
using TownOfUs.Modifiers;
using Pixeladditions.assets;
using MiraAPI.Utilities.Assets;
using MiraAPI.Patches.Stubs;


namespace Pixeladditions.Roles;

public sealed class SoulSnatcher(IntPtr cppPtr) : ImpostorRole(cppPtr), ITownOfUsRole
{
    public string RoleName => "Soul Snatcher";
    public string RoleLongDescription => "Badz seally";
    public string RoleDescription => RoleLongDescription;
    public Color RoleColor => Palette.ImpostorRed;
    public ModdedRoleTeams Team => ModdedRoleTeams.Impostor;
    public RoleAlignment RoleAlignment => RoleAlignment.ImpostorSupport;

    public CustomRoleConfiguration Configuration => new(this)
    {
        MaxRoleCount = 2,
    };




// inside SoulSnatcher : ImpostorRole(cppPtr), ITownOfUsRole
private MeetingMenu meetingMenu;

public override void Initialize(PlayerControl player)
{
    RoleBehaviourStubs.Initialize(this, player);

    if (Player.AmOwner)
    {
        meetingMenu = new MeetingMenu(
            this,
            ClickSnatch,
            MeetingAbilityType.Click,
            Pixelassets.soulsnatch, // active sprite shown on each player's row
            null!,                              // disabled sprite (only needed for Toggle type)
            IsExempt)
        {
            Position = new Vector3(-0.40f, 0f, -3f) // same offset Deputy uses
        };
    }
}

public override void OnMeetingStart()
{
    RoleBehaviourStubs.OnMeetingStart(this);

    if (Player.AmOwner)
    {
        // 2nd arg = should the buttons actually be usable right now
        meetingMenu.GenButtons(MeetingHud.Instance, Player.AmOwner && !Player.HasDied());
    }
}

public override void OnVotingComplete()
{
    RoleBehaviourStubs.OnVotingComplete(this);

    if (Player.AmOwner)
    {
        meetingMenu.HideButtons();
    }
}

public override void Deinitialize(PlayerControl targetPlayer)
{
    RoleBehaviourStubs.Deinitialize(this, targetPlayer);

    if (Player.AmOwner)
    {
        meetingMenu?.Dispose();
        meetingMenu = null!;
    }
}

public void ClickSnatch(PlayerVoteArea voteArea, MeetingHud __)
{
    var target = GameData.Instance.GetPlayerById(voteArea.TargetPlayerId).Object;

    RpcSnatchSoul(PlayerControl.LocalPlayer, target);

    meetingMenu?.HideButtons();
}

public bool IsExempt(PlayerVoteArea voteArea)
{
    return voteArea?.TargetPlayerId == Player.PlayerId || Player.Data.IsDead || voteArea!.AmDead;
}

    [MethodRpc((uint)Pixeladditions.RpcCalls.GrantRoleAbility)]
    public static void RpcSnatchSoul(PlayerControl player, PlayerControl target)
    {
        if (player.Data.Role is not SoulSnatcher)
        {
            Error("RpcSnatchSoul - Invalid soul snatcher");
            return;
        }

        var roleWhenAlive = target.GetRoleWhenAlive();

        if (roleWhenAlive is ITownOfUsRole touRole)
        {
            switch (touRole.RoleAlignment)
            {
                case RoleAlignment.CrewmatePower:
                player.RpcAddModifier<KnightedModifier>();
                Helpers.CreateAndShowNotification(
                "The target was a </b>Crewmate Power</b>, thus u were given an extra vote",
                Color.red,
                new Vector3(0f, 1f, -20f), 
                spr: Pixelassets.soulsnatch.LoadAsset()).AdjustNotification();

                    break;
                case RoleAlignment.CrewmateInvestigative:

                player.RpcAddModifier<MedicShieldModifier>(player);
                Helpers.CreateAndShowNotification(
                "The target was a </b>Crewmate Investigative</b>, thus u were given an",
                Color.red,
                new Vector3(0f, 1f, -20f), 
                spr: Pixelassets.soulsnatch.LoadAsset()).AdjustNotification();
                    break;
                case RoleAlignment.CrewmateKilling:
                
                player.RpcAddModifier<MedicShieldModifier>(player);
                Helpers.CreateAndShowNotification(
                "The target was a </b>Crewmate Killing</b>, thus u were given an",
                Color.red,
                new Vector3(0f, 1f, -20f), 
                spr: Pixelassets.soulsnatch.LoadAsset()).AdjustNotification();
                    break;
                case RoleAlignment.CrewmateSupport:

                player.RpcAddModifier<MedicShieldModifier>(player);
                Helpers.CreateAndShowNotification(
                "The target was a </b>Crewmate Support</b>, thus u were given an",
                Color.red,
                new Vector3(0f, 1f, -20f), 
                spr: Pixelassets.soulsnatch.LoadAsset()).AdjustNotification();
                    break;
                case RoleAlignment.CrewmateProtective:
                player.RpcAddModifier<MedicShieldModifier>(player);
                Helpers.CreateAndShowNotification(
                "The target was a </b>Crewmate Protective</b>, thus u were given an medic shield",
                Color.red,
                new Vector3(0f, 1f, -20f), 
                spr: Pixelassets.soulsnatch.LoadAsset()).AdjustNotification();
                    break;
                case RoleAlignment.NeutralBenign:

                Helpers.CreateAndShowNotification(
                "The target was a </b>Neutral Benign</b>, thus u were given an",
                Color.red,
                new Vector3(0f, 1f, -20f), 
                spr: Pixelassets.soulsnatch.LoadAsset()).AdjustNotification();
                    break;
                case RoleAlignment.NeutralEvil:
                
                Helpers.CreateAndShowNotification(
                "The target was a </b>Neutral Evil</b>, thus u were given an",
                Color.red,
                new Vector3(0f, 1f, -20f), 
                spr: Pixelassets.soulsnatch.LoadAsset()).AdjustNotification();
                    break;
                case RoleAlignment.NeutralKilling:

                Helpers.CreateAndShowNotification(
                "The target was a </b>Neutral Killing</b>, thus u were given an",
                Color.red,
                new Vector3(0f, 1f, -20f), 
                spr: Pixelassets.soulsnatch.LoadAsset()).AdjustNotification();
                    break;
                case RoleAlignment.NeutralOutlier:

                Helpers.CreateAndShowNotification(
                "The target was a </b>Neutral Outlier</b>, thus u were given an",
                Color.red,
                new Vector3(0f, 1f, -20f), 
                spr: Pixelassets.soulsnatch.LoadAsset()).AdjustNotification();
                    break;
                    
                    
                default:

                    break;
            }
        }
        else
        {
            // Vanilla role fallback (Crewmate, Impostor, Scientist, etc.)
            HandleVanillaRole(player, target, roleWhenAlive);
        }
    }
    private static void HandleVanillaRole(PlayerControl player, PlayerControl target, RoleBehaviour role) { /* TODO: fill in effect */ }
}