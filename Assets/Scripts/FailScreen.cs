using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FailScreen : MonoBehaviour
{
    #region Editor Fields

    [SerializeField]
    private float fadeTime = 0.7f;

    [SerializeField]
    private float fadeDelay;

    [SerializeField]
    private Color materialColor1;

    [SerializeField]
    private Color materialColor2;

    [SerializeField]
    private KeyCode fadeButton;

    Renderer render;
    bool isFading;

    #endregion

    void Start()
    {
        render = gameObject.GetComponent<Renderer>();

        render.material.color = materialColor1;
    }

    void Update()
    {
        if (Input.GetKeyDown(fadeButton))
        {
            BeginFade();
        }
    }

    public void BeginFade()
    {
        isFading = true;
    }

    void Fade()
    {
        if (isFading == true)
        {
            render.material.color = Color.Lerp(render.material.color, materialColor2, Time.fixedDeltaTime / fadeTime);
        }
    }

    void FixedUpdate()
    {
        Fade();
    }
}
