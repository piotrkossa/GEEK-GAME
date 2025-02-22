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

    private void ManageMonitors() {
        for (int i = 0; i < _globalVariables.MonitorTurretCount.Count; i++)

        if (_globalVariables.CanPlaceTurret(i+1)) {
            _monitors[i].sprite = greenMonitor;
        }
        else {
            _monitors[i].sprite = redMonitor;
        }
    }
}
