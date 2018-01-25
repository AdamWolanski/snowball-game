using System.Collections;
using UnityEngine;
using System;
using UnityEngine.UI;

public class UIController : MonoBehaviour {

    public delegate void FadeOutAction();
    public static event FadeOutAction OnFadeOut;

    public delegate void FadeInAction();
    public static event FadeInAction OnFadeIn;

    public static UIController instance;
    public Text MainText;
    public float FadeTimer;

    private void Initialize()
    {
        //if cam on -> disable
    }

    private void Start()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(this.gameObject);
	}
	
	private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))//Debug fade in
        {
            FadeIn();
        }
        if (Input.GetKeyDown(KeyCode.E))//Debug fade out
        {
            FadeOut();
        }
    }

    public void FadeOut()
    {
        StartCoroutine(FadeOut(FadeTimer, () => {
            if (OnFadeOut != null)
                OnFadeOut();
            Debug.Log("Faded Out!");
        }));
    }

    public void FadeIn()
    {
        StartCoroutine(FadeIn(FadeTimer, () => {
            if (OnFadeIn != null)
                OnFadeIn();
            Debug.Log("Faded In!");
        }));
    }

    public void SetText(string s)
    {
        MainText.text = s;
    }

    private IEnumerator FadeOut(float t, Action a)
    {
        MainText.color = new Color(MainText.color.r, MainText.color.g, MainText.color.b, 1);
        while (MainText.color.a > 0.0f)
        {
            MainText.color = new Color(MainText.color.r, MainText.color.g, MainText.color.b, MainText.color.a - (Time.deltaTime/t));
            yield return null;
        }
        a();
    }

    private IEnumerator FadeIn(float t, Action a)
    {
        MainText.color = new Color(MainText.color.r, MainText.color.g, MainText.color.b, 0);
        while (MainText.color.a < 1.0f)
        {
            MainText.color = new Color(MainText.color.r, MainText.color.g, MainText.color.b, MainText.color.a + (Time.deltaTime / t));
            yield return null;
        }
        a();
    }
}
