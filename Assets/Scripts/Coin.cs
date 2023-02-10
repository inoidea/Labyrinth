using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Coin : MonoBehaviour
{
    protected IUnit _unit;

    public static Action<float> OnCoinChangeCurNum;

    private void OnTriggerEnter(Collider other)
    {
        _unit = other.GetComponent<IUnit>();

        if (_unit == null)
            return;

        OnCoinChangeCurNum?.Invoke(1f);
    }

    private void OnTriggerExit(Collider other)
    {
        _unit = other.GetComponent<IUnit>();

        if (_unit == null)
            return;

        Destroy(gameObject);
    }
}
