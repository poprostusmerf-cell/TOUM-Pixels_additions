using MiraAPI.Modifiers;
using MiraAPI.Modifiers.Types;
using Pixeladditions.Patches;
using UnityEngine;
using TownOfUs.Modules;
using Pixeladditions.Roles;
using static Pixeladditions.networking.RpcBroadcastModules;

namespace Pixeladditions.Modifiers;

public sealed class StakeHoldertargetmodifier(PlayerControl StakeHolder) : BaseModifier
{

    public override string ModifierName => "Stakeholdermodifier";
    public override bool Unique => true;
    public override bool HideOnUi => true;

   // public PlayerControl Owner { get; set; } = owner;


    public override void OnActivate()
    {
    
    }

    public override void OnDeath(DeathReason reason)
    {
  if (StakeHolder.Data.Role is StakeHolder stakeHolder){
        stakeHolder.bets_won++;
        PlayerControl.LocalPlayer.RpcRemoveModifier<StakeHoldertargetmodifier>();
        RpcPrivateNotification(StakeHolder, "The person you targetted has died");
        if (stakeHolder.bets_won >= 3)
        {
            stakeHolder.MetGoal= true;
        }}
    }

    public override void OnMeetingStart()
    {
        PlayerControl.LocalPlayer.RpcRemoveModifier<StakeHoldertargetmodifier>();
    }
}