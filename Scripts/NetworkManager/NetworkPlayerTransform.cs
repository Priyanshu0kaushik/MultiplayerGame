using UnityEngine;
using Unity.Netcode.Components;

public class NetworkPlayerTransform : NetworkTransform
{
    protected override bool OnIsServerAuthoritative()
    {
        return false;
    }
}
