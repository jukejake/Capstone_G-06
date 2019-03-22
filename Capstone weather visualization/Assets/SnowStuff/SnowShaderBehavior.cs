using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnowShaderBehavior : MonoBehaviour
{
    public float snowRate = 0.1f;

    private const int textureHeight = 256;
    private const int textureWidth = 256;
    private Color c_color = new Color(0, 0, 0, 0);

    private Material m_material;
    private Texture2D m_texture;
    public Texture2D m_displacementTexture;
    private bool isEnabled = false;
    // private Snow snow;


    private void Awake()
    {
        //snow = GameObject.FindGameObjectWithTag("SnowParticle").GetComponent<Snow>();
    }

    void Start()
    {
        if (GetComponent<Renderer>())
        {
            Renderer renderer = GetComponent<Renderer>();
            foreach (Material material in renderer.materials)
            {
                if (material.shader.name == "Custom/SnowShader")
                {
                    m_material = material;
                    break;
                }
                else if (material.shader.name == "Custom/SnowShaderTransparent")
                {
                    m_material = material;
                    break;
                }
                else if (material.shader.name == "Tessellation")
                {
                    m_material = material;
                    break;
                }
            }

            if (null != m_material)
            {
                m_texture = new Texture2D(textureWidth, textureHeight);
                m_displacementTexture = new Texture2D(textureWidth, textureHeight);
                Graphics.CopyTexture(m_texture, m_displacementTexture);
                for (int x = 0; x < textureWidth; ++x)
                {
                    for (int y = 0; y < textureHeight; ++y)
                    {
                        m_texture.SetPixel(x, y, c_color);
                        m_displacementTexture.SetPixel(x, y, new Color(0, 0, 0, 0));
                    }
                }
                m_displacementTexture.Apply();
                m_texture.Apply();


                m_material.SetTexture("_DrawingTex", m_texture);

                if (m_material.shader.name == "Tessellation")
                {
                    m_material.SetTexture("_DispTex", m_displacementTexture);
                }

                isEnabled = true;
            }
        }
    }

    public void PaintOn(Vector2 textureCoord, Texture2D splashTexture)
    {
        if (isEnabled)
        {

            int x = (int)(textureCoord.x * textureWidth) - (splashTexture.width / 2);
            int y = (int)(textureCoord.y * textureHeight) - (splashTexture.height / 2);
            for (int i = 0; i < splashTexture.width; ++i)
            {
                for (int j = 0; j < splashTexture.height; ++j)
                {
                    int newX = x + i;
                    int newY = y + j;
                    Color existingColor = m_texture.GetPixel(newX, newY);
                    Color targetColor = splashTexture.GetPixel(i, j);
                    float alpha = targetColor.a;
                    if (alpha > 0)
                    {
                        Color result = Color.Lerp(existingColor, targetColor, alpha);   // resulting color is an addition of splash texture to the texture based on alpha

                        result.a = 0.2f + existingColor.a;                           // but resulting alpha is a sum of alphas (adding transparent color should not make base color more transparent)
                        m_displacementTexture.SetPixel(x, y, new Color(.2f, .2f, .2f, 1f));
                        m_texture.SetPixel(newX, newY, result);
                    }
                }
            }
            m_texture.Apply();
            m_displacementTexture.Apply();
        }
    }
}
