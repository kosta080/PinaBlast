﻿using System;

namespace Kor.Infra
{
    public class EventManager : IEventManager
    {
        public event Action<string> OnCollisionWithObject;
        
        public Action OnTimeIsUp;
        public Action OnPinataExploded;
        public Action OnRestartRound;

        /*
        public void TriggerCollisionWithObject(string message)
        {
            OnCollisionWithObject?.Invoke(message);
        }*/
    }

    public interface IEventManager
    {
        //public event Action<string> OnCollisionWithObject;
        //public void TriggerCollisionWithObject(string message);
    }
}