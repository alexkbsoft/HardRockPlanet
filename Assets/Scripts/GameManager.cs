using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public GameObject StartWaveBtn;
    public TextMeshProUGUI LifeText;
    public TextMeshProUGUI MineTitle;


    public TextMeshProUGUI RestTitle;
    public TextMeshProUGUI FuelTitle;
    public TextMeshProUGUI TimeTitle;

    public CarStorage MainStorage;

    private Mine _mine;


    void Start()
    {
        MainStorage.Reset();
        UpdateCounters(0);
    }

    public void OnPlayerDamage(float currentLife)
    {
        LifeText.text = ((int)currentLife).ToString();
    }

    public void OnEnterMine(Mine mine)
    {
        if (_mine != mine)
        {
            _mine = mine;
            StartWaveBtn.SetActive(true);

            MineTitle.text = mine.MineName;
            MineTitle.gameObject.SetActive(true);
        }
    }

    public void OnExitMine(Mine mine)
    {
        if (_mine = mine)
        {
            _mine = null;
            StartWaveBtn.SetActive(false);
            MineTitle.gameObject.SetActive(false);
        }
    }

    public void StartWave()
    {
        var graphToScan = AstarPath.active.data.gridGraph;
        graphToScan.center = _mine.transform.position;

        AstarPath.active.Scan(graphToScan);

        StartCoroutine(WaveFlow());
    }

    private IEnumerator WaveFlow()
    {
        var counter = _mine.SecondsToDefend;
        var toSpawn = 0;
        CarController.Instance.IsBlocked = true;
        Spawn();

        while (counter > 0)
        {
            UpdateCounters(counter);

            yield return new WaitForSeconds(1);

            toSpawn += 1;
            counter -= 1;

            if (counter % 2 == 0)
            {
                MainStorage.Resources += _mine.Income;
            }

            if (toSpawn >= _mine.WaveMomentSeconds)
            {
                toSpawn = 0;

                Spawn();
            }
        }

        UpdateCounters(counter);

        CarController.Instance.IsBlocked = false;
    }

    private void Spawn()
    {
        foreach (Spawner s in _mine.Spawners)
        {
            s.Spawn();
        }

    }

    private void UpdateCounters(int secondsRemaining)
    {
        RestTitle.text = MainStorage.Resources.ToString();
        FuelTitle.text = MainStorage.Fuel.ToString();
        TimeTitle.text = (secondsRemaining > 0 ? secondsRemaining : 0).ToString();
    }
}
