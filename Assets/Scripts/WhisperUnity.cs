using System;
using System.IO;
using System.Runtime.InteropServices;
using UnityEngine;

public class WhisperUnity : MonoBehaviour
{
    [DllImport("whisper", CallingConvention = CallingConvention.Cdecl)]
    private static extern IntPtr whisper_init(string modelPath);

    [DllImport("whisper", CallingConvention = CallingConvention.Cdecl)]
    private static extern int whisper_full(IntPtr ctx, float[] samples, int sampleCount);

    [DllImport("whisper", CallingConvention = CallingConvention.Cdecl)]
    private static extern IntPtr whisper_full_get_segment_text(IntPtr ctx, int segment);

    [DllImport("whisper", CallingConvention = CallingConvention.Cdecl)]
    private static extern int whisper_full_n_segments(IntPtr ctx);

    public VoiceCommandController commandController;

    private IntPtr ctx;
    private AudioClip mic;
    private float[] buffer;

    void Start()
    {
        string modelPath = Path.Combine(Application.streamingAssetsPath, "ggml-tiny.en.bin");
        ctx = whisper_init(modelPath);

        mic = Microphone.Start(null, true, 2, 16000);
        buffer = new float[32000];

        Debug.Log("Whisper READY");
        InvokeRepeating("ProcessAudio", 1f, 0.5f); // 
    }

    void ProcessAudio()
    {
        if (mic == null) return;

        int pos = Microphone.GetPosition(null);
        if (pos <= 0) return;

        mic.GetData(buffer, 0);

        int result = whisper_full(ctx, buffer, buffer.Length);
        if (result != 0) return;

        int n = whisper_full_n_segments(ctx);
        if (n <= 0) return;

        string text = Marshal.PtrToStringAnsi(whisper_full_get_segment_text(ctx, n - 1));

        if (!string.IsNullOrEmpty(text))
        {
            Debug.Log("Whisper: " + text);
            commandController.ExecuteCommand(text);
        }
    }
}
