using Comfort.Common;
using HarmonyLib;
using EFT;
using QuickThrow.Utils;
using UnityEngine;

namespace QuickThrow.Patch
{
    [HarmonyPatch(typeof(Player))]
    internal class QuickThrowGrenade
    {
        [HarmonyPrefix]
        [HarmonyPatch("SetInHands", typeof(ThrowWeapItemClass), typeof(Callback<IHandsThrowController>))]
        public static bool Prefix(Player __instance, ThrowWeapItemClass throwWeap, Callback<IHandsThrowController> callback)
        {
            if (Plugin.DisableFastGrenade.Value)
                return true;
            if (!__instance.IsYourPlayer)
                return true;
            if (Input.GetKey(Plugin.KeyboardBindingOrigine.Value.MainKey))
                return true;
            
            __instance.SetInHandsForQuickUse(throwWeap,
                new Callback<GInterface179>(result =>
                {
                    QuickThrowLogger.Log($"[QuickThrowGrenade] SetInHandsForQuickUse result = {result}");
                }));
            return false;
        }
        
        [HarmonyPatch(typeof(Player.BaseGrenadeHandsController), "vmethod_1")]
        public class ForceLowThrow
        {
            [HarmonyPrefix]
            public static void Prefix(
                ref float timeSinceSafetyLevelRemoved,
                ref bool low,
                Player.BaseGrenadeHandsController __instance)
            {
                if (Plugin.DisableFastGrenade.Value)
                    return;
                    
                var playerField = AccessTools.Field(typeof(Player.BaseGrenadeHandsController), "_player");
                if (playerField?.GetValue(__instance) is not Player player)
                    return;

                if (__instance is Player.QuickGrenadeThrowHandsController && player.IsYourPlayer)
                {
                    if (!Input.GetKey(Plugin.KeyboardBindingShort.Value.MainKey))
                        return;

                    low = true;
                    QuickThrowLogger.Log($"[QuickThrowGrenade] Forced low throw (QuickThrow) | timeSinceSafetyLevelRemoved={timeSinceSafetyLevelRemoved}, low={low}");
                }
                else
                {
                    QuickThrowLogger.Log($"[QuickThrowGrenade] Normal throw | timeSinceSafetyLevelRemoved={timeSinceSafetyLevelRemoved}, low={low}");
                }
            }
        }
    }
}