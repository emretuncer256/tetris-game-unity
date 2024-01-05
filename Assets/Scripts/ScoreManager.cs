using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    private int _score = 0;
    private int _rows;
    private int _level = 1;

    public int rowCountInLevel = 5;

    private int _minRow = 1;
    private int _maxRow = 4;

    public TextMeshProUGUI rowTxt;
    public TextMeshProUGUI levelTxt;
    public TextMeshProUGUI scoreTxt;

    private void Start()
    {
        ResetValues();
    }

    public void ResetValues()
    {
        _level = 1;
        _rows = rowCountInLevel * _level;
    }

    public void RowScore(int n)
    {
        n = Mathf.Clamp(n, _minRow, _maxRow);

        switch (n)
        {
            case 1:
                _score += 30 * _level;
                break;
            case 2:
                _score += 50 * _level;
                break;
            case 3:
                _score += 150 * _level;
                break;
            case 4:
                _score += 500 * _level;
                break;
        }

        UpdateText();
    }

    void UpdateText()
    {
        if (scoreTxt) scoreTxt.text = ZeroPrefix(_score, 5);
        if (levelTxt) levelTxt.text = _level.ToString();
        if (rowTxt) rowTxt.text = _rows.ToString();
    }

    string ZeroPrefix(int score, int figureCount)
    {
        string scoreStr = score.ToString();
        while (scoreStr.Length < figureCount)
        {
            scoreStr = "0" + scoreStr;
        }

        return scoreStr;
    }
}