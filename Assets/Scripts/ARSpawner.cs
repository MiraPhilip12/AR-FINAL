using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class ARSpawner : MonoBehaviour
{
    public GameObject[] vehiclePrefabs;
    public GameObject currentVehicle;

    private ARRaycastManager raycastManager;
    private ARTrackedImageManager imageManager;
    private bool isMarkerlessMode = true;

    void Start()
    {
        raycastManager = GetComponent<ARRaycastManager>();
        imageManager = GetComponent<ARTrackedImageManager>();

        if (imageManager != null)
            imageManager.trackablesChanged.AddListener(OnImagesChanged);
    }

    void Update()
    {
        if (isMarkerlessMode && Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Began)
            {
                List<ARRaycastHit> hits = new List<ARRaycastHit>();
                if (raycastManager.Raycast(touch.position, hits, TrackableType.PlaneWithinPolygon))
                {
                    Pose hitPose = hits[0].pose;
                    SpawnVehicle(hitPose.position, hitPose.rotation);
                }
            }
        }
    }

    void OnImagesChanged(ARTrackablesChangedEventArgs<ARTrackedImage> args)
    {
        foreach (var image in args.added)
        {
            SpawnVehicle(image.transform.position, image.transform.rotation);
        }
    }

    public void SpawnVehicle(Vector3 position, Quaternion rotation)
    {
        if (currentVehicle != null)
            Destroy(currentVehicle);

        if (vehiclePrefabs.Length > 0)
        {
            currentVehicle = Instantiate(vehiclePrefabs[0], position, rotation);
            FindObjectOfType<UIController>()?.OnVehicleSpawned(currentVehicle);
        }
    }

    public void SetMarkerlessMode(bool enabled)
    {
        isMarkerlessMode = enabled;
    }
}