using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Infra
{
    public class CommonButton : Button
    {
        private Vector3 _originalScale;
        private Color _originalColor;

        private void Awake()
        {
            base.Awake();
            _originalScale = targetGraphic.transform.localScale;
            _originalColor = targetGraphic.color;
        }
        public override void OnPointerDown(PointerEventData eventData)
        {
            base.OnPointerDown(eventData);
            targetGraphic.transform.localScale = _originalScale * 0.95f;
            targetGraphic.color = Color.white;
        }

        public override void OnPointerUp(PointerEventData eventData)
        {
            base.OnPointerUp(eventData);
            targetGraphic.transform.localScale = _originalScale;
            targetGraphic.color = _originalColor;
        }
    }
}