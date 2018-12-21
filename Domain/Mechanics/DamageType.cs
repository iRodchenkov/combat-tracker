﻿using System;

namespace Domain.Mechanics
{
    [Flags]
    public enum DamageType
    {
        Untyped = 0,
        Weapon = 1,
        Fire = 1 << 2,
        Ice = 1 << 3
    }
}