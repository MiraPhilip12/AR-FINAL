using UnityEngine;

public class VehicleCustomization : MonoBehaviour
{
    public Material[] bodyColors;
    public Renderer bodyRenderer;
    private int currentColorIndex = 0;

    public GameObject[] wheelPrefabs;
    public Transform[] wheelPositions;
    private int currentWheelIndex = 0;
    private GameObject[] currentWheels;

    public void ChangeColor()
    {
        if (bodyColors.Length == 0 || bodyRenderer == null) return;

        currentColorIndex = (currentColorIndex + 1) % bodyColors.Length;
        bodyRenderer.material = bodyColors[currentColorIndex];
    }

    public void ChangeWheels()
    {
        if (wheelPrefabs.Length == 0 || wheelPositions.Length == 0) return;

        if (currentWheels != null)
        {
            foreach (GameObject wheel in currentWheels)
                if (wheel != null) Destroy(wheel);
        }

        currentWheelIndex = (currentWheelIndex + 1) % wheelPrefabs.Length;
        currentWheels = new GameObject[wheelPositions.Length];

        for (int i = 0; i < wheelPositions.Length && i < wheelPrefabs.Length; i++)
        {
            if (wheelPrefabs[currentWheelIndex] != null && wheelPositions[i] != null)
            {
                currentWheels[i] = Instantiate(wheelPrefabs[currentWheelIndex], wheelPositions[i]);
                currentWheels[i].transform.localPosition = Vector3.zero;
                currentWheels[i].transform.localRotation = Quaternion.identity;
            }
        }
    }
}