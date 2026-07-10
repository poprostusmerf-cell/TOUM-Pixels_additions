using MiraAPI.Utilities.Assets;
using UnityEngine;
using TownOfUs.Assets;

namespace Pixeladditions.assets
{


public static class Pixelassets
{
	public static LoadableAsset<Sprite> soulsnatch { get; } =
		new LoadableResourceAsset("Pixeladditions.Resources.soulsnatch.png", 100f);

}
}
