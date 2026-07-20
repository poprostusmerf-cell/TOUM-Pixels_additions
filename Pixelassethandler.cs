using MiraAPI.Utilities.Assets;
using UnityEngine;
using TownOfUs.Assets;

namespace Pixeladditions.assets
{


public static class Pixelassets
{
	public static LoadableAsset<Sprite> soulsnatch { get; } =
		new LoadableResourceAsset("Pixeladditions.Resources.soulsnatch.png", 100f);
	public static LoadableAsset<Sprite> soulsnatch2 { get; } =
		new LoadableResourceAsset("Pixeladditions.Resources.soulsnatch2.png", 100f);

	public static LoadableAsset<Sprite> DevilIcon { get; } =
		new LoadableResourceAsset("Pixeladditions.Resources.Roles.DevilIcon.png", 100f);

	public static LoadableAsset<Sprite> BionicBugIcon { get; } =
		new LoadableResourceAsset("Pixeladditions.Resources.Roles.BionicBugIcon.png", 100f);

	public static LoadableAsset<Sprite> SoulSnatcherIcon { get; } =
		new LoadableResourceAsset("Pixeladditions.Resources.Roles.SoulSnatcherIcon.png", 100f);

	public static LoadableAsset<Sprite> StakeHolderIcon { get; } =
		new LoadableResourceAsset("Pixeladditions.Resources.Roles.StakeHolderIcon.png", 100f);

	public static LoadableAsset<Sprite> DevilLeapButton { get; } =
		new LoadableResourceAsset("Pixeladditions.Resources.Buttons.Neutral.LeapButton.png", 100f);

	public static LoadableAsset<Sprite> DevilEnrageButton { get; } =
		new LoadableResourceAsset("Pixeladditions.Resources.Buttons.Neutral.EnrageButton.png", 100f);

	public static LoadableAsset<Sprite> DevilDevourButton { get; } =
		new LoadableResourceAsset("Pixeladditions.Resources.Buttons.Neutral.DevourButton.png", 100f);

	public static LoadableAsset<Sprite> StakeHoldertargetButton { get; } =
		new LoadableResourceAsset("Pixeladditions.Resources.Buttons.Neutral.StakeHolderTargetButton.png", 60f);

	public static LoadableAsset<Sprite> BionicBugTargetButton { get; } =
		new LoadableResourceAsset("Pixeladditions.Resources.Buttons.Impostor.BionicBugTargetButton.png", 100f);
	public static LoadableAsset<Sprite> BionicBugKillButton { get; } =
		new LoadableResourceAsset("Pixeladditions.Resources.Buttons.Impostor.BionicBugKillButton.png", 100f);

	public static LoadableAsset<Sprite> BionicBugtargetIcon { get; } =
		new LoadableResourceAsset("Pixeladditions.Resources.Buttons.Impostor.BionicBugtargetIcon.png", 60f);

	public static LoadableAsset<Sprite> SoulSnatcherTargetButton { get; } =
		new LoadableResourceAsset("Pixeladditions.Resources.Buttons.Impostor.SoulSnatcherTargetButton.png", 60f);

}
}
