﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NonPlayerCharacter : MonoBehaviour
{
    public float displayTime = 4;
    public GameObject dialogBox;
    
    private float _timerDisplay;

    private void Start()
    {
        dialogBox.SetActive(false);
        _timerDisplay = -1.0f;
    }
    
    public void DisplayDialog()
    {
        _timerDisplay = displayTime;
        dialogBox.SetActive(true);
    }
    
    private void Update()
    {
        if (_timerDisplay >= 0)
        {
            _timerDisplay -= Time.deltaTime;
            if (_timerDisplay < 0)
            {
                dialogBox.SetActive(false);
            }
        }
    }
    
}
