using UnityEngine;

public class ScaleObjectToScreen : MonoBehaviour
{
    private float referenceWidth = 1080f;
    private float referenceHeight = 1920f;
    
    void Start()
    {
        float screenWidth = Screen.width;
        float screenHeight = Screen.height;
        
        float widthRatio = screenWidth / referenceWidth;
        float heightRatio = screenHeight / referenceHeight;
        
        float scaleFactor = 0.80f + (Mathf.Min(widthRatio, heightRatio) - 1) * 0.15f;
        
        transform.localScale = new Vector3(scaleFactor, scaleFactor, scaleFactor);
    }
}