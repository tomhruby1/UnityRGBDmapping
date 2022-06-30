using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using ROSBridgeLib;
using ROSBridgeLib.geometry_msgs;
using SimpleJSON;
using ROSBridgeLib.sensor_msgs;
using Unity.VisualScripting;

/*
 * ROS PointCloud2 subscriber
 *
 * 
 */
public class TFSubscriber : ROSBridgeSubscriber
{
    //global pointcloud msg buffer in scene, where stored //TODO: how to acces without searching by name in CallBack
    static GameObject cloudBuffer; 
    
    // These two are important
    public new static string GetMessageTopic() {
        //Topic name is up to the user. It should return the full path to the topic. 
        //For eg, "/turtle1/cmd_vel" instead of "/cmd_vel".
        return "/tf";
    }

    public new static string GetMessageType() {
        //Same as the definition present in ROS documentation:
        return "tf/tfMessage";
    }

    // Important function (I think.. Converts json to PoseMsg)
    public new static ROSBridgeMsg ParseMessage(JSONNode msg) {
        return new TfMessageMsg(msg);
    }
    
    // This function should fire on each received ros message
    public new static void CallBack(TfMessageMsg msg)
    {
        //get kinect - world transform
        //TODO: maybe incorporate also the kinect to depth cam transforms...
        foreach(var trans in msg.GetTransforms())
            if (trans.GetChildFrameId() == "/kinect")
            {
                TransformMsg t = trans.GetTransform();
                Vector3 position = new Vector3((float)t._translation.GetX(), (float)t._translation.GetY(), (float)t._translation.GetZ());
                Quaternion rotation = new Quaternion(t._rotation.GetX(), t._rotation.GetY(), t._rotation.GetZ(),
                    t._rotation.GetW());
                GameObject.FindWithTag("PCDbuffer").GetComponent<CloudBuffer>().SetTransform(position, rotation);
            }
    }
}