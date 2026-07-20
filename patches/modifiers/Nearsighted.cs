using HarmonyLib;
using MiraAPI.Modifiers;
using Pixeladditions.Modifiers;

namespace Pixeladditions.Patches;

[HarmonyPatch(typeof(ShipStatus), nameof(ShipStatus.CalculateLightRadius))]
public static class HalfVisionPatch
{
    [HarmonyPostfix]
    [HarmonyPriority(Priority.Low)] // run after TOU-Mira's own VisionPatch so we scale their final result
    public static void Postfix(NetworkedPlayerInfo player, ref float __result)
    {
        if (player?.Object != null && player.Object.HasModifier<NearSighted>())
        {
            var mod = player.Object.GetModifier<NearSighted>()!;
            __result *= mod.VisionPerc;
        }
    }
}
