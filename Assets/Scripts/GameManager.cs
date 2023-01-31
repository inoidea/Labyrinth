using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.Debug;
using static UnityEngine.Random;


public class GameManager : MonoBehaviour
{
    [Header("Effects")]
    [SerializeField] private Transform _effectPointParent;
    [SerializeField] private List<Transform> _effectPoints;

    [SerializeField] private GameObject _positiveEffect;
    [SerializeField] private GameObject _negativeEffect;

    private void Start()
    {
        for (int i = 0; i < _effectPointParent.childCount; i++)
        {
            _effectPoints.Add(_effectPointParent.GetChild(i));
        }

        ShowEffects();
    }

    private void ShowEffects()
    {
        if (_effectPoints.Count > 0)
        {
            _effectPoints.ForEach(p => {
                bool posEffect = ((Range(1, 10) % 2) == 0);

                Instantiate((posEffect) ? _positiveEffect : _negativeEffect, p.transform.position, Quaternion.identity);
            });
        }
    }
}
