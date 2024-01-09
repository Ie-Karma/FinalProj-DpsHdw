using UnityEngine;

public class Blackboard : MonoBehaviour
{
    private bool m_IsPointed = false;
    private GameObject m_Pointer;
    private Camera m_Camera;
    private bool m_IsmCameraNull;
    private const float MaxDistance = 1000;

    private void Start()
    {
        m_Camera = Camera.main;
        m_Pointer = transform.GetChild(0).gameObject;
    }

    private void Update()
    {
        if (m_IsPointed)
        {
            m_Pointer.transform.position = Vector3.Lerp(m_Pointer.transform.position, GetPoint(), 1);
            
        }
    }
    public void OnPointerEnter()
    {
        m_IsPointed = true;
        m_Pointer.transform.position = GetPoint();
    }
    
    public void OnPointerExit()
    {
        m_IsPointed = false;
    }

    private Vector3 GetPoint()
    {
        if (!m_Camera) return Vector3.zero;
        var rayFromCam = m_Camera.ScreenPointToRay(new Vector3(Screen.width / 2f, Screen.height / 2f, 0f));
        
        if (Physics.Raycast(rayFromCam, out var hitInfo))
        {
            return hitInfo.distance > MaxDistance ? m_Pointer.transform.position : hitInfo.point;
        } else
        {            
            return m_Pointer.transform.position;
        }

    }
    
}
