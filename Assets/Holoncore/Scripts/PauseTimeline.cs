using System;
using UnityEngine;
using UnityEngine.Playables;


public class PauseTimeline : MonoBehaviour
{
    public PlayableDirector Timeline;
    public float ResumeAfterSeconds = 9999;

    private double _originalTimelineSpeed;

    void OnEnable ()
    {
        _originalTimelineSpeed = Timeline.playableGraph.GetRootPlayable(0).GetSpeed();
        Timeline.playableGraph.GetRootPlayable(0).SetSpeed(0);
        Invoke(nameof(Unpause), ResumeAfterSeconds);
    }

    public void Unpause()
    {
        CancelInvoke();
        Timeline.playableGraph.GetRootPlayable(0).SetSpeed(_originalTimelineSpeed);
    }
}