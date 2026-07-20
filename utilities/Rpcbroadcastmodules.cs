using System;
using Reactor.Networking.Attributes; 
using MiraAPI.Utilities;              

using UnityEngine;
using Pixeladditions.assets;
using Il2CppSystem.Runtime.InteropServices;

namespace Pixeladditions.networking
{
public static class RpcBroadcastModules
{
[MethodRpc((uint)Pixeladditions.RpcCalls.BroadcastNotification)]
public static void RpcBroadcastNotification(PlayerControl sender, string message, byte SpriteId)
{
    var Sprite = SpriteId switch
    {
        0 => Pixelassets.soulsnatch.LoadAsset(),
        1 => Pixelassets.soulsnatch2.LoadAsset(),
        _ => null,
    };
        
        Helpers.CreateAndShowNotification(
        $"<b>{message}</b>",
        Color.white,
        new Vector3(0f, 1f, -20f),
        spr: Sprite).AdjustNotification();
}

[MethodRpc((uint)Pixeladditions.RpcCalls.PrivateNotification)]
public static void RpcPrivateNotification(PlayerControl target, string message)
{
    if (PlayerControl.LocalPlayer != target)
    {
        return; // not the intended recipient, do nothing
    }

    var notif = Helpers.CreateAndShowNotification(
        $"<b>{message}</b>",
        Color.white,
        new Vector3(0f, 1f, -20f),
        spr: Pixelassets.soulsnatch.LoadAsset());

    notif.AdjustNotification();
}
}}