using System;
using Reactor.Networking.Attributes; 
using MiraAPI.Utilities;              
using Il2CppInterop.Runtime.Attributes;
using MiraAPI.GameOptions;
using MiraAPI.Patches.Stubs;
using TownOfUs.Roles;
using MiraAPI.Roles;
using TownOfUs.Roles.Neutral;
using TownOfUs.Networking;
using MiraAPI.Networking;
using UnityEngine;
using Pixeladditions.assets;
using Pixeladditions.Options;
using AmongUs.GameOptions;
using MiraAPI.Hud;
using MiraAPI.Modifiers;
using MiraAPI.Utilities.Assets;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TownOfUs.Assets;
using TownOfUs.Extensions;
using TownOfUs.Modules.Localization;
using TownOfUs.Modules.Wiki;
using TownOfUs.Utilities;
using TownOfUs;
using TownOfUs.Buttons;

namespace Pixeladditions.Roles;

public sealed class Devil(IntPtr cppPtr) : NeutralRole(cppPtr), ITownOfUsRole, IWikiDiscoverable, IDoomable
{
    public string RoleName => "Devil";
    public string RoleDescription => "sacrafices must be made";
    public string RoleLongDescription => $"You Have {meetingstilldeath} meetings left to live!";
    public Color RoleColor => Color.gray;
    public ModdedRoleTeams Team => ModdedRoleTeams.Custom;
    public RoleAlignment RoleAlignment => RoleAlignment.NeutralKilling;

    public DoomableType DoomHintType => DoomableType.Relentless;

    public string GetAdvancedDescription()
    {
        return "Makes deals with the devil unlocking the ability to kill but cutting your life short" + MiscUtils.AppendOptionsText(GetType());
    }

    [HideFromIl2Cpp]
    public List<CustomButtonWikiDescription> Abilities => new()
    {
        new(
            "Enrage",
            $"Enrages the Devil allowing him to kill and use his leap abillity, \nbut killing the devil in {OptionGroupSingleton<Deviloptions>.Instance.meetingstilldeath} meetings",
            Pixelassets.DevilEnrageButton),
        new(
            "Leap",
            "Leap towards a player, killing them",
            Pixelassets.DevilLeapButton)
    };

    public CustomRoleConfiguration Configuration => new(this)
    {
   //     CanUseVent = OptionGroupSingleton<ShroudOptions>.Instance.CanVent,
        Icon = Pixelassets.DevilIcon,
        IntroSound = TouAudio.ImpostorIntroSound,
        GhostRole = (RoleTypes)RoleId.Get<NeutralGhostRole>(),
        OptionsScreenshot = TouBanners.NeutralRoleBanner,
        MaxRoleCount = 1,
    };


    private float meetingstilldeath = OptionGroupSingleton<Deviloptions>.Instance.meetingstilldeath;

    // The unlock condition itself — tracked on the role so the button can read it
    public int TasksCompleted { get; set; }
    public bool Unlocked { get; set; } = false;
    
    public override void Deinitialize(PlayerControl targetPlayer)
    {
        RoleBehaviourStubs.Deinitialize(this, targetPlayer);
    }

        public override void OnVotingComplete()
    {
        meetingstilldeath--;
        Helpers.CreateAndShowNotification(
        $"You have </b>{meetingstilldeath}</b> Meetings left to live",
        Color.black,
        new Vector3(0f, 1f, -20f), 
        spr: Pixelassets.soulsnatch.LoadAsset()).AdjustNotification();        
        if (meetingstilldeath <= 0)
        {
        PlayerControl.LocalPlayer.RpcCustomMurder(PlayerControl.LocalPlayer, MeetingCheck.Ignore, createDeadBody: false,   
        teleportMurderer: false);
        }

    }

}
