// Upgrade NOTE: replaced '_Object2World' with 'unity_ObjectToWorld'
// Upgrade NOTE: replaced '_World2Object' with 'unity_WorldToObject'

Shader "Custom/CurvedWorld_Alpha" {
        Properties {
            // Diffuse texture
            _Color ("Main Color", Color) = (1,1,1,1)
            _MainTex ("Base (RGB)", 2D) = "white" {}
            // Degree of curvature
            _Curvature ("Curvature", Float) = 0.001
            _QOffset ("Offset", Vector) = (0,0,0,0)
		
        }
    
        SubShader
        {
           // Cull off
             Tags {"RenderType"="Transparent Cutout"}
            
             Blend SrcAlpha OneMinusSrcAlpha
              
            
             LOD 600
             
                 CGPROGRAM
                    // Surface shader function is called surf, and vertex preprocessor function is called vert
                    // addshadow used to add shadow collector and caster passes following vertex modification
                    #pragma surface surf Lambert vertex:vert addshadow alpha
                    //#pragma surface surf Lambert alpha
                    #pragma surface surf Standard fullforwardshadows alpha:fade
                     #include "UnityCG.cginc"
                    // Access the shaderlab properties
                    uniform sampler2D _MainTex;
                    uniform float _Curvature;
                    float4 _QOffset;
	   
                    fixed4 _Color;
                    // Basic input structure to the shader function
                    // requires only a single set of UV texture mapping coordinates
                    struct Input {
                        float2 uv_MainTex;
                    };
 
                    // This is where the curvature is applied
                    void vert( inout appdata_full v)
                    {
                        // Transform the vertex coordinates from model space into world space
                        float4 vv = mul( unity_ObjectToWorld, v.vertex );
            
                        // Now adjust the coordinates to be relative to the camera position
                        vv.xyz -= _WorldSpaceCameraPos.xyz;
 
                        // Reduce the y coordinate (i.e. lower the "height") of each vertex based
                        // on the square of the distance from the camera in the z axis, multiplied
                        // by the chosen curvature factor
                        vv = float4( ((vv.z * vv.z) + (vv.x * vv.x)) * - _Curvature * _QOffset.x, ((vv.z * vv.z) + (vv.y * vv.y)) * - _Curvature * _QOffset.y, ((vv.z * vv.z) + (vv.z * vv.z)) * - _Curvature * _QOffset.z, 0.0f );
    
 
                        // Now apply the offset back to the vertices in model space
                        v.vertex += mul(unity_WorldToObject, vv);
                    }
 
                    // This is just a default surface shader
                    void surf (Input IN, inout SurfaceOutput o) {
                        fixed4 c = tex2D (_MainTex, IN.uv_MainTex) * _Color ;
                        o.Albedo = c.rgb;
                        o.Alpha = c.a;
                        o.Emission = +c.rgba;                     
                    }
                 ENDCG          
             
        }
           //FallBack "Unlit/Transparent Cutout"
}