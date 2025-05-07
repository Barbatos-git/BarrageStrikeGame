using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanvasController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        CanvasScaler scaler = GetComponent<CanvasScaler>();
        float aspectRatio = (float)Screen.width / Screen.height;
        scaler.matchWidthOrHeight = aspectRatio > 1 ? 0 : 1;
        Rect safeArea = Screen.safeArea;
        RectTransform rectTransform = GetComponent<RectTransform>();
        rectTransform.offsetMin = safeArea.position;
        rectTransform.offsetMax = safeArea.position + safeArea.size;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
