using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.XR;

// Attach to the Shadow prefab
class ShadowReplayer : MonoBehaviour
{
    List<InputFrame> playbackData;
    float startTime;

    void Init(List<InputFrame> data)
    {
        playbackData = data;
        startTime = Time.time;
    }

    InputFrame FindFrameForTime(float elapsed)
    {
        if (playbackData == null || playbackData.Count == 0)
            return null;

        // Find the last frame that happened before "elapsed"
        InputFrame closest = playbackData[0];
        foreach (InputFrame frame in playbackData)
        {
            if (frame.time <= elapsed)
            {
                closest = frame;
            }
            else
            {
                break;
            }
        }
        return closest;
    }


    void Update()
    {
        // float elapsed = Time.time - startTime;

        // // Find the recorded frame that matches current time
        // InputFrame frame = FindFrameForTime(elapsed);

        // // Send inputs into a PlayerController but in AI mode
        // controller.Move(frame.horizontal);
        // if (frame.jump) controller.Jump();
        // if (frame.attack) controller.Attack();
    }
}
