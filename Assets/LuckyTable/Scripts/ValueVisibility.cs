using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class ValueVisibility : MonoBehaviour
{
    public int MinValue = 1;
    public int Value
    {
        get { return m_value; }
        set { m_value = value; UpdateImage(); }
    }

    // Start is called before the first frame update
    void Start()
    {
        m_image = GetComponent<Image>();
        Value = -1;
    }

    public void SetValue(int i_value)
    {
        Value = i_value;
    }

    void UpdateImage()
    {
        if (m_image != null)
        {
            m_image.enabled = (Value >= MinValue);
        }
    }

    private Image m_image;
    private int m_value;
}
