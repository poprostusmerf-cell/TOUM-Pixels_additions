﻿using System.Linq;
using BepInEx;
using Reactor.Utilities;
using BepInEx.Configuration;
using BepInEx.Unity.IL2CPP;
using HarmonyLib;
using MiraAPI.PluginLoading;
using MiraAPI;
using Reactor;
using Reactor.Networking;
using Reactor.Networking.Attributes;
using Pixeladditions.Patches;

namespace Pixeladditions;

[BepInAutoPlugin("mira.Pixeladditions", "Pixeladditions")]
[BepInProcess("Among Us.exe")]
[BepInDependency(ReactorPlugin.Id)]
[BepInDependency(MiraApiPlugin.Id)]
[ReactorModFlags(ModFlags.RequireOnAllClients)]
public partial class Pixeladditions : BasePlugin, IMiraPlugin
{
    public Harmony Harmony { get; } = new(Id);
    public string OptionsTitleText => "Mira API\nPixeladditions";
    public ConfigFile GetConfigFile() => Config;
    public override void Load(
    )
    {
//  changeees!!     ExampleEventHandlers.Initialize();
         ReactorCredits.Register<Pixeladditions>(ReactorCredits.AlwaysShow);
        ReactorCredits.Register("Pixeladditions", "1.6.8", false,
    location => location == ReactorCredits.Location.MainMenu);
    PixeladditionsCredits.Initialize();
        Harmony.PatchAll();
    }
public enum RpcCalls : uint
{
    GrantRoleAbility = 100, 
    BroadcastNotification = 101,

    PrivateNotification = 102,
}

}