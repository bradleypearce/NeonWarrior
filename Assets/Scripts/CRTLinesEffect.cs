using UnityEngine;
using System.Collections;

/// CLASS CRTLINESEFFECT
/// 
/// This class simply blits the result of our CRTLines shader to the screen.
/// We use a material as the process of adding the material to the script is 
/// very simple. 
[ExecuteInEditMode]
public class CRTLinesEffect : MonoBehaviour 
{
	public Material m_mat;
    
    /// ONRENDERIMAGE
    /// 
    /// This function simply processes the current render texture (before effect) 
    /// into our shader and outputs it as the destination texture.
	void OnRenderImage(RenderTexture _source, RenderTexture _destination)
	{
		Graphics.Blit (_source, _destination, m_mat); 
	}
}
