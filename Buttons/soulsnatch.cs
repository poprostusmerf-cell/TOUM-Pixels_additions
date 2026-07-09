using MiraAPI.Hud;
using MiraAPI.Utilities.Assets;
using TownOfUs.Roles.Crewmate;
using UnityEngine;

namespace Pixelassets;

public class soulsnatch : CustomActionButton
{
    public override string Name => "Soul Snatch";
    public override float Cooldown => 25f;
    public override float EffectDuration => 0f; 
    public override int MaxUses => 1; // 0 means unlimited uses.
    public override LoadableAsset<Sprite> Sprite => Pixelassets.soulsnatch;
    public override ButtonUsesMode UsesMode => ButtonUsesMode.PerGame;

    public override bool Enabled(RoleBehaviour role)
    {
        return role is SoulSnatcher; // only show button when the player is soul snatcher.
    }

    protected override void OnClick()
    {
        
    }

}