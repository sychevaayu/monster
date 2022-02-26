using System;
using System.Collections;
using Assets.Scripts.Abstractions.HealthBar;
using Unity.Collections;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Experimental.Rendering;
using static TMPro.SpriteAssetUtilities.TexturePacker_JsonArray;

namespace Assets.Scripts.HealthBarGui
{
    public class HealthBarGui : MonoBehaviour
    {
        [SerializeField]
        private float currentHealth;

        [SerializeField]
        private float healthPercentage;

        [SerializeField]
        private GUISkin texture;

        private HealthBar healthBar;

        private Rect barRect;

        private void Start() {
            SetDefaultHealthBar();
            SetHealthBarBox();
        }

        private void UpdateBar() {
            currentHealth = healthBar.HealthAmount;
            healthPercentage = healthBar.HealthPercentage;
        }

        private void OnGUI() {
            SetHealthBarBox();
        }

        private void SetDefaultHealthBar() {
            int barYPosition = Screen.height - 55;
            const int barXPosition = 30;
            const int barWidth = 200;
            const int barHeight = 30;
            barRect = new Rect(barXPosition, barYPosition, barWidth, barHeight);
            healthBar = new HealthBar(100);
            healthBar.OnHeal.AddListener(UpdateBar);
            healthBar.OnTakeDamage.AddListener(UpdateBar);
            healthBar.OnDead.AddListener(UpdateBar);
            UpdateBar();
        }

        private void SetHealthBarBox() {
            const float frameWeight = 4;
            float healthWidth = (barRect.width - frameWeight * 2) * healthBar.HealthPercentage / 100;
            float healthHeight = barRect.height - frameWeight * 2;
            float healthX = barRect.x + frameWeight;
            float healthY = barRect.y + frameWeight;
            var healthView = new Rect(healthX, healthY, healthWidth, healthHeight);
            var content = new GUIContent($"\t{healthBar.HealthAmount}/{healthBar.MaxHealthAmount} ({healthBar.HealthPercentage}%)");
            GUI.Box(barRect, string.Empty, texture.GetStyle("Frame"));
            GUI.Box(healthView, content, texture.GetStyle("Bar"));
        }
    }
}