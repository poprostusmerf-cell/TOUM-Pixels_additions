using MiraAPI.GameOptions;
using MiraAPI.Utilities;
using MiraAPI.GameOptions.Attributes;
using MiraAPI.GameOptions.OptionTypes;
using Pixeladditions.Roles;
using Pixeladditions.Roles.Impostor;
using Pixeladditions.Roles;

namespace Pixeladditions.Options;

public sealed class StakeHolderoptions : AbstractOptionGroup<Devil>
{
    public override string GroupName => "StakeHolder";

    [ModdedNumberOption("Number of bets to win", 1f, 5f, 1f, MiraNumberSuffixes.None)]
    public float betstowin { get; set; } = 3f;



}