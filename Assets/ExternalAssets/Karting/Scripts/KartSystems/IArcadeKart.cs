using System;
using UnityEngine;

namespace KartGame.KartSystems
{
    public interface IArcadeKart
    {
        float AirPercent { get; }
        float GroundPercent { get; }
        InputData Input { get; }
        bool IsDrifting { get; }
        Rigidbody Rigidbody { get; }
        bool WantsToDrift { get; }

        float GetMaxSpeed();
        float LocalSpeed();
        void Reset();
        void SetCanMove(bool move);
    }
}