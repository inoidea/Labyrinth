using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using static UnityEngine.Debug;
using static UnityEngine.Random;


public class GameManager : MonoBehaviour
{
    [Header("Coins")]
    [SerializeField] private Transform _coinPointParent;
    [SerializeField] private List<Transform> _coinPoints;

    [SerializeField] private GameObject _coinPref;

    [Header("Effects")]
    [SerializeField] private Transform _effectPointParent;
    [SerializeField] private List<Transform> _effectPoints;

    [SerializeField] private GameObject _positiveEffect;
    [SerializeField] private GameObject _negativeEffect;

    public List<GameObject> _activeElementList;

    public static Action<float> OnCoinChangeMaxNum;

    private JsonData<SaveData> _jsonData = new JsonData<SaveData>();
    private SaveData _saveData = new SaveData();

    private string path = Path.Combine(Application.streamingAssetsPath, "JsonData.xml");

    private void Start()
    {
        // Сформировать список точек появления эффектов.
        for (int i = 0; i < _effectPointParent.childCount; i++)
        {
            _effectPoints.Add(_effectPointParent.GetChild(i));
        }

        // Сформировать список точек появления монет.
        for (int i = 0; i < _coinPointParent.childCount; i++)
        {
            _coinPoints.Add(_coinPointParent.GetChild(i));
        }

        OnCoinChangeMaxNum?.Invoke(_coinPoints.Count);

        Subscribe();
        ShowEffects();
        ShowCoins();
    }

    private void Subscribe()
    {
        Player.DeleteAllActiveElements += DeleteAllActiveElements;
    }

    private void ShowEffects()
    {
        if (_effectPoints.Count > 0)
        {
            List<SaveData> jsonDataList = new List<SaveData>();

            _effectPoints.ForEach(p => {
                bool posEffect = ((Range(1, 10) % 2) == 0);

                GameObject effect = Instantiate((posEffect) ? _positiveEffect : _negativeEffect, p.transform.position, Quaternion.identity);
                _activeElementList.Add(effect);

                jsonDataList.Add(new SaveData() { Name = effect.name, Position = effect.transform.position });
            });

            // Сохранить данные эффектов.
            _jsonData.SaveFromList(jsonDataList, path);
        }
    }

    private void ShowCoins()
    {
        if (_coinPoints.Count > 0)
        {
            _coinPoints.ForEach(p => {
                GameObject coin = Instantiate(_coinPref, p.transform.position, Quaternion.identity);
                coin.transform.rotation = Quaternion.Euler(90, 0, 0);
                _activeElementList.Add(coin);
            });
        }
    }

    private void DeleteAllActiveElements()
    {
        if (_activeElementList.Count > 0)
        {
            _activeElementList.ForEach(p => { Destroy(p); });
        }
    }

    private void OnDestroy()
    {
        Player.DeleteAllActiveElements -= DeleteAllActiveElements;
    }
}
