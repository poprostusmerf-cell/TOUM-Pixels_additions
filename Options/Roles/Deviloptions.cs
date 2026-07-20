using MiraAPI.GameOptions;
using MiraAPI.Utilities;
using MiraAPI.GameOptions.Attributes;
using MiraAPI.GameOptions.OptionTypes;
using Pixeladditions.Roles;
using Pixeladditions.Roles.Impostor;

namespace Pixeladditions.Options;

public sealed class Deviloptions : AbstractOptionGroup<Devil>
{
    public override string GroupName => "Devil";

    [ModdedNumberOption("Devil kill cooldown", 5f, 30f, 5f, MiraNumberSuffixes.Seconds)]
    public float Devourcooldown { get; set; } = 20f;

     [ModdedNumberOption("Leap cooldown", 5f, 45f, 5f, MiraNumberSuffixes.Seconds)]
    public float Leapcooldown { get; set; } = 20f;

     [ModdedNumberOption("meetings till death", 1f, 3f, 1f, MiraNumberSuffixes.None)]
    public float meetingstilldeath { get; set; } = 3;


}