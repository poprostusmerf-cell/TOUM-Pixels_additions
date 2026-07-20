using HarmonyLib;
using System.Collections.Generic; 
using System.Linq; 
using MiraAPI.Modifiers;
using Pixeladditions.assets;
using Pixeladditions.Modifiers;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Pixeladditions.Patches;

[HarmonyPatch]
public static class TrackerMapPatch
{
    public static readonly Dictionary<byte, GameObject> TrackerIcons = [];

[HarmonyPatch(typeof(MapBehaviour), nameof(MapBehaviour.ShowSabotageMap))]
[HarmonyPatch(typeof(MapBehaviour), nameof(MapBehaviour.ShowCountOverlay))]
[HarmonyPatch(typeof(MapBehaviour), nameof(MapBehaviour.ShowNormalMap))]
[HarmonyPostfix]
public static void Postfix(MapBehaviour __instance)
{
    var mods = ModifierUtils.GetActiveModifiers<BionicBugarrowmodifier>()
        .Where(x => x.Owner.AmOwner);

    var activeIds = new HashSet<byte>();

    foreach (var mod in mods)
    {
        activeIds.Add(mod.Player.PlayerId);

        if (!TrackerIcons.TryGetValue(mod.Player.PlayerId, out var icon) || icon == null)
        {
            icon = Object.Instantiate(__instance.HerePoint.gameObject, __instance.HerePoint.transform.parent);
            var renderer = icon.GetComponent<SpriteRenderer>();
            renderer.sprite = Pixelassets.BionicBugtargetIcon.LoadAsset();
            icon.name = $"Tracker Target {mod.Player.PlayerId} Map Icon";
            TrackerIcons[mod.Player.PlayerId] = icon;
        }

        var location = mod.LastKnownPosition / ShipStatus.Instance.MapScale;
        location.z = -1.99f;
        icon.transform.localPosition = location; // still set here too, so it's correct the instant the map opens
    }

    foreach (var id in TrackerIcons.Keys.Where(id => !activeIds.Contains(id)).ToList())
    {
        if (TrackerIcons[id] != null)
        {
            Object.Destroy(TrackerIcons[id]);
        }
        TrackerIcons.Remove(id);
    }
}
}