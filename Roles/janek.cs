using System.Text;
using Il2CppInterop.Runtime.Attributes;
using MiraAPI.GameOptions;
using MiraAPI.Hud;
using MiraAPI.Modifiers;
using MiraAPI.Roles;
using Reactor.Networking.Attributes;
using TownOfUs.Buttons.Crewmate;
using TownOfUs.Modifiers.Game.Alliance;
using TownOfUs.Modules;
using TownOfUs.Options.Roles.Crewmate;
using UnityEngine;
using MiraAPI.Networking;
using MiraAPI.Patches.Stubs;
using MiraAPI.Utilities;
using Reactor.Utilities.Extensions;
using TownOfUs.Modifiers;
using TownOfUs.Modifiers.Crewmate;
using TownOfUs.Networking;
using Pixeladditions.assets;
using TownOfUs.Utilities;

namespace Pixeladditions.impostor.soulsnatcher;

public class SoulSnatcher : ImpostorRole, ICustomRole
{
    public string RoleName => "Soul snatcher";
    public string RoleLongDescription => "Badz seally";
    public string RoleDescription => RoleLongDescription;
    public Color RoleColor => Palette.ImpostorRed;
    public ModdedRoleTeams Team => ModdedRoleTeams.Impostor;

    public CustomRoleConfiguration Configuration => new CustomRoleConfiguration(this)
    {
        MaxRoleCount = 2,
    };
    

public static void soulsnatch(PlayerControl player, PlayerControl target)
    {
        if (player.Data.Role is not SoulSnatcher)
        {
//            Error("RpcRemember - Invalid amnesiac");
            return;
        }
        //target.GetRoleWhenAlive()
    var targetrole = target.GetRoleWhenAlive();
    var targetalligment = targetrole.GetRoleAlignment();
    }
}