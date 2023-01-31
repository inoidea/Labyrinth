using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.Random;
using static UnityEngine.Debug;

public enum NegativeEffectType
{
    Slowdown, Damage, Respawn, Mix
}

public class NegativeEffect : MonoBehaviour
{
    public NegativeEffectType _effectType;

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
                case NegativeEffectType.Slowdown: player.SpeedChange(Range(-2, -5)); break;
                case NegativeEffectType.Damage: player.TakeDamage(Range(10, 50)); break;
                case NegativeEffectType.Respawn: player.Respawn(); break;
                case NegativeEffectType.Mix: Log("Будет перемешивание эффектов"); break;
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

    private void GetEffect()
    {
        int effectTypeNumber = Range(0, 3);

        switch (effectTypeNumber)
        {
            case 0: _effectType = NegativeEffectType.Slowdown; break;
            case 1: _effectType = NegativeEffectType.Damage; break;
            case 2: _effectType = NegativeEffectType.Respawn; break;
            case 3: _effectType = NegativeEffectType.Mix; break;
            default: break;
        }
    }
}
