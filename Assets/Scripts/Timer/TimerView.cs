using System;
using Kosta.Infra;
using TMPro;
using UnityEngine;

namespace Kosta.Timer
{
    public class TimerView : MonoBehaviour
    {
        [SerializeField] private TMP_Text _timerText;
        private TimerController _timerController;
        
        private void Start()
        {
            _timerController = ServiceLocator.Resolve<TimerController>();
        }

        private void Update()
        {
            _timerText.text = _timerController.WholeSecondsLeft.ToString();
        }
    }
}