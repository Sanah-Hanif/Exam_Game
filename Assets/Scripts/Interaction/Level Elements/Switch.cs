﻿using System;
using System.Collections.Generic;
using Interactions;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Interaction.Level_Elements
{
    public class Switch : InteractionController
    {

        [SerializeField] private List<EnvironmentInteraction> dependancies;

        private InputActionMap map;
        
        public override void Interact()
        {
            if(dependancies == null)
                dependancies = new List<EnvironmentInteraction>();
            if(dependancies.Count == 0)
                return;
            foreach (var interaction in dependancies)
            {
                interaction.Interact();
            }
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.layer != LayerMask.NameToLayer("Player")) return;
            map = other.GetComponent<PlayerInputManager>().Player;
            map.GetAction("Ability").Disable();
            map.GetAction("Interact").performed += Performed;
            map.GetAction("Interact").Enable();
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            if (other.gameObject.layer != LayerMask.NameToLayer("Player")) return;
            map.GetAction("Ability").Enable();
            map.GetAction("Interact").performed -= Performed;
            map.GetAction("Interact").Disable();
            map = null;
        }

        private void Performed(InputAction.CallbackContext ctx)
        {
            Interact();
        }
    }
}
