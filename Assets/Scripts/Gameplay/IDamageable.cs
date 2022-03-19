using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Gameplay
{
    public interface IDamageable
    {
        public void TakeDamage(float value);
    }
}