using System;
using AmongUs.GameOptions;
using Il2CppInterop.Runtime.Attributes;
using MiraAPI.GameOptions;
using MiraAPI.Patches.Stubs;
using MiraAPI.Roles;
using TownOfUs.Roles;
using TownOfUs.Roles.Neutral;
using UnityEngine;
using TownOfUs.Modules;
using MiraAPI.Utilities;
using Reactor.Networking.Attributes;
using TownOfUs.Utilities;
using TownOfUs.Modifiers.Crewmate;
using MiraAPI.Modifiers;
using TownOfUs.Modifiers;
using Pixeladditions.assets;
using MiraAPI.Utilities.Assets;
using Pixeladditions.Modifiers;
using TownOfUs.Assets;
using System.Collections.Generic;
using Pixeladditions.Options;
namespace Pixeladditions.Roles;



public sealed class StakeHolder(IntPtr cppPtr) : NeutralRole(cppPtr), ITownOfUsRole, IWikiDiscoverable, IDoomable
{
    public string RoleName => "StakeHolder";
    public string RoleDescription => $"predict {OptionGroupSingleton<StakeHolderoptions>.Instance.betstowin} Deaths to win";
    public string RoleLongDescription => $"You have predicted {bets_won} deaths \n Current target:{PlayerControl.GetName}";
    public Color RoleColor => Color.magenta;
    public ModdedRoleTeams Team => ModdedRoleTeams.Custom;
    public RoleAlignment RoleAlignment => RoleAlignment.NeutralEvil;

    public DoomableType DoomHintType => DoomableType.Relentless;

    public string GetAdvancedDescription()
    {
        return $"Place bets on player dying in the next round and win {OptionGroupSingleton<StakeHolderoptions>.Instance.betstowin} bets to win the game" + MiscUtils.AppendOptionsText(GetType());
    }

        [HideFromIl2Cpp]
    public List<CustomButtonWikiDescription> Abilities => new()
    {
        new(
            "Target",
            $"Place a bet on that players death. predict {OptionGroupSingleton<StakeHolderoptions>.Instance.betstowin} deaths to win the game",
            Pixelassets.StakeHoldertargetButton)
    };

    public CustomRoleConfiguration Configuration => new(this)
    {
        Icon = Pixelassets.StakeHolderIcon,
        IntroSound = TouAudio.ToppatIntroSound,
        GhostRole = (RoleTypes)RoleId.Get<NeutralGhostRole>(),
        OptionsScreenshot = TouBanners.NeutralRoleBanner,
        MaxRoleCount = 1,
    };


    public bool MetGoal { get; set; } = false;

    public float bets_won{get; set;} = 0;
    public bool MetWinCon => MetGoal;

    public bool WinConditionMet()
    {
        return MetGoal;
    }

    public override bool DidWin(GameOverReason gameOverReason)
    {
        return MetGoal;
    }


private MeetingMenu meetingMenu;

public override void Initialize(PlayerControl player)
{
    RoleBehaviourStubs.Initialize(this, player);

    if (Player.AmOwner)
    {
        meetingMenu = new MeetingMenu(
            this,
            Stakeholdertargetclick,
            MeetingAbilityType.Click,
            Pixelassets.StakeHoldertargetButton, 
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

public void Stakeholdertargetclick(PlayerVoteArea voteArea, MeetingHud __)
{
    var target = GameData.Instance.GetPlayerById(voteArea.TargetPlayerId).Object;

    
    Stakeholdertargetlogic(target);
    
    meetingMenu?.HideButtons();
}

public bool IsExempt(PlayerVoteArea voteArea)
{
    return voteArea?.TargetPlayerId == Player.PlayerId || Player.Data.IsDead || voteArea!.AmDead;
}


public static void Stakeholdertargetlogic(PlayerControl target)
    {
    if (PlayerControl.LocalPlayer.Data.Role is StakeHolder stakeHolder)
    {
        target.RpcAddModifier<StakeHoldertargetmodifier>(PlayerControl.LocalPlayer);
    }
    }
}