using System;
using Il2CppInterop.Runtime.Attributes;
using MiraAPI.Modifiers;
using MiraAPI.Patches.Stubs;
using MiraAPI.Roles;
using Pixeladditions.Modifiers;
using TownOfUs.Roles;
using UnityEngine;
using TownOfUs.Assets;
using AmongUs.GameOptions;
using TownOfUs.Roles.Neutral;
using Pixeladditions.assets;
using MiraAPI.GameOptions;
using MiraAPI.Networking;
using Pixeladditions.Options;
using System.Collections.Generic;



namespace Pixeladditions.Roles.Impostor;

public sealed class BionicBug(IntPtr cppPtr) : ImpostorRole(cppPtr), ITownOfUsRole, IWikiDiscoverable, IDoomable
{
    public string RoleName => "Bionic Bug";
    public string RoleDescription => "Track a player's last known location on the map and execute them ";
    public string RoleLongDescription => RoleDescription;
    public Color RoleColor => Color.red;
    public ModdedRoleTeams Team => ModdedRoleTeams.Impostor;
    public RoleAlignment RoleAlignment => RoleAlignment.ImpostorKilling;

    public DoomableType DoomHintType => DoomableType.Relentless;

        public string GetAdvancedDescription()
    {
        return "Place Bionic Bugs on players and execute them when the opportunity comes" + MiscUtils.AppendOptionsText(GetType());
    }


        [HideFromIl2Cpp]
    public List<CustomButtonWikiDescription> Abilities => new()
    {
        new(
            "Bug",
            "Place a bug on a player tracking their location.",
            Pixelassets.BionicBugTargetButton),
        new(
            "Execute",
            "Kill the current tracked target",
            Pixelassets.BionicBugKillButton)
    };

    public CustomRoleConfiguration Configuration => new(this)
    {
        Icon = Pixelassets.BionicBugIcon,
        IntroSound = TouAudio.ImpostorIntroSound,
//        GhostRole = (RoleTypes)RoleId.Get<NeutralGhostRole>(),
        OptionsScreenshot = TouBanners.ImpostorRoleBanner,
        MaxRoleCount = 1,
    };

    public override void Deinitialize(PlayerControl targetPlayer)
    {
        RoleBehaviourStubs.Deinitialize(this, targetPlayer);
        Clear();
    }

    public void Clear()
    {
        var players = ModifierUtils.GetPlayersWithModifier<BionicBugarrowmodifier>(
            [HideFromIl2Cpp](x) => x.Owner == Player);

        foreach (var player in players)
        {
            player.RemoveModifier<BionicBugarrowmodifier>();
        }
    }
}