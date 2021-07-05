using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]

public class CharacterIK : MonoBehaviour
{
    protected Animator animator;
    public Vector3 FootIK_Offset;
    public CharacterMovement character;
    private float ik_weight;
    private float Lerp_Speed = 10f;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (character.Velocity.magnitude > 1f)
        {
            ik_weight = Mathf.Lerp(ik_weight, 0, Time.deltaTime * Lerp_Speed);
        }
        else
        {
            ik_weight = Mathf.Lerp(ik_weight, 1, Time.deltaTime * Lerp_Speed);
        }
    }

    void OnAnimatorIK()
    {
        Vector3 L_Foot = animator.GetBoneTransform(HumanBodyBones.LeftFoot).position;
        Vector3 R_Foot = animator.GetBoneTransform(HumanBodyBones.RightFoot).position;

        L_Foot = GetHitPoint(L_Foot + Vector3.up, L_Foot - Vector3.up*5) + FootIK_Offset;
        R_Foot = GetHitPoint(R_Foot + Vector3.up, R_Foot - Vector3.up*5) + FootIK_Offset;

        transform.localPosition = new Vector3(0, -Mathf.Abs(L_Foot.y - R_Foot.y) / 2 * ik_weight, 0);

        animator.SetIKPositionWeight(AvatarIKGoal.LeftFoot, ik_weight);
        animator.SetIKPosition(AvatarIKGoal.LeftFoot, L_Foot);

        animator.SetIKPositionWeight(AvatarIKGoal.RightFoot, ik_weight);
        animator.SetIKPosition(AvatarIKGoal.RightFoot, R_Foot);
    }

    private Vector3 GetHitPoint(Vector3 start, Vector3 end)
    {
        RaycastHit hit;
        if (Physics.Linecast(start, end, out hit))
        {
            return hit.point;
        }
        return end;
    }
}
