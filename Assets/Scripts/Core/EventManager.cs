using System;
using System.Collections.Generic;
using UnityEngine;

public class EventManager {
    /* Event-invoke function pairs are declared here.
     * The wrapper functions makes it so that we can invoke an event from any
     * script. All the events being in this manager also makes it so that any
     * suscribers only need to know about this event manager */

    // Triggered when an item is grabbed
    public static event Action waveStarted;
    public static void InvokeWaveStarted() {
        waveStarted?.Invoke();
    }
}

