﻿using RubyAdventure;
using UnityEngine;

namespace RubyAdventure
{
    public class HealthCollectible : MonoBehaviour
    {
        void OnTriggerEnter2D(Collider2D other)
        {
            RubyController controller = other.GetComponent<RubyController>();

            if (controller != null)
            {
                if (controller.CurrentHealth < controller.MaxHealth)
                {
                    controller.ChangeHealth(1);
                    Destroy(gameObject);
                }
            }
        }
    }
}