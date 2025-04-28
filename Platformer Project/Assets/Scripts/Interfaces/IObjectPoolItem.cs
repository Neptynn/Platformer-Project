using Platformer.ObjectPoolSystem;
using UnityEngine;
using System;

namespace Platformer.Interfaces
{
    public interface IObjectPoolItem
    {
        void SetObjectPool<T>(ObjectPool pool, T comp) where T : Component;

        void Release();
    }
}