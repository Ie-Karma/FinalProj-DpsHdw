using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using static UnityEngine.Time;

public class Interactable : MonoBehaviour
{

    [SerializeField, Header("Display text")]
    private string info;
    
    private bool m_IsOpen = false;
    private Transform m_Panel;
    private Animator m_Animator;
    private static readonly int OpenAnim = Animator.StringToHash("open");
    private static readonly int CloseAnim = Animator.StringToHash("close");
    private Camera m_Camera;

    // Start is called before the first frame update
    private void Start()
    {
        m_Camera = Camera.main;
        m_Animator = GetComponent<Animator>();
        GetComponentInChildren<TextMeshProUGUI>().text = info;
        m_Panel = transform.GetChild(0);
    }

    // Update is called once per frame
    private void Update()
    {
        if (m_IsOpen)
        {
            RotatePanel();
        }
    }
    
    private void RotatePanel()
    {
        if (m_Camera) m_Panel.LookAt(m_Camera.transform);
    }

    public void OnPointerEnter()
    {
        if (m_IsOpen) return;
        m_IsOpen = true;
        m_Animator.SetTrigger(OpenAnim);

    }
    
    public void OnPointerExit()
    {
        if (!m_IsOpen) return;
        m_IsOpen = false;
        m_Animator.SetTrigger(CloseAnim);
    }
    
}
