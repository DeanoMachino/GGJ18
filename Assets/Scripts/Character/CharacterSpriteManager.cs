using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSpriteManager : MonoBehaviour {

    public static CharacterSpriteManager Instance;

    private void Awake() {
        Instance = this;
    }

    public List<CharacterAnimation> characterAnimations
        ;
}

[System.Serializable]
public class CharacterAnimation {
    public Sprite idleSprite;
    public RuntimeAnimatorController animationController;
}
