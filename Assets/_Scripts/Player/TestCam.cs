using UnityEngine;
using System.Collections;
using UnityEngine.Assertions;

public class TestCam : MonoBehaviour {

    public GameObject Target;
    public Vector3 Offset;
    public float LerpFactor = 0.25f;

    Vector3 targetPos;
    private float interpVelocity;
    private Vector3 originalOffset;
    // Use this for initialization
    void Start()
    {
        targetPos = transform.position;
        originalOffset = Offset;

       Assert.IsNotNull<GameObject>(Target, "TestCam > No tracking target assigned.");
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Target)
        {
            Vector3 targetDirection = Target.transform.position - transform.position;

            interpVelocity = targetDirection.magnitude * 5f;

            targetPos = transform.position + (targetDirection.normalized * interpVelocity * Time.deltaTime);

            transform.position = Vector3.Lerp(transform.position, targetPos + Offset, LerpFactor);

        }
    }

    public void SetZoomOffset(float value)
    {
        Offset.y = value;
    }

    public void ResetZoomOffset()
    {
        Offset = originalOffset;
    }
}
