using UnityEngine;

namespace Runtime.Interaction {
    public class Interaction {
        public InteractionManager.InteractionType Type => _type;
        public InteractableObject InteractableObject => _interactableObject;

        private InteractionManager.InteractionType _type;
        private InteractableObject _interactableObject;

        public Interaction(InteractionManager.InteractionType t, InteractableObject obj) {
            _type = t;
            _interactableObject = obj;
        }
    }
}