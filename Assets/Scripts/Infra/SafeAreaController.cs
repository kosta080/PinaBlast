using UnityEngine;

[RequireComponent(typeof(Camera))]
public class SafeAreaController : MonoBehaviour
{
    [SerializeField] private Camera _camera;
    
    void Start()
    {
        AdjustCameraToSafeArea();
    }

    private void AdjustCameraToSafeArea()
    {
        Rect safeArea = Screen.safeArea;
        Rect newRect = new Rect
        {
            x = safeArea.xMin / Screen.width,
            y = safeArea.yMin / Screen.height,
            width = safeArea.width / Screen.width,
            height = safeArea.height / Screen.height
        };
        _camera.rect = newRect;
    }
}