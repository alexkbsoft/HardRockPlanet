using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject StartWaveBtn;
    public TextMeshProUGUI LifeText;
    public TextMeshProUGUI MineTitle;


    public TextMeshProUGUI RestTitle;
    public TextMeshProUGUI FuelTitle;
    public TextMeshProUGUI TimeTitle;
    public TextMeshProUGUI DeadReason;

    public TextMeshProUGUI MobilityValue;

    public CarStorage MainStorage;

    public GameObject DeathScrpeen;
    public GameObject UpgradeScreen;


    private Mine _mine;

    private int _curSeconds = 0;

    void Update() {
        UpdateCounters(_curSeconds);
    }


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
        _curSeconds = counter;

        while (counter > 0)
        {
            UpdateCounters(counter);

            yield return new WaitForSeconds(1);

            toSpawn += 1;
            counter -= 1;

            _curSeconds = counter;

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
        FuelTitle.text = ((int)MainStorage.Fuel).ToString();
        TimeTitle.text = (secondsRemaining > 0 ? secondsRemaining : 0).ToString();
        MobilityValue.text = CarController.Instance.maxAccel.ToString();
    }

    public void OnDead(string reason) {
        CarController.Instance.IsBlocked = true;

        DeadReason.text = reason;
        DeathScrpeen.SetActive(true);
    }

    public void Retry() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void UpgradeWindow() {
        UpgradeScreen.SetActive(true);
    }
    public void CloseUpgradeWindow() {
        UpgradeScreen.SetActive(false);
    }

    public void UpgradeMobility() {

        if (MainStorage.Resources >= 200) {
            CarController.Instance.maxAccel += 50;
            MainStorage.Resources -= 200;
        }
    }
}
