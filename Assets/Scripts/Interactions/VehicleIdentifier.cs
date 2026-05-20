using UnityEngine;

public class VehicleIdentifier : MonoBehaviour
{
    public int carIndex = 0;
    public int voiceoverClipIndex = 0;

    void Start()
    {
        gameObject.tag = "Vehicle";
    }

    public int GetCarIndex()
    {
        return carIndex;
    }
}