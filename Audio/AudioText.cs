using UnityEngine;
using UnityEngine.UI;

public class AudioText : MonoBehaviour
{
    [SerializeField] private string volumeName;
    [SerializeField] private string textIntro;

    private Text text;
    private void Awake()
    {
        text = GetComponent<Text>();
    }

    private void Update()
    {
        UpdateVolume();
    }

    private void UpdateVolume()
    {
        float volumeValue = PlayerPrefs.GetFloat(volumeName) * 100;
        text.text = textIntro + Mathf.RoundToInt(volumeValue).ToString() + "%";
    }
}
