using UnityEngine;
using UnityEditor.Animations;
using UnityEditor;

public class RecordTransformHierarchy : MonoBehaviour
{
    public Animator myAnimator;
    public AnimationClip myClip;
    public AnimatorController myAnimController;


    private GameObjectRecorder m_Recorder;

    void Start()
    {
        // Create recorder and record the script GameObject.
        m_Recorder = new GameObjectRecorder(gameObject);
        myAnimator = this.gameObject.GetComponent<Animator>();
        myClip = new AnimationClip();
        AssetDatabase.CreateAsset(myClip, "Assets/WanderParticles/_Animations/" + this.gameObject.name + "-clip.anim");
        
        // Bind all the Transforms on the GameObject and all its children.
        m_Recorder.BindComponentsOfType<Transform>(gameObject, true);
    }

    void LateUpdate()
    {
        if (myClip == null)
            return;

        // Take a snapshot and record all the bindings values for this frame.
        m_Recorder.TakeSnapshot(Time.deltaTime);
    }

    void OnDisable()
    {
        if (myClip == null)
            return;

        if (m_Recorder.isRecording)
        {
            // Save the recorded session to the clip.
            m_Recorder.SaveToClip(myClip);
            
        }
    }
    
}