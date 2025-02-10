using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class MicrophoneInput : MonoBehaviour
{
    public Text outputText;
    public Button recordButton;

    private AudioClip recordedAudio;
    private string deviceName;

    void Start()
    {
        recordButton.onClick.AddListener(RecordAudio);
    }

    public void RecordAudio()
    {
        deviceName = Microphone.devices[0];
        recordedAudio = Microphone.Start(deviceName, false, 10, 44100);
    }

    void Update()
    {
        if (recordedAudio != null && Microphone.GetPosition(deviceName) > 0)
        {
            float[] samples = new float[recordedAudio.samples];
            recordedAudio.GetData(samples, 0);

            float level = 0;
            foreach (var sample in samples)
            {
                level += Mathf.Abs(sample);
            }
            level /= samples.Length;

            outputText.text = "Level: " + level.ToString();

            if (Microphone.GetPosition(deviceName) >= recordedAudio.samples)
            {
                Microphone.End(deviceName);
                StartCoroutine(ConvertAudioToTranscript());
            }
        }
    }

    IEnumerator ConvertAudioToTranscript()
    {
        // You would need to use a speech-to-text API or library here to convert the audio to text.
        // For example, you could use the Unity Speech Recognition package or the Google Cloud Speech-to-Text API.

        // For the sake of this example, let's assume you have a function called "SpeechToText" that takes the audio clip and returns a string.
        string transcript = SpeechToText(recordedAudio);

        outputText.text = "Transcript: " + transcript;

        yield return null;
    }

    string SpeechToText(AudioClip audioClip)
    {
        // Implement your speech-to-text conversion logic here.
        // This could involve using a speech-to-text API or library.

        // For this example, let's just return a placeholder string.
        return "Your speech transcript goes here.";
    }
}