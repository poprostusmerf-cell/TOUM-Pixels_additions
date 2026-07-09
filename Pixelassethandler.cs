using MiraAPI.Utilities.Assets;
using UnityEngine;

namespace Pixelassets;

public static class Pixelassets
{
//public static LoadableResourceAsset soulsnatch { get; } = new("janekmaraka.Resources.soulsnatch.png");
public static LoadableAsset<Sprite> soulsnatch { get; } = new LoadableResourceAsset("Pixeladditions.Resources.soulsnatch.png", 100f);
}
