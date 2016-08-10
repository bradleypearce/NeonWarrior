using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
public class ScreenDistortion : MonoBehaviour {

    public Material m_mat;

    void OnRenderImage(RenderTexture _source, RenderTexture _destination)
    {
        Graphics.Blit(_source, _destination, m_mat);
    }
}
