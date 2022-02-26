using System;
using UnityEngine.Events;

namespace Assets.Scripts.Abstractions.HealthBar
{
    internal class HealthBar
    {
        private float maxHealthAmount;

        private float currentHealthAmount;

        private float healthPercentage;

        internal HealthBar(float maxHealth) {
            maxHealthAmount = maxHealth;
            currentHealthAmount = maxHealth;
            healthPercentage = 100;
            OnHeal = new UnityEvent();
            OnTakeDamage = new UnityEvent();
            OnDead = new UnityEvent();
        }

        internal HealthBar(float maxHealth, float currentHealth) {
            maxHealthAmount = maxHealth;
            currentHealthAmount = currentHealth;
            healthPercentage = 100;
            OnDead = new UnityEvent();
        }

        internal float HealthPercentage => healthPercentage;

        internal float HealthAmount => currentHealthAmount;

        public float MaxHealthAmount => maxHealthAmount;

        public UnityEvent OnHeal { get; set; }

        public UnityEvent OnTakeDamage { get; set; }

        public UnityEvent OnDead { get; set; }

        internal void IncreaseMaxHealthAmount(float addedQuantity) {
            currentHealthAmount += addedQuantity;
            maxHealthAmount = MaxHealthAmount + addedQuantity;
        }

        internal void Heal(float addedQuantity) {
            currentHealthAmount += addedQuantity;
            ChangeHealthPercentage();
            OnHeal.Invoke();
        }

        internal void TakeDamage(float damageAmount) {
            currentHealthAmount -= damageAmount;
            if (currentHealthAmount <= 0) {
                currentHealthAmount = 0;
            }
            ChangeHealthPercentage();
            OnTakeDamage.Invoke();
        }

        private void ChangeHealthPercentage() {
            if (Math.Abs(currentHealthAmount) > 0) {
                healthPercentage = 100 * currentHealthAmount / maxHealthAmount;
            } else {
                healthPercentage = 0;
                OnDead.Invoke();
            }
        }
    }
}
