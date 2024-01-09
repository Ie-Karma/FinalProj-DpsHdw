using UnityEngine;

public class MovementController : MonoBehaviour
{
    public float moveSpeed = 5f;

    public float Sensitivity {
        get { return sensitivity; }
        set { sensitivity = value; }
    }
    [Range(0.1f, 9f)][SerializeField] float sensitivity = 2f;
    [Tooltip("Limitacion del angulo de la camara")]
    [Range(0f, 90f)][SerializeField] float yRotationLimit = 88f;

    Vector2 m_Rotation = Vector2.zero;
    const string XAxis = "Mouse X";
    const string YAxis = "Mouse Y";
    
#if UNITY_EDITOR
    void OnEnable()
    {
        // Activa el script solo en el editor
        enabled = true;
    }
#else
    void OnEnable()
    {
        // Desactiva el script fuera del editor
        enabled = false;
    }
#endif
    
    void Start()
    {
        // Ocultar y bloquear el cursor al centro de la pantalla
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
    
    void Update()
    {
        // Movimiento
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        Vector3 movement = new Vector3(horizontalInput, 0f, verticalInput) * (moveSpeed * Time.deltaTime);
        transform.Translate(movement);

        // Rotación con el ratón
        RotateWithMouse();
    }
    

    public void RotateWithMouse(){
        m_Rotation.x += Input.GetAxis(XAxis) * sensitivity;
        m_Rotation.y += Input.GetAxis(YAxis) * sensitivity;
        m_Rotation.y = Mathf.Clamp(m_Rotation.y, -yRotationLimit, yRotationLimit);
        var xQuat = Quaternion.AngleAxis(m_Rotation.x, Vector3.up);
        var yQuat = Quaternion.AngleAxis(m_Rotation.y, Vector3.left);

        transform.localRotation = xQuat * yQuat;
    }
}