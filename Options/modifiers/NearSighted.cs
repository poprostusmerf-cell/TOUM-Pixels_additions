using MiraAPI.GameOptions;
using MiraAPI.Utilities;
using MiraAPI.GameOptions.Attributes;
using MiraAPI.GameOptions.OptionTypes;
using Pixeladditions.Roles;
using Pixeladditions.Roles.Impostor;
using Pixeladditions.Modifiers;

namespace Pixeladditions.Options;

public sealed class NearSightedoptions : AbstractOptionGroup<Devil>
{
    public override string GroupName => "NearSightedoptions";
    public override bool ShowInModifiersMenu => true;

    [ModdedNumberOption("ExtensionModifierCluelessAmount", 0, 15)]
    public int NearSightedamount { get; set; } = 0;

    public ModdedNumberOption Nearisghtedchance { get; } =
        new("ExtensionModifierCluelessChance", 50f, 0, 100f, 10f, MiraNumberSuffixes.Percent)
        {
            Visible = () => OptionGroupSingleton<NearSightedoptions>.Instance.NearSightedamount > 0
        };

}