using MiraAPI.Hud;
using MiraAPI.Utilities.Assets;
using MiraAPI.Utilities;
using UnityEngine;
using Pixeladditions.assets;
//using TownOfUs.Roles.Neutral;
using Pixeladditions.impostor.soulsnatcher;

namespace Pixeladditions.buttons.impostor;

public class soulsnatch : TownOfUsRoleButton<SoulSnatcher, DeadBody>
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


//    public override DeadBody? GetTarget()
//    {
//        return PlayerControl.LocalPlayer.GetNearestDeadBody(Distance);
//    }

    protected override void OnClick()
    {

        if (Target == null)
        {
            return;
        }

        var targetId = Target.ParentId;
        var targetPlayer = MiscUtils.PlayerById(targetId);

        if (targetPlayer == null)
        {
            return; // Someone may have left mid game or something and gc just vacuumed, but idk. better safe than sorry ig.
        }

        SoulSnatcher.soulsnatch(PlayerControl.LocalPlayer, targetPlayer);

    }

}