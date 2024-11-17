using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class CustomSlider : MonoBehaviour
{
    // Root visual element
    private VisualElement m_root;

    private VisualElement m_slider;

    private VisualElement m_dragger;

    private VisualElement m_bar;
    // Start is called before the first frame update
    void Start()
    {
        m_root = GetComponent<UIDocument>().rootVisualElement;
        m_slider = m_root.Q<Slider>("MySlider");
        m_dragger = m_root.Q<VisualElement>("unity-dragger");

        AddElements();
    }

    void AddElements()
    {

        Debug.Log("We com her");
        m_bar = new VisualElement();
        m_bar.name = "Bar";
        m_dragger.Add(m_bar);
        Debug.Log(m_dragger);
        m_bar.AddToClassList("bar");

        //m_bar.name = "Bar";
        IStyle style = m_bar.style;
        style.backgroundColor = Color.red;
        //style.width = 100f;
        //style.height = 100f;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
