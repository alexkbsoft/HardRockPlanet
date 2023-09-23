using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public GameObject StartWaveBtn;
    public TextMeshProUGUI LifeText;
    private Mine _mine;


    void Start()
    {
        
    }

    public void OnPlayerDamage(float currentLife) {
        LifeText.text = ((int)currentLife).ToString();
    }

    public void OnEnterMine(Mine mine) {
        if (_mine != mine) {
            _mine = mine;
            StartWaveBtn.SetActive(true);
        }
    }

    public void OnExitMine(Mine mine) {
        if (_mine = mine) {
            _mine = null;
        }
    }

    public void StartWave() {
        foreach(Spawner s in _mine.Spawners) {
            s.Spawn();
            s.Count += 1;
            s.Speed += 1;
        }
    }
}
