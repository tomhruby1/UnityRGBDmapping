using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using ROSBridgeLib;
using SimpleJSON;
using ROSBridgeLib.sensor_msgs;
/*
 * ROS PointCloud2 subscriber
 *
 * 
 */
public class PointCloudSubscriber : ROSBridgeSubscriber
{
    //global pointcloud msg buffer in scene, where stored //TODO: how to acces without searching by name in CallBack
    static GameObject cloudBuffer; 
    
    // These two are important
    public new static string GetMessageTopic() {
        //Topic name is up to the user. It should return the full path to the topic. 
        //For eg, "/turtle1/cmd_vel" instead of "/cmd_vel".
        return "/camera/depth/points";
    }

    public new static string GetMessageType() {
        //Same as the definition present in ROS documentation:
        return "sensor_msgs/PointCloud2";
    }

    // Important function (I think.. Converts json to PoseMsg)
    public new static ROSBridgeMsg ParseMessage(JSONNode msg) {
        return new PointCloud2Msg(msg);
    }

    // This function should fire on each received ros message
    public new static void CallBack(PointCloud2Msg msg) {
        //when msg constructed, the point cloud should be decoded into PointCloud object
        //Debug.Log("Pointcloud received");
        GameObject.FindWithTag("PCDbuffer").GetComponent<CloudBuffer>().StoreCloud(msg.GetCloud());
    }
}