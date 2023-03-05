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
            case PositiveEffectType.Boost: _unit.SpeedChange(Range(2, 10)); break;
            case PositiveEffectType.Medicine: _unit.AddHP(Range(10, 70)); break;
            case PositiveEffectType.Shield: _unit.ShieldExists = true; break;
            default: break;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        _unit = other.GetComponent<IUnit>();

        if (_unit == null)
            return;

        Destroy(gameObject);

        Log("Получен положительный эффект " + GetEffectInfo());
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

    public (PositiveEffectType type, Vector3 pos) GetEffectInfo()
    {
        return (_effectType, transform.position);
    }
}
