using System;
using UnityEngine;
using UnityEngine.Playables;


public class TriggerEnterToContinue : MonoBehaviour
{
    public PlayableDirector Timeline;
    public float ResumeAfterSeconds;

    private double _originalTimelineSpeed;

    void OnEnable ()
    {
        _originalTimelineSpeed = Timeline.playableGraph.GetRootPlayable(0).GetSpeed();
        Timeline.playableGraph.GetRootPlayable(0).SetSpeed(0);
        Invoke(nameof(Unpause), ResumeAfterSeconds);
    }

    private void OnTriggerEnter(Collider other)
    {
        CancelInvoke();
        Unpause();
    }

    private void Unpause()
    {
        Timeline.playableGraph.GetRootPlayable(0).SetSpeed(_originalTimelineSpeed);
    }
}