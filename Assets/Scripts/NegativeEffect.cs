using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.Random;
using static UnityEngine.Debug;
using UnityEngine.UIElements;

public enum NegativeEffectType
{
    Slowdown, Damage, Respawn
}

public class NegativeEffect : MonoBehaviour
{
    public NegativeEffectType _effectType;
    protected IUnit _unit;

    private void Awake()
    {
        GetEffect();
    }

    private void OnTriggerEnter(Collider other)
    {
        _unit = other.GetComponent<IUnit>();

        if (_unit == null)
            return;

        switch (_effectType)
        {
            case NegativeEffectType.Slowdown: _unit.SpeedChange(Range(-2, -5)); break;
            case NegativeEffectType.Damage: _unit.TakeDamage(Range(10, 50)); break;
            case NegativeEffectType.Respawn: _unit.Respawn(); break;
            default: break;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        _unit = other.GetComponent<IUnit>();

        if (_unit == null)
            return;

        Destroy(gameObject);

        Log("Получен отрицательный эффект " + GetEffectInfo());
    }

    private void GetEffect()
    {
        int effectTypeNumber = Range(0, 2);

        switch (effectTypeNumber)
        {
            case 0: _effectType = NegativeEffectType.Slowdown; break;
            case 1: _effectType = NegativeEffectType.Damage; break;
            case 2: _effectType = NegativeEffectType.Respawn; break;
            default: break;
        }
    }

    public (NegativeEffectType type, Vector3 pos) GetEffectInfo()
    {
        return (_effectType, transform.position);
    }
}
