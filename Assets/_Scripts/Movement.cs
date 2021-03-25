using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;

public class Movement : MonoBehaviour
{
    public XRNode InputSource;
    private Vector2 inputAxis;
    private XRRig rig;
    private CharacterController _character;
    public float fallingSpeed = 10;
    public LayerMask groundLayer;
    public float additionalHeight;
    private void Start()
    {
        _character = GetComponent<CharacterController>();
        rig = GetComponent<XRRig>();
    }

    private void Update()
    {
        var device = InputDevices.GetDeviceAtXRNode(InputSource);
        device.TryGetFeatureValue(CommonUsages.primary2DAxis, out inputAxis);
    }

    private void FixedUpdate()
    {
        var headYaw = Quaternion.Euler(0, rig.cameraGameObject.transform.eulerAngles.y, 0);
        var direction = headYaw * new Vector3(inputAxis.x, 0, inputAxis.y);
        _character.Move(direction * Time.fixedDeltaTime * 10);

        if (isGrounded())
            fallingSpeed = 0;
        else
            fallingSpeed += -9.81f * Time.fixedDeltaTime;
        _character.Move(Vector3.up * fallingSpeed * Time.fixedDeltaTime);
    }
    bool isGrounded()
    {
        var rayStart = transform.TransformPoint(_character.center);
        var rayLength = _character.center.y + 0.01f;
        var hasHit = Physics.SphereCast(rayStart, _character.radius, Vector3.down, out RaycastHit hitInfo, rayLength, groundLayer);
        return hasHit;
    }
    void CapsuleFollowHeadset()
    {
        _character.height = rig.cameraInRigSpaceHeight;
        var capsuleCenter = transform.InverseTransformPoint(rig.cameraGameObject.transform.position);
    }
}