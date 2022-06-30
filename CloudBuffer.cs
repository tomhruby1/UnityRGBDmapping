using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Design;
//using System.Drawing;
using PointCloud;
using UnityEngine;


/// <summary>
/// TODO maybe do global var singleton style?
/// </summary>
public class CloudBuffer : MonoBehaviour
{
   private PointCloud<PointXYZRGB> cloudBuffer;

   private Vector3 camPosition;
   private Quaternion camRotation;

   //rendering vars
   //if renderPointcloud, has to have a mesh and mesh renderer components
   public GameObject renderedObject; 
   private Mesh mesh;
   
   //sensor object - should be parent of renderObject
   public GameObject sensor;
   public PointCloudIntegrator integrator;

   //preferences
   public bool renderPointcloud = false;  //if enabled the pcd is not passed to integrator, but only visualized

   public void StoreCloud(PointCloud<PointXYZRGB> cloud)
   {
      cloudBuffer = cloud;
      if(renderPointcloud)
         RenderCloud();
      else
         integrator.Integrate(cloud, camPosition);
   }

   public void SetTransform(Vector3 position, Quaternion rotation)
   {
      camPosition = position;
      camRotation = rotation;
      
      sensor.transform.localPosition = camPosition;
      sensor.transform.localRotation = camRotation;
   }

   public PointCloud<PointXYZRGB> GetCloud()
   {
      return cloudBuffer;
   }
   
   //get reference to mesh for visualization if enabled
   void Start()
   {
      if (renderPointcloud && renderedObject != null)
      {
         mesh = new Mesh(); 
         renderedObject.GetComponent<MeshFilter>().mesh = mesh; 
      }
   }
   
   (Vector3, Color) PCDpoint2Vector3(PointXYZRGB point)
   {
      Vector3 position = new Vector3(point.X, point.Y, point.Z); 
      Color color = new Color(point.R/255f, point.G/255f, point.B/255f, 1f);
      
      return (position, color);
   }
   
   //Creates renderable pcd vertex based gameobject mesh
   private void RenderCloud()
   {
      PointCloud<PointXYZRGB> pcd = cloudBuffer;

      Vector3[] vertices = new Vector3[pcd.Size];
      Color[] colors = new Color[pcd.Size];
      int[] idxs = new int[pcd.Size];
      for (int i = 0; i < pcd.Size; i++)
      {
         (vertices[i], colors[i]) = PCDpoint2Vector3(pcd.At(i));
         idxs[i] = i;
      }
      //Debug.Log("vertex-0: " + vertices[0]);
      mesh.vertices = vertices;
      mesh.colors = colors;
      mesh.SetIndices(idxs, MeshTopology.Points, 0);
   }
}
