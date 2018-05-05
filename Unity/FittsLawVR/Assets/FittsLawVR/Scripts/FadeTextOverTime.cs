using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class FadeTextOverTime : MonoBehaviour {

    public void FadeText(float duration)
    {
        Text text = gameObject.GetComponent<Text>();
        text.color = new Color(text.color.r, text.color.g, text.color.b, 1);
        StartCoroutine(FadeTextToZeroAlpha(duration, text));
    }

    public IEnumerator FadeTextToZeroAlpha(float time, Text text)
    {
        while (text.color.a > 0.0f)
        {
            text.color = new Color(text.color.r, text.color.g, text.color.b, text.color.a - (Time.deltaTime / time));
            yield return null;
        }

        gameObject.SetActive(false);
    }
}
