using UnityEngine;
using System.Collections;

// This is an interface for all objects that want to be notified when they are self destructed.
interface ISelfDestructor
{
    void OnSelfDestruct();
}
