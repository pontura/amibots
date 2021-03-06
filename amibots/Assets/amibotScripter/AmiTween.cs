﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmiTween : MonoBehaviour {

    Character character;

    Vector2 foot_r_OriginalPos;
    Vector2 foot_l_OriginalPos;
    Vector2 arm_r_OriginalPos;
    Vector2 arm_l_OriginalPos;
	Vector2 head_OriginalPos;

    public Vector2 body_r_OriginalPos;

    void Start () {
        character = GetComponent<Character>();

        AlignBodyToFoots();

        foot_r_OriginalPos = character.foot_right.transform.localPosition;
        foot_l_OriginalPos = character.foot_left.transform.localPosition;

        arm_r_OriginalPos = character.hand_right.transform.localPosition;
        arm_l_OriginalPos = character.hand_left.transform.localPosition;

        body_r_OriginalPos = character.body.transform.localPosition;
		head_OriginalPos = character.head.transform.localPosition;
    }
    public void Reset()
    {
        character.foot_right.transform.localPosition = foot_r_OriginalPos;
        character.foot_left.transform.localPosition = foot_l_OriginalPos;

        character.hand_right.transform.localPosition = arm_r_OriginalPos;
        character.hand_left.transform.localPosition = arm_l_OriginalPos;

        character.body.transform.localPosition = body_r_OriginalPos;
		character.head.transform.localPosition = head_OriginalPos;
    }
    public void Move(GameObject bodyPart, string _direction, float qty)
    {
		Vector3 direction = GetDirection(bodyPart.name, _direction);

        if (direction != Vector3.zero)
        {
            //  bodyPart.transform.Translate(direction * (Time.deltaTime * qty));           
            Vector3 pos = direction * (Time.deltaTime * qty);
            bodyPart.transform.Translate(pos);
          //  bodyPart.transform.localPosition += pos;
         //   print(bodyPart.name);
            if ( bodyPart.name == "L Foot Ik" || bodyPart.name == "R Foot Ik")
            {
                AlignBodyToFoots();

               // if (character.transform.localPosition.x > character.lookAtTarget.x)
                //    character.OnCharacterMoveInX(-pos.x);
               // else
                    character.OnCharacterMoveInX(pos.x);

            }
        }
    }
    Vector3 GetDirection(string bodyPart, string direction)
    {
		if (bodyPart == "L Foot Ik" || bodyPart == "R Foot Ik" || bodyPart == "Hips bone") {
			switch (direction) {
			case "forward":
				return Vector3.right;
			case "backward":
				return -Vector3.right;
			case "up":
				return Vector3.up;
			case "down":
				return -Vector3.up;
			}
		} else {
			switch (direction) {
			case "forward":
				return Vector3.up;
			case "backward":
				return -Vector3.up;
			case "up":
				return -Vector3.right;
			case "down":
				return Vector3.right;
			}
		}
        return Vector3.zero;
    }
    float lastCenterPos = 0;
    void AlignBodyToFoots()
    {
        Vector3 centerPos = character.body.transform.localPosition;
        float newCenterPos = (character.foot_left.transform.localPosition.x + character.foot_right.transform.localPosition.x) / 2;
        centerPos.x = newCenterPos;
        if (lastCenterPos == 0 || lastCenterPos != newCenterPos)
        {
            character.body.transform.localPosition = centerPos;
            
            lastCenterPos = newCenterPos;           
        }
    }
}
