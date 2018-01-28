using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSpriteManager : MonoBehaviour {

    public static CharacterSpriteManager Instance;

    private void Awake() {
        Instance = this;
    }

    public List<RuntimeAnimatorController> characterAnimators;
}
