using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IconManager : MonoBehaviour
{
    public Sprite iconON;
    public Sprite iconOFF;

    private Image _iconImage;

    public bool currentIconStatus = true;

    private void Start()
    {
        _iconImage = GetComponent<Image>();
        _iconImage.sprite = (currentIconStatus) ? iconON : iconOFF;
    }

    public void ToggleIcon(bool iconStatus)
    {
        if (!_iconImage || !iconON || !iconOFF) return;

        _iconImage.sprite = (iconStatus) ? iconON : iconOFF;
    }
}