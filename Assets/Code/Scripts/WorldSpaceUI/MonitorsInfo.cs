using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

public class MonitorsInfo : MonoBehaviour
{
    [Header("External Dependencies")]
    [SerializeField] private GlobalVariables _globalVariables;

    [Header("Monitors")]
    [SerializeField] private Image[] _monitors;

    [Header("Textures")]
    [SerializeField] private Sprite greenMonitor;
    [SerializeField] private Sprite redMonitor;


    private void Start() {
        _globalVariables.onMonitorTurretCountChanged.AddListener(ManageMonitors);
    }

    private void ManageMonitors(int monitor) {
        if (_globalVariables.CanPlaceTurret(monitor)) {
            _monitors[monitor-1].sprite = greenMonitor;
        }
        else {
            _monitors[monitor-1].sprite = redMonitor;
        }
    }
}
