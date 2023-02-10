using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class CoinUI : MonoBehaviour
{
    [SerializeField] private Text _coinCurNumText;
    [SerializeField] private Text _coinMaxNumText;
    [SerializeField] private Transform _winPanel;

    private float currentNum;
    private float maxNum;

    private void Start()
    {
        Subscribe();
        _winPanel.gameObject.SetActive(false);
    }

    private void Subscribe()
    {
        Coin.OnCoinChangeCurNum += OnCoinChangeCurNum;
        GameManager.OnCoinChangeMaxNum += OnCoinChangeMaxNum;
    }

    private void OnCoinChangeCurNum(float coin)
    {
        currentNum += coin;
        _coinCurNumText.text = currentNum.ToString();

        if (currentNum == maxNum)
            Win();
    }

    private void OnCoinChangeMaxNum(float coin)
    {
        maxNum += coin;
        _coinMaxNumText.text = coin.ToString();
    }

    private void Win()
    {
        _winPanel.gameObject.SetActive(true);
    }

    private void OnDestroy()
    {
        Coin.OnCoinChangeCurNum -= OnCoinChangeCurNum;
        GameManager.OnCoinChangeMaxNum -= OnCoinChangeMaxNum;
    }
}
