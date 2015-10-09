using System.Diagnostics;
using System.IO;
using UnityEngine;

public class AdvVRTracker : MonoBehaviour {

    UnityEngine.VR.VRNode VRHead = UnityEngine.VR.VRNode.Head;
    TextWriterTraceListener listener;
    Transform cameraTransform;
    Transform cameraRigTransform;
    Transform headTransform;
    Transform legTransform;
    Transform spineTransform;
    //Transform camFindTransform;

    Vector3 rotationNeeded;
    Vector3 positionNeeded;

    Vector3 initHeadRot;

    float distSpineToHead;

    bool logging = false;


    // This script should actually be moved to the main thing, honestly. And various
    // things should be edited to reflect that. Use anim.GetBoneTransform instead
    void Start()
    {
        // set up listener
        string filename = @"./output.txt";
        FileStream traceLog = new FileStream(filename, FileMode.OpenOrCreate);
        if (logging)
        {
            listener = new TextWriterTraceListener(traceLog);
        }
        headTransform = transform.Find("ScientistSkeleton/Hips/Spine/Spine1/Neck/Head");
        cameraRigTransform = headTransform.Find("CameraRig");
        cameraTransform = cameraRigTransform.Find("Camera");   // Gotta love long identifiers
        //camFindTransform = headTransform.Find("CameraFinder");
        legTransform = transform.Find("ScientistSkeleton/Hips/LeftUpLeg");
        spineTransform = transform.Find("ScientistSkeleton/Hips/Spine");

        initHeadRot = headTransform.localRotation.eulerAngles;

        distSpineToHead = headTransform.position.y - (legTransform.position.y + 0.1f);
    }

    // Update is called once per frame
    void LateUpdate () {
	    if(UnityEngine.VR.VRDevice.isPresent)
        {

            Quaternion quatRot = UnityEngine.VR.InputTracking.GetLocalRotation(VRHead);
            Vector3 vecPos = UnityEngine.VR.InputTracking.GetLocalPosition(VRHead);

            // output to listener 

            //float scale = 0.1f;
            Vector3 optionalRigPosOffset = new Vector3(0, 0, 1);
            Vector3 optionalRigRotation = new Vector3(0, 180, 0);
            Vector3 recRigPosOffset = new Vector3(0, 0.055f, 0.1f);

            rotationNeeded = quatRot.eulerAngles;
            positionNeeded = vecPos;

            cameraRigTransform.localRotation = Quaternion.Euler(-rotationNeeded /*+ optionalRigRotation*/);
            headTransform.localRotation = Quaternion.Euler(initHeadRot + rotationNeeded);
            cameraRigTransform.localPosition = -positionNeeded /*+ optionalRigPosOffset*/ + recRigPosOffset;
            //camFindTransform.localPosition = -positionNeeded + (recRigPosOffset)/scale;


            spineTransform.localRotation = Quaternion.Euler(Mathf.Atan(positionNeeded.z / distSpineToHead) / Mathf.PI * 180.0f, spineTransform.localRotation.eulerAngles.y, Mathf.Atan(positionNeeded.x / distSpineToHead) / Mathf.PI * 180.0f);

            //UnityEngine.Debug.Log(distSpineToHead);

            if (logging)
            {
                listener.WriteLine("HeadRotQuat: " + quatRot + ", HeadRotAngles: " + quatRot.eulerAngles);
                listener.WriteLine("HeadPos: " + vecPos);
                listener.WriteLine("CamRotQuat: " + cameraTransform.localRotation + ", CamRotAngles: " + cameraTransform.localRotation.eulerAngles);
                listener.WriteLine("CamPos: " + cameraTransform.localPosition);
                listener.WriteLine("spineRotQuat: " + spineTransform.localRotation + ", spineRotAngles: " + spineTransform.localRotation.eulerAngles + ", distSpineToHead: " + distSpineToHead);
            }
            /*if (Input.GetButtonDown("LeftShoulder"))
            {
                listener.WriteLine("----Looking Directly Left---");
            }*/

            if (Input.GetButtonDown("LeftShoulder") && logging)
            {
                listener.WriteLine("----Leaning forward---");
            }

            //UnityEngine.VR.InputTracking.Recenter();

            /*if (Input.GetButtonDown("LeftShoulder"))
            {
                UnityEngine.VR.InputTracking.Recenter();
                listener.WriteLine("---Recentering InputTracking---");
            }*/



        }
    }

    void OnApplicationQuit()
    {
        if (UnityEngine.VR.VRDevice.isPresent && logging)
        {
            // flush any open output before termination
            //   maybe in an override of Form.OnClosed 
            listener.Flush();
        }
    }
}
