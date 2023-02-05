using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthUI : MonoBehaviour
{
    [SerializeField] private Image _hpBar;
    [SerializeField] private Text _hpText;

    private void Awake()
    {
        Subscribe();
    }

    private void Subscribe() { 
        Player.OnHealthChanged += OnHealthChanged;
    }

    private void OnHealthChanged(float hp, float maxHp)
    {
        var healthPerc = Math.Clamp((hp * 100 / maxHp), 0, 100);
        var anchorMax = _hpBar.rectTransform.anchorMax;
        anchorMax.x = Math.Clamp((healthPerc / 100), 0, 1);
        _hpBar.rectTransform.anchorMax = anchorMax;

        _hpText.text = healthPerc.ToString() + "%";
    }
}
