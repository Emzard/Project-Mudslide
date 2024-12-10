using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class CustomSlider : MonoBehaviour
{
    // Root visual element
    private VisualElement m_root;

    private List<VisualElement> slider_list;
    // Start is called before the first frame update
    void Start()
    {
        m_root = GetComponent<UIDocument>().rootVisualElement.Q<VisualElement>("options-screen");

        // retrieve the sliders
        slider_list = m_root.Query("StoneSlider").ToList();

        slider_list.ForEach(elem => {
            AddElements(elem);
        });
    }

    void AddElements(VisualElement elem)
    {
        // get the draggers in the sliders
        VisualElement og_dragger = elem.Q<VisualElement>("unity-dragger");
        VisualElement m_bar = new VisualElement();

        // adding the progress bar
        m_bar.name = "Bar";
        m_bar.AddToClassList("bar-slider");
        og_dragger.Add(m_bar);

        VisualElement new_dragger = new VisualElement();
        elem.Add(new_dragger);
        new_dragger.name = "new-dragger";
        new_dragger.AddToClassList("new-dragger");

        // disable the picking mode to pick up the dragger
        new_dragger.pickingMode = PickingMode.Ignore;

        elem.RegisterCallback<ChangeEvent<float>>(evt =>
        {
            SliderValueChanged(evt, new_dragger, og_dragger);
        });
        elem.RegisterCallback<GeometryChangedEvent>(evt => {
            SliderInit(evt, new_dragger, og_dragger);
         });
    }

    void SliderValueChanged(ChangeEvent<float> value, VisualElement new_dragger, VisualElement og_dragger)
    {
        // center the dragger to the other dragger
        Vector2 dist = new Vector2((new_dragger.layout.width - og_dragger.layout.width) / 2, (new_dragger.layout.height - og_dragger.layout.height)/ 2);
        // convert the draggers position to world space
        Vector2 pos = og_dragger.parent.LocalToWorld(og_dragger.transform.position);
        // feed coordinates into new draggers coordinate position
        new_dragger.transform.position = new_dragger.parent.WorldToLocal(pos-dist);
    }
    
    void SliderInit(GeometryChangedEvent evt, VisualElement new_dragger, VisualElement og_dragger)
    {
        // center the dragger to the other dragger
        Vector2 dist = new Vector2((new_dragger.layout.width - og_dragger.layout.width) / 2, (new_dragger.layout.height - og_dragger.layout.height) / 2);
        // convert the draggers position to world space
        Vector2 pos = og_dragger.parent.LocalToWorld(og_dragger.transform.position);
        // feed coordinates into new draggers coordinate position
        new_dragger.transform.position = new_dragger.parent.WorldToLocal(pos-dist);
    }
}
