using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[RequireComponent(typeof(TMP_Text))]
public class ValueDisplay : MonoBehaviour
{
    public string FormatString;
    public int InitialValue = 0;
    public int Value
    {
        get { return m_value; }
        set { m_value = value; UpdateText(); }
    }

    // Start is called before the first frame update
    void Start()
    {
        m_text = GetComponent<TMP_Text>();
        Value = InitialValue;
    }

    public void SetValue(int i_value)
    {
        Value = i_value;
    }

    void UpdateText()
    {
        if (m_text != null)
        {
            m_text.text = string.Format(FormatString, Value);
        }
    }

    private TMP_Text m_text;
    private int m_value;
}
