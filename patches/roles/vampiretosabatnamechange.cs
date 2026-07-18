using HarmonyLib;
using TownOfUs.Modules.Localization;
using TownOfUs.Roles.Neutral;


namespace Pixeladditions.patches.vampiretosabat;

/// <summary>
/// Patches to rename the existing Vampire role (ground traps) to Sabat.
/// </summary>
[HarmonyPatch]
public static class Renamevampiretosabatpatch
{
    [HarmonyPatch(typeof(TownOfUs.Roles.Neutral.VampireRole), "get_LocaleKey")]
    [HarmonyPostfix]
    public static void VampireRoleLocaleKeyPostfix(ref string __result)
    {
        __result = "Sabat";
    }

    [HarmonyPatch(typeof(TownOfUs.Roles.Neutral.VampireRole), "get_RoleName")]
    [HarmonyPostfix]
    public static void VampireRoleRoleNamePostfix(ref string __result)
    {
        __result ="Sabat";
    }

    [HarmonyPatch(typeof(TownOfUs.Roles.Neutral.VampireRole), "get_RoleDescription")]
    [HarmonyPostfix]
    public static void VampireRoleRoleDescriptionPostfix(ref string __result)
    {
        __result = "badz czlonkiem sabatu";
    }

    [HarmonyPatch(typeof(TownOfUs.Roles.Neutral.VampireRole), "get_RoleLongDescription")]
    [HarmonyPostfix]
    public static void VampireRoleRoleLongDescriptionPostfix(ref string __result)
    {
        __result = "czlonek sabatu";
    }

}