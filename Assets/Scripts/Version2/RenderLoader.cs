using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RenderLoader : MonoBehaviour
{
    [Header("Video Renderer")]
    public Material skyMat;  // 天空盒材质
    public RenderTexture skybox;  // video render texture
    public RenderTexture black;  // 黑色渲染纹理
 // 图片纹理

    private void Start()
    {
        skyMat.SetTexture("_MainTex", black);
    }

}
