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
using TownOfUs.Assets;
using MiraAPI.Patches.Stubs;
using TownOfUs.Roles.Neutral;
using System.Collections.Generic;


namespace Pixeladditions.Roles;

public sealed class SoulSnatcher(IntPtr cppPtr) : ImpostorRole(cppPtr), ITownOfUsRole, IWikiDiscoverable, IDoomable
{
    public string RoleName => "Soul Snatcher";
    public string RoleLongDescription => "Badz seally";
    public string RoleDescription => RoleLongDescription;
    public Color RoleColor => Palette.ImpostorRed;
    public ModdedRoleTeams Team => ModdedRoleTeams.Impostor;
    public RoleAlignment RoleAlignment => RoleAlignment.ImpostorSupport;

    public DoomableType DoomHintType => DoomableType.Relentless;

    public bool usedrole{get; set;} = false;
        public string GetAdvancedDescription()
    {
        return "Soul Snatch players and gain buffs based on their Aligment!" + MiscUtils.AppendOptionsText(GetType());
    }

        [HideFromIl2Cpp]
    public List<CustomButtonWikiDescription> Abilities => new()
    {
        new(
            "SoulSnatch",
            "Snatch a players soul gaining a buff based on their role aligment: \n (in this release there are only 2 effect)\nCrewmate Power: gain an extra vote\nCrewmate Investigative: \nCrewmate Support: \nCrewmate Power: \nCrewmate Protective: gain a shield\nNeutral Benign: \nNeutral Evil: \nNeutral Killing: \nNeutral Outlier: ",
            Pixelassets.SoulSnatcherTargetButton)
    };

    public CustomRoleConfiguration Configuration => new(this)
    {
        Icon = Pixelassets.SoulSnatcherIcon,
        IntroSound = TouAudio.ImpostorIntroSound,
//        GhostRole = (RoleTypes)RoleId.Get<NeutralGhostRole>(),
        OptionsScreenshot = TouBanners.ImpostorRoleBanner,
        MaxRoleCount = 2,
    };

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
            Pixelassets.SoulSnatcherTargetButton, 
            null!,        
            IsExempt)
        {
            Position = new Vector3(-0.40f, 0f, -3f) 
        };
    }
}

public override void OnMeetingStart()
{
    RoleBehaviourStubs.OnMeetingStart(this);

    if (Player.AmOwner)
    {
        Error($"passedcheck1");
            meetingMenu.GenButtons(MeetingHud.Instance, Player.AmOwner && !Player.HasDied());
            if (usedrole == true)
            {
                meetingMenu.HideButtons();
                Error($"buttons_hidden");
            }
            

            

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
    usedrole = true;

    meetingMenu?.HideButtons();
}

public bool IsExempt(PlayerVoteArea voteArea)
{
    return voteArea?.TargetPlayerId == Player.PlayerId || Player.Data.IsDead || voteArea!.AmDead;
}

    [MethodRpc((uint)Pixeladditions.RpcCalls.GrantRoleAbility)]
    public static void RpcSnatchSoul(PlayerControl player, PlayerControl target)
    {
            Error($"soulsnatched");
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
                "The target was a </b>Crewmate Power</b>, thus you were given an extra vote",
                Color.red,
                new Vector3(0f, 1f, -20f), 
                spr: Pixelassets.soulsnatch.LoadAsset()).AdjustNotification();

                    break;
                case RoleAlignment.CrewmateInvestigative:


                Helpers.CreateAndShowNotification(
                "The target was a </b>Crewmate Investigative</b>, thus you were given an",
                Color.red,
                new Vector3(0f, 1f, -20f), 
                spr: Pixelassets.soulsnatch.LoadAsset()).AdjustNotification();
                    break;
                case RoleAlignment.CrewmateKilling:
                
                Helpers.CreateAndShowNotification(
                "The target was a </b>Crewmate Killing</b>, thus you were given an",
                Color.red,
                new Vector3(0f, 1f, -20f), 
                spr: Pixelassets.soulsnatch.LoadAsset()).AdjustNotification();
                //pomysl: masz mniejszy cooldown
                    break;
                case RoleAlignment.CrewmateSupport:

                Helpers.CreateAndShowNotification(
                "The target was a </b>Crewmate Support</b>, thus you were given an",
                Color.red,
                new Vector3(0f, 1f, -20f), 
                spr: Pixelassets.soulsnatch.LoadAsset()).AdjustNotification();
                    break;
                case RoleAlignment.CrewmateProtective:
                player.RpcAddModifier<MedicShieldModifier>(player);
                Helpers.CreateAndShowNotification(
                "The target was a </b>Crewmate Protective</b>, thus you were given an medic shield",
                Color.red,
                new Vector3(0f, 1f, -20f), 
                spr: Pixelassets.soulsnatch.LoadAsset()).AdjustNotification();
                    break;
                case RoleAlignment.NeutralBenign:

                Helpers.CreateAndShowNotification(
                "The target was a </b>Neutral Benign</b>, thus you were given an",
                Color.red,
                new Vector3(0f, 1f, -20f), 
                spr: Pixelassets.soulsnatch.LoadAsset()).AdjustNotification();
                    break;
                case RoleAlignment.NeutralEvil:
                
                Helpers.CreateAndShowNotification(
                "The target was a </b>Neutral Evil</b>, thus you were given an",
                Color.red,
                new Vector3(0f, 1f, -20f), 
                spr: Pixelassets.soulsnatch.LoadAsset()).AdjustNotification();
                    break;
                case RoleAlignment.NeutralKilling:

                Helpers.CreateAndShowNotification(
                "The target was a </b>Neutral Killing</b>, thus you were given an",
                Color.red,
                new Vector3(0f, 1f, -20f), 
                spr: Pixelassets.soulsnatch.LoadAsset()).AdjustNotification();
                    break;
                case RoleAlignment.NeutralOutlier:

                Helpers.CreateAndShowNotification(
                "The target was a </b>Neutral Outlier</b>, thus you were given an",
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
            
        }
    }
}