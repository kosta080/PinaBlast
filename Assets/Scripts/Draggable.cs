using UnityEngine;

namespace Kosta
{
    public class Draggable : MonoBehaviour
    {
        private Vector3 _startPosOffset;
        private Vector3 _mousePos;
        private bool _dragging;
        private float _distanceFromCamera = 10f;

        void Update()
        {
            if (!_dragging) return;

            transform.localPosition = GetMouseWorldPosition() - _startPosOffset;
        }

        private void OnMouseDown()
        {
            if (!Input.GetMouseButtonDown(0)) return;

            _startPosOffset = GetMouseWorldPosition() - transform.localPosition;
            _dragging = true;
        }

        private void OnMouseUp()
        {
            _dragging = false;
        }

        private Vector3 GetMouseWorldPosition()
        {
            _mousePos = Input.mousePosition;
            _mousePos.z = _distanceFromCamera;
            return Camera.main.ScreenToWorldPoint(_mousePos);
        }
    }
}