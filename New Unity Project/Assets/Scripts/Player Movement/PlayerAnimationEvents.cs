using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationEvents : MonoBehaviour
{
    public AudioClip aud_footstep;
    public AudioSource Audio;
   
    public void Footstep(float TargetWalkSpeed)
    {
        float actualSpeed = CharacterMovement.velocity.magnitude;

        if (Getmovementstate(TargetWalkSpeed) == Getmovementstate(actualSpeed))
        {
            Audio.pitch = Mathf.Clamp((actualSpeed / 5),1f, 1.5f);
            Audio.volume = Mathf.Clamp((actualSpeed / 5), 0.3f, 1.5f);

            Audio.PlayOneShot(aud_footstep);

        }
    }

    private int Getmovementstate(float speed)
    {
        if (speed<0.5)
        {
            return 0;
        }

        if (speed < 10)
        {
            return 5;
        }

        return 10;
    }
}
