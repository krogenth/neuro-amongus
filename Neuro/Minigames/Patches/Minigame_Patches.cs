﻿using HarmonyLib;

namespace Neuro.Minigames.Patches;

[HarmonyPatch(typeof(Minigame), nameof(Minigame.Begin))]
public static class Minigame_Begin
{
    [HarmonyPostfix]
    public static void Postfix(Minigame __instance, PlayerTask task)
    {
        MinigameHandler.TryCompleteMinigame(__instance, task);
    }
}

[HarmonyPatch(typeof(Minigame), nameof(Minigame.CoStartClose))]
public static class Minigame_CoStartClose
{
    [HarmonyPostfix]
    public static void Postfix(Minigame __instance, float duration)
    {
        MinigameTimeHandler.Instance.AddMinigameCloseTimeDelay(__instance, duration);
    }
}