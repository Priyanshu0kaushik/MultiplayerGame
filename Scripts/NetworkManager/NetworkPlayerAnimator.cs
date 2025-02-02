using UnityEngine;
using Unity.Netcode.Components;

public class NetworkPlayerAnimator : NetworkAnimator
{
    protected override bool OnIsServerAuthoritative()
    {
        return false;
    }
}
