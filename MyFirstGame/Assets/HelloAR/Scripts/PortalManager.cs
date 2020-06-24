using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class PortalManager : MonoBehaviour
{
    public GameObject mainCamera;
    public GameObject Sponza;
    private Material[] SponzaMaterials;
    private Material PortalPlaneMaterial;
    void Start()
    {
        SponzaMaterials = Sponza.GetComponent<Renderer>().sharedMaterials;    
        PortalPlaneMaterial = GetComponent<Renderer>().sharedMaterial;    
    }


    void OnTriggerStay(Collider other) 
    {
       Vector3 cameraPositionInPortalSpace = transform.InverseTransformPoint(mainCamera.transform.position);

       if(cameraPositionInPortalSpace.y <= 0.0f)
       {
           //Inside Stencil
           for(int i = 0; i < SponzaMaterials.Length ; ++i)
           {
               SponzaMaterials[i].SetInt("_StencilComp", (int)CompareFunction.NotEqual);
           }
           PortalPlaneMaterial.SetInt("_CullMode", (int)CullMode.Front);
       }
       else if(cameraPositionInPortalSpace.y < 0.5f)
       {
           //Disable Stencil
           for(int i = 0; i < SponzaMaterials.Length; ++i)
           {
               SponzaMaterials[i].SetInt("_StencilComp", (int)CompareFunction.Always);
           }
           PortalPlaneMaterial.SetInt("_CullMode", (int)CullMode.Off);
       }
       else
       {
           //Enable Stencil
           for(int i = 0; i < SponzaMaterials.Length; ++i)
           {
               SponzaMaterials[i].SetInt("_StencilComp", (int)CompareFunction.Equal);
           }      
           PortalPlaneMaterial.SetInt("_CullMode", (int)CullMode.Back); 
       }
        
    }
}
