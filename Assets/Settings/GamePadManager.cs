using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GamePadManager : MonoBehaviour
{
    
    private Gamepad[] _gamepads;
    
    private static GamePadManager _instance;

    public static GamePadManager Instance
    {
        get => _instance;
        set => _instance = value;
    }

    private void Start()
    {
        _gamepads = Gamepad.all.ToArray();
        foreach (var gamepad in _gamepads)
        {
            //Debug.Log(gamepad);
        }
    }
}
