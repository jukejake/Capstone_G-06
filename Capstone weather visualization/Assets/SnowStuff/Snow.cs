using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Snow : MonoBehaviour
{
    public ParticleSystem part;
    public List<ParticleCollisionEvent> collisionEvents;
    public Texture2D splashTexture;

    private RaycastHit hit;

    void Awake()
    {
        part = this.GetComponent<ParticleSystem>();
        collisionEvents = new List<ParticleCollisionEvent>();
    }


    private void OnParticleCollision(GameObject other)
    {
        int numCollisionEvents = part.GetCollisionEvents(other, collisionEvents);
        //if (numCollisionEvents <= 0) { return; }

        SnowShaderBehavior script;
        if (other.GetComponent<SnowShaderBehavior>()) {
            script = other.GetComponent<SnowShaderBehavior>();

            int i = 0;

            while (i < numCollisionEvents)
            {
                if (script != null)
                {
                    if (Physics.Raycast(collisionEvents[i].intersection, Vector3.right, out hit))
                    {
                        Debug.Log(hit.transform.name);
                        //MyShaderBehavior script = hit.collider.gameObject.GetComponent<MyShaderBehavior>();
                        script.PaintOn(hit.textureCoord, splashTexture);
                    }

                }
                i++;
            }
        }
    }
}

