using Cinemachine;
using UnityEngine;

[RequireComponent(typeof(CinemachineFreeLook))]
public class ClickAndDragCameraControl : MonoBehaviour
{
    // Some comment
    CinemachineFreeLook cameraFreeLook;
    float xSpeed;
    float ySpeed;
    // Start is called before the first frame update
    void Start()
    {
        cameraFreeLook = GetComponent<CinemachineFreeLook>();
        xSpeed = cameraFreeLook.m_XAxis.m_MaxSpeed;
        ySpeed = cameraFreeLook.m_YAxis.m_MaxSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        if ( Input.GetMouseButton( 1 ) )
        {
            cameraFreeLook.m_XAxis.m_MaxSpeed = xSpeed;
            cameraFreeLook.m_YAxis.m_MaxSpeed = ySpeed;
        }
        else
        {
            cameraFreeLook.m_XAxis.m_MaxSpeed = 0;
            cameraFreeLook.m_YAxis.m_MaxSpeed = 0;
        }
    }
}
