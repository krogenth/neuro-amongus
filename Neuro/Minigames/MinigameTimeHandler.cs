﻿using Il2CppInterop.Runtime.Attributes;
using System;
using Neuro.Events;
using Neuro.Utilities;
using Reactor.Utilities.Attributes;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Neuro.Minigames;

[RegisterInIl2Cpp, ShipStatusComponent]
public sealed class MinigameTimeHandler : MonoBehaviour
{
    public static MinigameTimeHandler Instance { get; private set; }

    [HideFromIl2Cpp]
    public Dictionary<string, List<float>> MinigameTimes { get; } = new Dictionary<string, List<float>>();

    private string _minigameName;
    private float _startTime;
    private Func<bool> _stopTimeCondition;

    public MinigameTimeHandler(IntPtr ptr) : base(ptr)
    {
    }

    private void Awake()
    {
        if (Instance)
        {
            NeuroUtilities.WarnDoubleSingletonInstance();
            Destroy(this);
            return;
        }

        Instance = this;
    }

    private void Update()
    {
        if (_stopTimeCondition != null && _stopTimeCondition())
        {
            Stop();
        }
    }

    private void AddMinigameTime(float time)
    {
        if (MinigameTimes.TryGetValue(_minigameName, out List<float> list))
        {
            if (list.Last() > 0.0f)
            {
                MinigameTimes[_minigameName].Add(time);
            }
        }
        else
        {
            MinigameTimes[_minigameName] = new List<float>() { time };
        }
    }

    public void Clear()
    {
        MinigameTimes.Clear();
    }

    public void StartTime(Minigame minigame)
    {
        _minigameName = minigame.GetIl2CppType().Name;
        _startTime = Time.time;
    }

    public void StopWhen(Func<bool> condition)
    {
        _stopTimeCondition = condition;
    }

    private void Stop()
    {
        _stopTimeCondition = null;
        AddMinigameTime(Time.time - _startTime);
    }
}