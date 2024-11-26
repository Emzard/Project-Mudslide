using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// script to handle the progress bar
public class LoadingProgressBar : MonoBehaviour
{
    private Scrollbar loadingbar;
    private void Awake()
    {
        loadingbar = GetComponent<Scrollbar>();
    }

    private void Update()
    {
        loadingbar.size = Loader.GetLoadingProgress();
    }
}
