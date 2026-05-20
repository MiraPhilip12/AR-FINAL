using UnityEngine;

public class VehicleTurntable : MonoBehaviour
{
    public float rotationSpeed = 30f;
    private bool isRotating = false;

    void Update()
    {
        if (isRotating)
        {
            transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime);
        }
    }

    public void ToggleRotation()
    {
        isRotating = !isRotating;
    }

    public void SetRotationSpeed(float speed)
    {
        rotationSpeed = speed;
    }

    public bool IsRotating()
    {
        return isRotating;
    }
}