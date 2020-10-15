using UnityEngine;

namespace RubyAdventure
{
    public interface IPoolable
    { 
        void OnSpawn();
        void OnDespawn();

    }
}
