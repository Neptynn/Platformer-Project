using Platformer.Interaction;
using UnityEngine;

namespace Platformer.Utilities
{
    public static class ComponentUtilities
    {
        public static bool IsInteractable(this Component component, out IInteractable interactable)
        {
            return component.TryGetComponent(out interactable);
        }
    }
}