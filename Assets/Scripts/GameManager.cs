using JetBrains.Annotations;
using System;
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

    public List<GameObject> _effectList;

    private void Awake()
    {
        Subscribe();
    }

    private void Start()
    {
        for (int i = 0; i < _effectPointParent.childCount; i++)
        {
            _effectPoints.Add(_effectPointParent.GetChild(i));
        }

        ShowEffects();
    }

    private void Subscribe()
    {
        Player.DeleteAllEffects += DeleteAllEffects;
    }

    private void ShowEffects()
    {
        if (_effectPoints.Count > 0)
        {
            _effectPoints.ForEach(p => {
                bool posEffect = ((Range(1, 10) % 2) == 0);

                GameObject effect = Instantiate((posEffect) ? _positiveEffect : _negativeEffect, p.transform.position, Quaternion.identity);
                _effectList.Add(effect);
            });
        }
    }

    public void DeleteAllEffects()
    {
        if (_effectList.Count > 0)
        {
            _effectList.ForEach(p => {
                try
                {
                    Destroy(p);
                }
                catch {
                    throw new Exception("Объект удален ранее.");
                }
            });
        }
    }
}
