using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class GameManager : MonoBehaviour
{

    [SerializeField]
    private Animator sphereAnimator;
    
    private GameObject m_Player;
    private float m_GameTime = 0;
    private bool m_IsGameStarted = false;
    private Vector3 m_PlayerStartPos;
    private Quaternion m_PlayerStartRot;
    private static readonly int StartA = Animator.StringToHash("start");
    private static readonly int EndA = Animator.StringToHash("end");

    private void Start()
    {
        if (Camera.main != null) m_Player = Camera.main.gameObject.transform.parent.gameObject;
        m_PlayerStartPos = m_Player.transform.position;
        m_PlayerStartRot = m_Player.transform.rotation;
    }

    private void Update()
    {
        if (!m_IsGameStarted) return;
        m_GameTime += Time.deltaTime;
            
        if (m_GameTime > 120)
        {
            EndGame();
        }
    }

    public void StartGame()
    {
        sphereAnimator.SetTrigger(StartA);
        m_IsGameStarted = true;
    }
    
    private void EndGame()
    {
        m_IsGameStarted = false;
        sphereAnimator.SetTrigger(EndA);
        m_GameTime = 0;
        
        StartCoroutine(MovePlayer());

    }
    
    private IEnumerator MovePlayer()
    {
        var pos = m_Player.transform.position;
        var rot = m_Player.transform.rotation;
        var t = 0f;
        while (t < 1)
        {
            t += Time.deltaTime;
            m_Player.transform.position = Vector3.Lerp(pos, m_PlayerStartPos, t);
            m_Player.transform.rotation = Quaternion.Lerp(rot, m_PlayerStartRot, t);
            yield return null;
        }
    }
    
    public void ExitGame()
    {
        Application.Quit();
    }

}
