using System.Diagnostics;
using System.IO;
using UnityEngine;

public class AdvVRTracker : MonoBehaviour {

    UnityEngine.VR.VRNode VRHead = UnityEngine.VR.VRNode.Head;
    TextWriterTraceListener listener;
    Transform cameraTransform;
    Transform cameraRigTransform;
    Transform headTransform;
    Transform spineTransform;
    Transform neckTransform;
    Animator anim;

    Vector3 rotationNeeded;
    Vector3 positionNeeded;

    Vector3 initHeadRot;
    
    bool logging = false;

    // These are all specific to the scientist model
    static float distHipsToSpine = 0.08003899f;
    static float distSpineToSpine1 = 0.18539f;
    static float distSpine1ToNeck = 0.264977f;
    static float distNeckToHead = 0.115351f;

    static float distSpineToNeck = distSpineToSpine1 + distSpine1ToNeck;
    static float distHipsToNeck = distHipsToSpine + distSpineToNeck;
    static float distHipsToHead = distHipsToNeck + distNeckToHead;

    float riftZ;
    float riftY;

    float h;
    float k;
    float p;
    float q;

    float rotSpine;
    float rotNeck;


    // This script should actually be moved to the main thing, honestly. And various
    // things should be edited to reflect that. Use anim.GetBoneTransform instead
    void Start()
    {
        anim = GetComponent<Animator>();
        // set up listener
        string filename = @"./output.txt";
        FileStream traceLog = new FileStream(filename, FileMode.OpenOrCreate);
        if (logging)
        {
            listener = new TextWriterTraceListener(traceLog);
        }
        
        headTransform = anim.GetBoneTransform(HumanBodyBones.Head);
        neckTransform = anim.GetBoneTransform(HumanBodyBones.Neck);
        spineTransform = anim.GetBoneTransform(HumanBodyBones.Spine);
        cameraRigTransform = headTransform.Find("CameraRig");
        cameraTransform = cameraRigTransform.Find("Camera");

        initHeadRot = headTransform.localRotation.eulerAngles;
        
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

            
            //camFindTransform.localPosition = -positionNeeded + (recRigPosOffset)/scale;


            //spineTransform.localRotation = Quaternion.Euler(Mathf.Atan(positionNeeded.z / distSpineToHead) / Mathf.PI * 180.0f, spineTransform.localRotation.eulerAngles.y, Mathf.Atan(positionNeeded.x / distSpineToHead) / Mathf.PI * 180.0f);

            riftZ = positionNeeded.z;
            riftY = positionNeeded.y + distHipsToHead;

            h = Mathf.Abs(distHipsToNeck - riftY);
            k = riftY - h - distHipsToSpine;
            

            p = Mathf.Sqrt(Mathf.Abs(Mathf.Pow(distSpineToNeck, 2) - Mathf.Pow(k, 2)));
            q = riftZ - p;

            UnityEngine.Debug.Log("h: " + h + ", k: " + k + ", p: " + p + ", q: " + q );
            UnityEngine.Debug.Log("vecposz: " + vecPos.z);

            rotSpine = Mathf.Acos(k / distSpineToNeck) * Mathf.Rad2Deg;
            rotNeck = Mathf.Atan2(q, h) * Mathf.Rad2Deg;

            UnityEngine.Debug.Log("spine: " + rotSpine);
            UnityEngine.Debug.Log("neck: " + rotNeck);

            //spineTransform.localRotation = Quaternion.Euler(new Vector3(/*spineTransform.localRotation.eulerAngles.x*/0, spineTransform.localRotation.eulerAngles.y, rotSpine/* - 90*/));
            //neckTransform.localRotation = Quaternion.Euler(new Vector3(rotNeck + 90, neckTransform.localRotation.eulerAngles.y, neckTransform.localRotation.eulerAngles.z));

            // Possibly subtract/add sum of above rotations to headTransform

            
            cameraRigTransform.localRotation = Quaternion.Euler(-rotationNeeded /*+ optionalRigRotation*/);
            rotationNeeded.Set(rotationNeeded.x, rotationNeeded.y /*- rotNeck - rotSpine*/, rotationNeeded.z);
            headTransform.localRotation = Quaternion.Euler(initHeadRot + rotationNeeded);
            cameraRigTransform.localPosition = -positionNeeded /*+ optionalRigPosOffset*/ + recRigPosOffset;

            //UnityEngine.Debug.Log(distSpineToHead);
            UnityEngine.Debug.Log("HeadPos: " + vecPos);

            if (logging)
            {
                listener.WriteLine("HeadRotQuat: " + quatRot + ", HeadRotAngles: " + quatRot.eulerAngles);
                listener.WriteLine("HeadPos: " + vecPos);
                listener.WriteLine("CamRotQuat: " + cameraTransform.localRotation + ", CamRotAngles: " + cameraTransform.localRotation.eulerAngles);
                listener.WriteLine("CamPos: " + cameraTransform.localPosition);
                listener.WriteLine("spineRotQuat: " + spineTransform.localRotation + ", spineRotAngles: " + spineTransform.localRotation.eulerAngles + ", distSpineToNeck: " + distSpineToNeck);
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

            if (Input.GetButtonDown("LeftShoulder"))
            {
                UnityEngine.VR.InputTracking.Recenter();
                listener.WriteLine("---Recentering InputTracking---");
            }



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
