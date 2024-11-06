using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PanelDifficultyController : MonoBehaviour
{
    public RectTransform optionsPanelDifficulty;
    public Vector3 hiddenPosition = new Vector3(2000f, 0f, 0f);
    public Vector3 visiblePosition = Vector3.zero;
    public float smoothTime = 0.3f;

    private Vector3 velocity = Vector3.zero;
    public bool isOptionsVisible = false;
    void Start()
    {
        ResetPanel();
        isOptionsVisible = false;
        optionsPanelDifficulty.localPosition = hiddenPosition;
    }
    void OnEnable()
    {
        ResetPanel(); 
    }
    void Update()
    {
        Vector3 targetPosition;

        if (isOptionsVisible)
        {
            targetPosition = visiblePosition;
        }
        else
        {
            targetPosition = hiddenPosition;
        }
        optionsPanelDifficulty.localPosition = Vector3.SmoothDamp(optionsPanelDifficulty.localPosition, targetPosition, ref velocity, smoothTime);
    }
    public void ToggleOptionsDifficulty()
    {
        isOptionsVisible = !isOptionsVisible;
    }
    public void ResetPanel()
    {
        isOptionsVisible = false;
        optionsPanelDifficulty.localPosition = hiddenPosition;
    }
}