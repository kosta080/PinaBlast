using Kosta.Infra;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Kosta.Pinata
{
    public class PinataHealthView : MonoBehaviour
    {
        [SerializeField] private Image _healthImage;
        [SerializeField] private TMP_Text _healthSliderText;
        
        PinataHealth _pinataHealth;
        private void Start()
        {
            _pinataHealth = ServiceLocator.Resolve<PinataHealth>();
            _pinataHealth.HealthPoints.ValueChanged += OnHealthChanged;
        }

        private void OnHealthChanged(int newValue)
        {
            float healthPercent = newValue / (float)_pinataHealth.HealthMax;
            _healthImage.fillAmount = healthPercent;
            _healthSliderText.text = newValue.ToString();
        }

        
    }
}