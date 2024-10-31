using System;
using System.Collections.Generic;
using Kor.Infra;
using UnityEngine;

namespace Kosta.Controls
{
    public class PlayerController
    {
        public Action<KeyCode> OnKeyPress;
        public Action<KeyCode> OnKeyRelease;
        
        private HashSet<KeyCode> _keysPressed = new();
        
        private EventManager _eventManager;
        private bool _isEnabled = true;
        
        public PlayerController()
        {
            _eventManager = ServiceLocator.Resolve<EventManager>();
            OnKeyPress += HandleKeyPress;
            OnKeyRelease += HandleKeyRelease;

            _eventManager.OnPinataExploded += DisablePlayerController;
            _eventManager.OnTimeIsUp += DisablePlayerController;
            _eventManager.OnRestartRound += EnablePlayerController;
        }

        private void EnablePlayerController()
        {
            _isEnabled = true;
        }

        private void DisablePlayerController()
        {
            _isEnabled = false;
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
            if (!_isEnabled) return false;
            return _keysPressed.Contains(key);
        }
        
    }
}