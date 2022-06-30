# Unity3D based RGBD mapping using ROS data stream

## Getting started
1. Clone the repository   
``git clone github.com/tomhruby1/UnityRGBDmapping --recurse-submodules ``
2. install ROS - tested on Ubuntu 20.04 + ROS Noetic
3. install rosbridge package http://wiki.ros.org/rosbridge_suite 
4. Download TUM RGBD dataset .bag file for experiments - https://vision.in.tum.de/data/datasets/rgbd-dataset/download 
5. Create a Unity scene and add necessary components to hierarchy: 
   1. ROSInitializer, CloudBuffer, VoxelMap, Integrator
   2. VoxelMapShading, and pointcloud object for visualization (if setup in CloudBuffer) requires MeshFilter and MeshRenderer components
## Run it 
1. launch ROS core:
```roscore```
2. launch ROS bridge:  
```roslaunch rosbridge_server rosbridge_websocket.launch``` 
3. run the downloaded dataset with provided .launch file:  
``roslaunch tum_rgbd_to_pcd.launch [bag_file_path]``
4. run the unity scene