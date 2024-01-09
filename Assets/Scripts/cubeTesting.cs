using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;
using static UnityEngine.Time;

public class cubeTesting : MonoBehaviour
{
    
    private MeshRenderer m_MeshRenderer;

    [SerializeField] private float timeToInteract = 0;
    public UnityEvent Activated;
    
    private float m_TimePointed = 0;
    private bool m_IsPointed = false;
    // Start is called before the first frame update
    void Start()
    {
        m_MeshRenderer = GetComponent<MeshRenderer>();
        
    }

    // Update is called once per frame
    void Update()
    {
        if (m_IsPointed)
        {
            m_TimePointed += deltaTime;
            if (m_TimePointed > timeToInteract)
            {
                //m_TimePointed = 0;
                m_IsPointed = false;
                OnPointerClick();
            }
        }
    }
    
    private void ChangeColor(int n)
    {
        var material = m_MeshRenderer.material;
        material.color = n switch
        {
            0 => Color.red,
            1 => Color.blue,
            2 => Color.green,
            _ => material.color
        };
    }
    
    public void OnPointerEnter()
    {
        m_IsPointed = true;
        ChangeColor(0);
    }
    
    public void OnPointerExit()
    {
        m_IsPointed = false;
        m_TimePointed = 0;
        ChangeColor(1);
    }
    
    public void OnPointerClick()
    {
        ChangeColor(2);
        Activated.Invoke();
    }
    
}
