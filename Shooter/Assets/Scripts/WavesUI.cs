using UnityEngine;
using TMPro;

public class WavesUI : MonoBehaviour
{
    private TextMeshProUGUI _text;

    private void Start()
    {
        _text = GetComponent<TextMeshProUGUI>();
        WaveManager.SharedInstance.onWaveChanged.AddListener(RefreshText);
        RefreshText();
    }

    private void RefreshText()
    {
        int currentPhysicalWave = WaveManager.SharedInstance.MaxWaves - WaveManager.SharedInstance.WavesCount;
        int uiCurrentWave = Mathf.CeilToInt(currentPhysicalWave / 2f);
        int uiMaxWaves = Mathf.CeilToInt(WaveManager.SharedInstance.MaxWaves / 2f);

        _text.text = "WAVE: " + uiCurrentWave + "/" + uiMaxWaves;
    }
}