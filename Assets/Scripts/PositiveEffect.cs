using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using static UnityEngine.Random;
using static UnityEngine.Debug;
using static UnityEditor.Experimental.GraphView.GraphView;

public enum PositiveEffectType
{
    Boost, Medicine, Shield
}

public class PositiveEffect : MonoBehaviour
{
    public PositiveEffectType _effectType;

    private void Awake()
    {
        GetEffect();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Player player = other.GetComponent<Player>();

            switch (_effectType)
            {
                case PositiveEffectType.Boost: player.SpeedChange(Range(2, 10)); break;
                case PositiveEffectType.Medicine: player.AddHP(Range(10, 70)); break;
                case PositiveEffectType.Shield: player.ShieldExists = true; break;
                default: break;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Destroy(gameObject);
            Log("Удален эффект " + _effectType);
        }
    }

    private void GetEffect() {
        int effectTypeNumber = Range(0, 2);

        switch (effectTypeNumber)
        {
            case 0: _effectType = PositiveEffectType.Boost; break;
            case 1: _effectType = PositiveEffectType.Medicine; break;
            case 2: _effectType = PositiveEffectType.Shield; break;
            default: break;
        }
    }
}
