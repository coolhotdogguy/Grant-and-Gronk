using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Build;
using UnityEngine;
using UnityEngine.UI;

public class Icons : MonoBehaviour
{
    [SerializeField] Image[] icons;
    [SerializeField] Image[] iconsBW;

    bool acivateIcon;
    bool deactivateIcon;
    int planetTypeToFadeIn;
    int planetTypeToFadeOut;

    void Update()
    {
        if (acivateIcon)
        {
            //iconsBW[planetTypeToFadeIn].color -= Color.HSVToRGB(0f, 0.2f, 0f) * Time.deltaTime;
            iconsBW[planetTypeToFadeIn].color -= new Color(0f, 0f, 0f, 0.2f) * Time.deltaTime;
            icons[planetTypeToFadeIn].color += new Color(1f, 1f, 1f, 1f) * Time.deltaTime;
            if (iconsBW[planetTypeToFadeIn].color.a <= 0 && icons[planetTypeToFadeIn].color.a >= 1)
            {
                acivateIcon = false;
            }
        }
        if (deactivateIcon)
        {
            //iconsBW[planetTypeToFadeOut].color += Color.HSVToRGB(0f, 0.2f, 0f) * Time.deltaTime;
            iconsBW[planetTypeToFadeOut].color += new Color(0f, 0f, 0f, 0.2f) * Time.deltaTime;
            icons[planetTypeToFadeOut].color -= new Color(1f, 1f, 1f, 1f) * Time.deltaTime;
            if (iconsBW[planetTypeToFadeOut].color.a >= 0.3921569)
            {
                deactivateIcon = false;
            }
        }
    }

    public void FadeInPlanetIcon(int planetType)
    {
        planetTypeToFadeIn = planetType;
        acivateIcon = true;
    }

    public void FadeOutPlanetIcon(int planetType)
    {
        planetTypeToFadeOut = planetType;
        deactivateIcon = true;
    }
}
