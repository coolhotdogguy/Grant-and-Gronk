using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontDestoryOnLoad : MonoBehaviour
{
    private void Start()
    {
        DontDestroyOnLoad(this);
    }
}
