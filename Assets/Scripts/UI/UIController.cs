using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIController : MonoBehaviour
{
    [Header("Main Menu Buttons")]
    public Button btnMarkerless;
    public Button btnMarkerBased;
    public Button btnQuit;

    [Header("Vehicle UI Panel")]
    public GameObject vehicleUIPanel;
    public Button btnChangeColor;
    public Button btnChangeWheel;
    public Button btnEngineToggle;
    public Button btnRotationToggle;

    private GameObject currentVehicle;
    private bool isUIVisible = false;

    void Start()
    {
        if (btnMarkerless != null)
            btnMarkerless.onClick.AddListener(StartMarkerlessMode);

        if (btnMarkerBased != null)
            btnMarkerBased.onClick.AddListener(StartMarkerBasedMode);

        if (btnQuit != null)
            btnQuit.onClick.AddListener(QuitGame);

        if (btnChangeColor != null)
            btnChangeColor.onClick.AddListener(OnColorButtonPressed);

        if (btnChangeWheel != null)
            btnChangeWheel.onClick.AddListener(OnWheelButtonPressed);

        if (btnEngineToggle != null)
            btnEngineToggle.onClick.AddListener(OnEngineButtonPressed);

        if (btnRotationToggle != null)
            btnRotationToggle.onClick.AddListener(OnRotationButtonPressed);

        if (vehicleUIPanel != null)
            vehicleUIPanel.SetActive(false);
    }

    void StartMarkerlessMode()
    {
        if (AudioManager.Instance != null)
            AudioManager.Instance.PlayButtonClick();
        SceneManager.LoadScene("AR_Markerless");
    }

    void StartMarkerBasedMode()
    {
        if (AudioManager.Instance != null)
            AudioManager.Instance.PlayButtonClick();
        SceneManager.LoadScene("AR_MarkerBased");
    }

    void QuitGame()
    {
        if (AudioManager.Instance != null)
            AudioManager.Instance.PlayButtonClick();

#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }

    public void OnVehicleSpawned(GameObject vehicle)
    {
        currentVehicle = vehicle;
        if (vehicleUIPanel != null)
            vehicleUIPanel.SetActive(true);
        isUIVisible = true;
    }

    void OnColorButtonPressed()
    {
        if (AudioManager.Instance != null)
            AudioManager.Instance.PlayButtonClick();

        if (currentVehicle != null)
        {
            VehicleCustomization custom = currentVehicle.GetComponent<VehicleCustomization>();
            if (custom != null) custom.ChangeColor();
        }
    }

    void OnWheelButtonPressed()
    {
        if (AudioManager.Instance != null)
            AudioManager.Instance.PlayButtonClick();

        if (currentVehicle != null)
        {
            VehicleCustomization custom = currentVehicle.GetComponent<VehicleCustomization>();
            if (custom != null) custom.ChangeWheels();
        }
    }

    void OnEngineButtonPressed()
    {
        if (AudioManager.Instance != null)
        {
            AudioManager.Instance.PlayButtonClick();
            AudioManager.Instance.ToggleEngine();
        }

        if (btnEngineToggle != null)
        {
            Text text = btnEngineToggle.GetComponentInChildren<Text>();
            if (text != null)
            {
                bool isRunning = AudioManager.Instance != null && AudioManager.Instance.IsEngineRunning();
                text.text = isRunning ? "ENGINE ON" : "ENGINE OFF";
            }
        }
    }

    void OnRotationButtonPressed()
    {
        if (AudioManager.Instance != null)
            AudioManager.Instance.PlayButtonClick();

        if (currentVehicle != null)
        {
            VehicleTurntable turntable = currentVehicle.GetComponent<VehicleTurntable>();
            if (turntable != null) turntable.ToggleRotation();
        }

        if (btnRotationToggle != null)
        {
            Text text = btnRotationToggle.GetComponentInChildren<Text>();
            if (text != null)
            {
                bool isRotating = currentVehicle != null &&
                    currentVehicle.GetComponent<VehicleTurntable>() != null &&
                    currentVehicle.GetComponent<VehicleTurntable>().IsRotating();
                text.text = isRotating ? "ROTATE OFF" : "ROTATE ON";
            }
        }
    }
}