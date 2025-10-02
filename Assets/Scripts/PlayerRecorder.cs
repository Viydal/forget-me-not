// using System.Collections.Generic;
using UnityEngine;

class InputFrame
{
    public float time;
    public float horizontal;
    public bool jump;
    public bool attack;
}

class PlayerRecorder : MonoBehaviour
{
    List<InputFrame> currentRecording;
    bool isRecording = false;
    float startTime;

    void StartRecording()
    {
        currentRecording = new List<InputFrame>();
        isRecording = true;
        startTime = Time.time;
    }

    void Update()
    {
        if (!isRecording) return;

        // Capture inputs each frame
        InputFrame frame = new InputFrame();
        frame.time = Time.time - startTime;
        frame.horizontal = Input.GetAxisRaw("Horizontal");
        frame.jump = Input.GetButtonDown("Jump");
        frame.attack = Input.GetButtonDown("Fire1");

        currentRecording.Add(frame);
    }

    List<InputFrame> StopRecording()
    {
        isRecording = false;
        return currentRecording;
    }
}