using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using static UnityEngine.Time;

public class Teleporter : MonoBehaviour
{

    [SerializeField] private float timeToInteract = 0;
    
    private Animator m_Animator;
    private float m_TimePointed = 0;
    private bool m_IsPointed = false;
    private static readonly int AnimString = Animator.StringToHash("transition");

    // Start is called before the first frame update
    private void Start()
    {
        m_Animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    private void Update()
    {
        SetTimePointed();
        Transition();
    }
    
    private void SetTimePointed()
    {
        if (m_IsPointed)
        {
            m_TimePointed += deltaTime;
            
        }else if (m_TimePointed > 0)
        {
            m_TimePointed -= deltaTime;
        }else
        {
            m_TimePointed = 0;
        }
        
        if (!(m_TimePointed > timeToInteract)) return;
        m_IsPointed = false;
        OnPointerClick();
    }

    private void Transition()
    {
        
        m_Animator.SetFloat(AnimString, m_TimePointed);

    }

    private void Teleport()
    {
        
        
    }
    
    public void OnPointerEnter()
    {
        m_IsPointed = true;
    }
    
    public void OnPointerExit()
    {
        m_IsPointed = false;
        m_TimePointed = 0;
    }
    
    public void OnPointerClick()
    {
        Teleport();
    }
}
