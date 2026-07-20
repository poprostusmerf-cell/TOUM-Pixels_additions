using MiraAPI.GameOptions;
using MiraAPI.Utilities;
using MiraAPI.GameOptions.Attributes;
using MiraAPI.GameOptions.OptionTypes;
using Pixeladditions.Roles;
using Pixeladditions.Roles.Impostor;

namespace Pixeladditions.Options;

public sealed class BionicBugoption : AbstractOptionGroup<BionicBug>
{
    public override string GroupName => "Tracker";

    [ModdedNumberOption("TrackerTrackCooldown", 5f, 60f, 5f, MiraNumberSuffixes.Seconds)]
    public float TrackCooldown { get; set; } = 20f;

}