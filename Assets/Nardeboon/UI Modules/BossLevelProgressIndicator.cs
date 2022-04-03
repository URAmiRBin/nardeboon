using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossLevelProgressIndicator : LevelProgressIndicator {
    [SerializeField] Sprite _bossSpritePassed, _bossSpriteNotPassed;

    public override void SetLevel(int level) {
        base.SetLevel(level);
        progressImages[cap - 1].sprite = level != 0 && level % cap == 0 ? _bossSpritePassed : _bossSpriteNotPassed;
    }
}
