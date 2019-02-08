using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Snow : MonoBehaviour
{
    public ParticleSystem part;
    public List<ParticleCollisionEvent> collisionEvents;
    public Texture2D splashTexture;

    private RaycastHit hit;

    void Start()
    {
        part = GetComponent<ParticleSystem>();
        collisionEvents = new List<ParticleCollisionEvent>();
    }


    private void OnParticleCollision(GameObject other)
    {
        int numCollisionEvents = part.GetCollisionEvents(other, collisionEvents);

        SnowShaderBehavior script = other.GetComponent<SnowShaderBehavior>();
        //if (null != script)
        //    script.PaintOn(hit.textureCoord, splashTexture);

        Rigidbody rb = other.GetComponent<Rigidbody>();
        int i = 0;

        while (i < numCollisionEvents)
        {
            if (null != script)
            {

                if (Physics.Raycast(collisionEvents[i].intersection, Vector3.right, out hit))
                {
                    Debug.Log(hit.transform.name);
                    //MyShaderBehavior script = hit.collider.gameObject.GetComponent<MyShaderBehavior>();
                    if (null != script)
                        script.PaintOn(hit.textureCoord, splashTexture);
                }

            }
        }
    }
}

