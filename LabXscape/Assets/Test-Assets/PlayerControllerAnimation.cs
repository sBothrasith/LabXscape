using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine.Unity;

public class PlayerController : MonoBehaviour
{
    public SkeletonAnimation skeletonAnimation;
    public AnimationReferenceAsset idle;
    public string currentState;

    // Start is called before the first frame update
    void Start()
    {
        currentState = "Idle";
        setCharacterState(currentState);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void setAnimation(AnimationReferenceAsset animation, bool loop, float timeScale)
    {
        skeletonAnimation.state.SetAnimation(0, animation, loop).TimeScale = timeScale;
    }

    public void setCharacterState(string state)
    {
        if (state.Equals("Idle"))
        {
            setAnimation(idle, true, 1f);
        }
    }
}
