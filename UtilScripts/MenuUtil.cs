using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuUtil : MonoBehaviour
{
    [SerializeField] GameObject menuSlider;
    [SerializeField] GameObject clickAnywhereText;
    [SerializeField] AudioController backgroundMusic;
    public bool flag;

    private void Awake()
    {
        flag = true;
    }

    private void Update()
    {


        if (clickAnywhereText && clickAnywhereText.transform.localScale.y >= 0.8f && flag == true)
        {
            if (clickAnywhereText.transform.localScale.y <= 0.81f)
            {
                Debug.Log("here");
                flag = false;
            }
            clickAnywhereText.transform.localScale -= new Vector3(0.001f, 0.001f, 0.001f);
        }
        else if (clickAnywhereText && flag == false)
        {
            if (clickAnywhereText.transform.localScale.y >= 0.9f)
            {
                flag = true;
            }
            clickAnywhereText.transform.localScale += new Vector3(0.001f, 0.001f, 0.001f);
        }

    }
    public void SlideMenu()
    {
        menuSlider.GetComponent<Animation>().Play("ButtonSideSlide");
        clickAnywhereText.SetActive(false);
        backgroundMusic.Play();

    }

}