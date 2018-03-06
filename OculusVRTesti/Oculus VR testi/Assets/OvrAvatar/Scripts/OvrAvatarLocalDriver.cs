using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;
using Oculus.Avatar;

public class OVRAvatarLocalDriver : OVRAvatarDriver {

    private const float mobileBaseHeadHeight = 1.7f;

    float voiceAmplitude = 0.0f;
    ControllerPose GetControllerPose(OVRInput.Controller controller)
    {
        OVRAvatarButton buttons = 0;
        if (OVRInput.Get(OVRInput.Button.One, controller)) buttons |= OVRAvatarButton.One;
        if (OVRInput.Get(OVRInput.Button.Two, controller)) buttons |= OVRAvatarButton.Two;
        if (OVRInput.Get(OVRInput.Button.Start, controller)) buttons |= OVRAvatarButton.Three;
        if (OVRInput.Get(OVRInput.Button.PrimaryThumbstick, controller)) buttons |= OVRAvatarButton.Joystick;

        OVRAvatarTouch touches = 0;
        if (OVRInput.Get(OVRInput.Touch.One, controller)) touches |= OVRAvatarTouch.One;
        if (OVRInput.Get(OVRInput.Touch.Two, controller)) touches |= OVRAvatarTouch.Two;
        if (OVRInput.Get(OVRInput.Touch.PrimaryThumbstick, controller)) touches |= OVRAvatarTouch.Joystick;
        if (OVRInput.Get(OVRInput.Touch.PrimaryThumbRest, controller)) touches |= OVRAvatarTouch.ThumbRest;
        if (OVRInput.Get(OVRInput.Touch.PrimaryIndexTrigger, controller)) touches |= OVRAvatarTouch.Index;
        if (!OVRInput.Get(OVRInput.NearTouch.PrimaryIndexTrigger, controller)) touches |= OVRAvatarTouch.Pointing;
        if (!OVRInput.Get(OVRInput.NearTouch.PrimaryThumbButtons, controller)) touches |= OVRAvatarTouch.ThumbUp;

        return new ControllerPose
        {
            buttons = buttons,
            touches = touches,
            joystickPosition = OVRInput.Get(OVRInput.Axis2D.PrimaryThumbstick, controller),
            indexTrigger = OVRInput.Get(OVRInput.Axis1D.PrimaryIndexTrigger, controller),
            handTrigger = OVRInput.Get(OVRInput.Axis1D.PrimaryHandTrigger, controller),
            isActive = (OVRInput.GetActiveController() & controller) != 0,
        };
    }

    private PoseFrame GetCurrentPose()
    {
        Vector3 headPos = UnityEngine.XR.InputTracking.GetLocalPosition(UnityEngine.XR.XRNode.CenterEye);
#if UNITY_ANDROID && !UNITY_EDITOR
        headPos.y += mobileBaseHeadHeight;
#endif

        return new PoseFrame
        {
            voiceAmplitude = voiceAmplitude,
            headPosition = headPos,
            headRotation = UnityEngine.XR.InputTracking.GetLocalRotation(UnityEngine.XR.XRNode.CenterEye),
            handLeftPosition = OVRInput.GetLocalControllerPosition(OVRInput.Controller.LTouch),
            handLeftRotation = OVRInput.GetLocalControllerRotation(OVRInput.Controller.LTouch),
            handRightPosition = OVRInput.GetLocalControllerPosition(OVRInput.Controller.RTouch),
            handRightRotation = OVRInput.GetLocalControllerRotation(OVRInput.Controller.RTouch),
            controllerLeftPose = GetControllerPose(OVRInput.Controller.LTouch),
            controllerRightPose = GetControllerPose(OVRInput.Controller.RTouch),
        };
    }

    public override void UpdateTransforms(IntPtr sdkAvatar)
    {
        if (sdkAvatar != IntPtr.Zero)
        {
            PoseFrame pose = GetCurrentPose();

            OVRAvatarTransform bodyTransform = OVRAvatar.CreateOVRAvatarTransform(pose.headPosition, pose.headRotation);
            OVRAvatarHandInputState inputStateLeft = OVRAvatar.CreateInputState(OVRAvatar.CreateOVRAvatarTransform(pose.handLeftPosition, pose.handLeftRotation), pose.controllerLeftPose);
            OVRAvatarHandInputState inputStateRight = OVRAvatar.CreateInputState(OVRAvatar.CreateOVRAvatarTransform(pose.handRightPosition, pose.handRightRotation), pose.controllerRightPose);

            CAPI.OVRAvatarPose_UpdateBody(sdkAvatar, bodyTransform);
            CAPI.OVRAvatarPose_UpdateHands(sdkAvatar, inputStateLeft, inputStateRight);
        }
    }
}
