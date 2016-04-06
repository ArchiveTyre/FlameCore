using UnityEngine;
using System.Collections;

public class Core_SelfDestruct : MonoBehaviour {

    [Tooltip("How long untill self destruct in seconds, If -1 then will never explode")]
    public float fuseTime = 3f;

    [Tooltip("How long has this object been alive in seconds.")]
    [ReadOnly] [SerializeField] private float lifeTime = 0f;

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
        Destroy(gameObject);
    }
}
