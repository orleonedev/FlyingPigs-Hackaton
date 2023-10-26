using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BarBehaviour : MonoBehaviour
{
    public Slider slider;
    public Color low;
    public Color high;
    public ImageShow imageShow;
    public ImageShow failure;
    public int level = 1;

    private float count = 0.1f;
    private int rising = 1;

    private bool shouldMove = true;
    private bool lastResult = false;

    void Start() {
        imageShow = GetComponentInChildren<ImageShow>();
    }

    public void ResetForNextLevel() {
        count = 0.1f;
        SetValue(count, 100);
        shouldMove = true;
        imageShow.SwitchShow(lastResult);
    }
    
    private void Update()
    {
        SetValue(count, 100);
        if (count >= 100 || count <= 0) {
            rising = -rising;
        }

        if (shouldMove) {
            count += 0.1f * rising * level / 2;
        }
    }

    public void SetValue(float value, float maxValue) {
        slider.value = value;
        slider.maxValue = maxValue;
        slider.fillRect.GetComponentInChildren<Image>().color = Color.Lerp(low, high, value/maxValue);
    }

    public void OnPress() {
        if (shouldMove) {
            shouldMove = false;
            if (count > 85) {
                lastResult = true;
                imageShow.SwitchShow(lastResult);
                level ++;
            }
            else {
                lastResult = false;
                imageShow.SwitchShow(lastResult);
                level = 1;
            }
            Invoke("ResetForNextLevel", 3);
        }
    }
}
