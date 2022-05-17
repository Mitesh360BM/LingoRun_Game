using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkyboxMatChange : MonoBehaviour
{
    public List<Material> skyBoxMat;

    [Header("Fog Color")]
    public Color dayColor;
    public Color nightColor;
    [Header("Light Color")]
    public Color dayColorLight;
    public Color nightColorLight;
    public Light light;
    void Start()
    {
        int i = Random.Range(0, 0);
        if (i == 0)
        { RenderSettings.skybox = skyBoxMat[i];
          //  RenderSettings.fogColor = new Vector4(0.5960785f, 0.8745099f, 1f,1f);
            RenderSettings.fogColor = dayColor;
            light.color = dayColorLight;
            
        }
        if (i == 1)
        {
            RenderSettings.skybox = skyBoxMat[i];
            RenderSettings.fogColor = new Vector4(0.1603774f, 0.1603774f, 0.1603774f, 1f);
            RenderSettings.fogColor = nightColor;
            light.color = nightColorLight;
           
        }
      
    }

  
}
