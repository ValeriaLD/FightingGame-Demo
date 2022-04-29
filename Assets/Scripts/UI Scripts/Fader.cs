using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Fader : MonoBehaviour
{
    public static Fader instance;
    [SerializeField]
    private Text diedText;

    private bool isFaded = false;
    public float duration = 0.4f;


    private void Awake()
    {
        instance = this;
    }
    public void Fade()
    {
        
        var canvasGr = GetComponent<CanvasGroup>();

        canvasGr.alpha = 0f;

        StartCoroutine(Fading(canvasGr, canvasGr.alpha, 1));

        isFaded = !isFaded;
    }

    public IEnumerator Fading(CanvasGroup canvasGrp, float startFade, float endFade)
    {
        float counter = 0;

        while(counter < duration)
        {
            counter += Time.deltaTime;
            canvasGrp.alpha = Mathf.Lerp(startFade, endFade, counter / duration);

            yield return null;
        }
    }
}
