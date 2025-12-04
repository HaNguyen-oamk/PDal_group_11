
using UnityEngine;
using Vosk;
using System.Collections;

public class VoiceRecognitionVosk : MonoBehaviour
{
    public string modelPath = "vosk-model-en-us-0.22-lgraph";
    public VoiceCommandController commandController;   // <-- add

    private VoskRecognizer recognizer;
    private Model model;

    private AudioClip micClip;
    private int samplePos = 0;

    void Start()
    {
        StartCoroutine(InitRecognizer());
    }

    IEnumerator InitRecognizer()
    {
        string path = System.IO.Path.Combine(Application.streamingAssetsPath, modelPath);
        model = new Model(path);
        recognizer = new VoskRecognizer(model, 16000.0f);

        micClip = Microphone.Start(null, true, 1, 16000);
        Debug.Log("VOSK READY");

        yield return null;
    }

    void Update()
    {
        if (micClip == null) return;

        int micPosition = Microphone.GetPosition(null);
        int diff = micPosition - samplePos;
        if (diff < 0) diff += micClip.samples;

       if (diff <= 0) return;
       if (micClip.samples == 0) return;
       
       float[] samples = new float[diff];
       micClip.GetData(samples, samplePos);


        samplePos = micPosition;

        // convert to bytes
        byte[] bytes = new byte[samples.Length * 2];
        int res = 0;
        for (int i = 0; i < samples.Length; i++)
        {
            short val = (short)(samples[i] * 32767);
            bytes[res++] = (byte)(val & 0xff);
            bytes[res++] = (byte)((val >> 8) & 0xff);
        }

        if (recognizer.AcceptWaveform(bytes, bytes.Length))
        {
            string result = recognizer.Result();
            Debug.Log("FINAL: " + result);
            commandController.ExecuteCommand(result);   // 
        }
        else
        {
            string partial = recognizer.PartialResult();
        }
    }
}
