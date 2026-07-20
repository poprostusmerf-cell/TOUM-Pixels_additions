using MiraAPI.Modifiers.Types;
using Pixeladditions.Options;
using MiraAPI.GameOptions;
using MiraAPI.Modifiers;
using MiraAPI.Utilities.Assets;
using TownOfUs.Assets;
using TownOfUs.Extensions;
using TownOfUs.Modifiers.Game;
using TownOfUs.Modules.Localization;
using TownOfUs.Modules.Wiki;
using TownOfUs.Utilities;
using UnityEngine;


namespace Pixeladditions.Modifiers;

public sealed class NearSighted : UniversalGameModifier
{
    public override string ModifierName => "NearSighted";
    public override string IntroInfo => "You probably need glasses!";

    public override bool HideOnUi => false;

    public override int GetAssignmentChance() => OptionGroupSingleton<NearSightedoptions>.Instance.Nearisghtedchance;
    public override int GetAmountPerGame() => OptionGroupSingleton<NearSightedoptions>.Instance.NearSightedamount;
    public float VisionPerc { get; set; } = 0.5f;
}
