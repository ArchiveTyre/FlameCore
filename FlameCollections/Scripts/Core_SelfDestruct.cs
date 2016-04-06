using UnityEngine;
using System.Collections;

public class Core_SelfDestruct : MonoBehaviour {

    [Tooltip("How long untill self destruct in seconds, If -1 then will never explode")]
    public float fuseTime = 3f;

    [Tooltip("How long has this object been alive in seconds.")]
    [ShowOnly] [SerializeField] private float lifeTime = 0f;

    // Should it be Fixed?
    void Update()
    {
        lifeTime += Time.deltaTime;
        if (!(fuseTime < 0))
        {
            if (lifeTime >= fuseTime)
            {
                Destruct();
            }
        }
    }

    void Destruct()
    {

        // Are there any objects that want to be notified?
        ISelfDestructor[] selfDestructors;
        selfDestructors = gameObject.GetComponents<ISelfDestructor>();
        foreach (ISelfDestructor dest in selfDestructors)
        {
            // There is one!
            dest.OnSelfDestruct();
        }

        // Kill the objects.
        Destroy(gameObject);
    }
}
