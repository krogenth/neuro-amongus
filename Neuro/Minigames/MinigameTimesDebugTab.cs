﻿using Neuro.Debugging;
using Neuro.Utilities;
using Steamworks;
using System.Linq;
using UnityEngine;

namespace Neuro.Minigames;

[DebugTab]
public sealed class MinigameTimesDebugTab : DebugTab
{
    public override string Name => "Minigame Times";

    public override bool IsEnabled => MinigameTimeHandler.Instance;

    public override void BuildUI()
    {
        if (GUILayout.Button("Clear")) MinigameTimeHandler.Instance.Clear();
        foreach (var minigameTimeKeyValuePair in MinigameTimeHandler.Instance.MinigameTimes)
        {
            var minTime = minigameTimeKeyValuePair.Value.OrderBy(t => t.Total).First();
            var maxTime = minigameTimeKeyValuePair.Value.OrderByDescending(t => t.Total).First();
            using (new HorizontalScope())
            {
                GUILayout.Label($"{minigameTimeKeyValuePair.Key}");
                GUILayout.FlexibleSpace();
                GUILayout.Label($"range: {minTime.Total:0.00}s - {maxTime.Total:0.00}s");
            }
        }
    }
}
