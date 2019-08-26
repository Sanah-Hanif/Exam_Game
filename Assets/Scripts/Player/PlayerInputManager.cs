using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputManager : MonoBehaviour
{
    [SerializeField] private PlayerInput input;

    public InputActionMap Ability { get; private set; }
    public InputActionMap Player { get; private set; }

    private void Awake()
    {
        Ability = input.actions.actionMaps[input.actions.actionMaps.Count - 1];
        Player = input.actions.actionMaps[0];
    }
}
