using UnityEngine;

public class FadeOut : MonoBehaviour
{
    private CanvasGroup canvasGroup;
    public float speed = 1f;
    public float delay = 0.5f;

    void Start()
    {
        canvasGroup = GetComponent<CanvasGroup>();
    }


    void Update()
    {
        delay = delay - Time.deltaTime * speed;
        if (delay <= 0)
        {
            if (canvasGroup.alpha >= 0)
            {
                canvasGroup.alpha -= Time.deltaTime * speed;
            }
            else
            {
                canvasGroup.blocksRaycasts = false;
                canvasGroup.interactable = false;
            }
        }
    }
}