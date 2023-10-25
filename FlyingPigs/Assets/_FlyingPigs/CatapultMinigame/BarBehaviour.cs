using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BarBehaviour : MonoBehaviour
{
    public Slider slider;
    public Color low;
    public Color high;
    public int level = 1;

    private float count = 0.1f;
    private int rising = 1;

    private bool shouldMove = true;

    public void ResetForNextLevel() {
        count = 0.1f;
        SetValue(count, 100);
        shouldMove = true;
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
                Debug.Log("SUCCESS");
                level ++;
            }
            else {
                Debug.Log("FAILURE");
                level = 1;
            }
            Invoke("ResetForNextLevel", 3);
        }
    }
}
