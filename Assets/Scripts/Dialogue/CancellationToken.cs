using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// A cancellation token class. Used to indicate to a Unity coroutine that it needs to stop execution as soon as it gets a chance to do so.
/// The token is initially passed to the coroutine when it starts and the flag can later be set to request cancellation by calling RequestCancellation.
/// </summary>
public class CancellationToken
{
    /// <summary>
    /// Whether cancellation has been requested. This condition should be checked by the coroutine.
    /// </summary>
    public bool CancellationRequested { get; private set; } = false;

    /// <summary>
    /// Used by the calling code to request that the coroutine stop execution as soon as possible (but cleanly).
    /// </summary>
    public void RequestCancellation()
    {
        CancellationRequested = true;
    }
}
