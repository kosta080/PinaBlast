using UnityEngine;

[RequireComponent(typeof(Camera))]
public class SafeAreaController : MonoBehaviour
{
    [SerializeField] private Camera _camera;
    public bool Adjust = true;
    
    void Start()
    {
        if (!Adjust) return;
        Rect newRect = GetSafeArea();
        _camera.rect = newRect;
    }

    public static Rect GetSafeArea()
    {
        Rect safeArea = Screen.safeArea;
        return new Rect
        {
            x = safeArea.xMin / Screen.width,
            y = safeArea.yMin / Screen.height,
            width = safeArea.width / Screen.width,
            height = safeArea.height / Screen.height
        };
        
    }
}