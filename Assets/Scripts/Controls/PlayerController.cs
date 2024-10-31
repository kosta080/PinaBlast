using System;
using System.Collections.Generic;
using Kosta.Infra;
using UnityEngine;

namespace Kosta.Controls
{
    public class PlayerController
    {
        public Action<KeyCode> OnKeyPress;
        public Action<KeyCode> OnKeyRelease;
        
        private HashSet<KeyCode> _keysPressed = new();
        
        public PlayerController()
        {
            OnKeyPress += HandleKeyPress;
            OnKeyRelease += HandleKeyRelease;
        }
        
        private void HandleKeyPress(KeyCode keyCode)
        {
            _keysPressed.Add(keyCode);
        }

        private void HandleKeyRelease(KeyCode keyCode)
        {
            _keysPressed.Remove(keyCode);
        }

        public bool IsKeyDown(KeyCode key)
        {
            return _keysPressed.Contains(key);
        }
        
    }
}