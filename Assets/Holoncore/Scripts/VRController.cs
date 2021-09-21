using UnityEngine;
using Valve.VR;

public class VRController : MonoBehaviour
{

    public float m_sensitivity = 0.1f;
    public float m_maxSpeed = 1.0f;

    public SteamVR_Action_Boolean m_MovePress = null;
    public SteamVR_Action_Vector2 m_MoveValue = null;

    private float m_speed = 0.0f;

    private CharacterController m_CharacterController = null;
    private Transform m_CameraRig = null;
    private Transform m_Head = null;

    private void Awake()
    {
        m_CharacterController = GetComponent<CharacterController>();
    }

    // Start is called before the first frame update
    private void Start()
    {
        m_CameraRig = SteamVR_Render.Top().origin;
        m_Head = SteamVR_Render.Top().head;
    }

    // Update is called once per frame
    private void Update()
    {
        HandleHead();
        HandleHeight();
        CalculateMovement();
        
    }

    private void HandleHead()
    {
        //store current
        Vector3 oldPosition = m_CameraRig.position;
        Quaternion oldRotation = m_CameraRig.rotation;

        //rotation
        transform.eulerAngles = new Vector3(0.0f, m_Head.rotation.eulerAngles.y, 0.0f);

        //restore
        m_CameraRig.position = oldPosition;
        m_CameraRig.rotation = oldRotation;

    }

    private void CalculateMovement()
    {
        //figure out movement orientation

        Vector3 orientationEuler = new Vector3(0, transform.eulerAngles.y, 0);
        Quaternion orientation = Quaternion.Euler(orientationEuler);
        Vector3 movement = Vector3.zero;

        //if not moving
        if (m_MovePress.GetStateUp(SteamVR_Input_Sources.Any))
            m_speed = 0;

        //if button pressed
        if (m_MovePress.state)
        {
            //Add, clamp
            m_speed += m_MoveValue.axis.y * m_sensitivity;
            m_speed = Mathf.Clamp(m_speed, -m_maxSpeed, m_maxSpeed);

            //orientation
            movement += orientation * (m_speed * Vector3.forward) * Time.deltaTime;
        }
        //apply
        m_CharacterController.Move(movement);
    }


    private void HandleHeight()
    {
        //get the head in local space
        float headHeight = Mathf.Clamp(m_Head.localPosition.y, 1, 2);
        m_CharacterController.height = headHeight;

        //cut in half
        Vector3 newCenter = Vector3.zero;
        newCenter.y = m_CharacterController.height / 2;
        newCenter.y += m_CharacterController.skinWidth;

        //move capsule in local space
        newCenter.x = m_Head.localPosition.x;
        newCenter.z = m_Head.localPosition.z;

        //rotate;
        newCenter = Quaternion.Euler(0, -transform.eulerAngles.y, 0) * newCenter;

        //apply
        m_CharacterController.center = newCenter;
    }

}
