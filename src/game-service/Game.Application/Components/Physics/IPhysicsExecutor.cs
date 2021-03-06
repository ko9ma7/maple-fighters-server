﻿using System;
using Coroutines;

namespace Game.Application.Components
{
    public interface IPhysicsExecutor : IDisposable
    {
        ICoroutineRunner GetCoroutineRunner();
    }
}