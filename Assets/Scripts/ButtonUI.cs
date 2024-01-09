using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using static UnityEngine.Time;

public class ButtonUI : MonoBehaviour
{

    [SerializeField] private float timeToInteract = 0;
    
    [Space,Header("Transition")]
    [SerializeField] private Color endColor = Color.red;
    [SerializeField] private float endSize = 1.5f;

    [Space, Header("Events")]
    [SerializeField] private UnityEvent pointed;
    [SerializeField] private UnityEvent exit;
    [SerializeField] private  UnityEvent activated;
    
    private Color m_StartColor = Color.white;
    private float m_StartSize = 1;
    private Image m_Image;
    private float m_TimePointed = 0;
    private bool m_IsPointed = false;
    
    // Start is called before the first frame update
    private void Start()
    {
        m_Image = GetComponent<Image>();
        m_StartColor = m_Image.color;
        m_StartSize = transform.localScale.x;
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
        m_Image.color = Color.Lerp(m_StartColor, endColor, m_TimePointed / timeToInteract);
        transform.localScale = Vector3.Lerp(Vector3.one * m_StartSize, Vector3.one * endSize, m_TimePointed / timeToInteract);
    }
    
    public void OnPointerEnter()
    {
        pointed.Invoke();
        m_IsPointed = true;
    }
    
    public void OnPointerExit()
    {
        exit.Invoke();
        m_IsPointed = false;
        m_TimePointed = 0;
    }
    
    public void OnPointerClick()
    {
        activated.Invoke();
    }

}
