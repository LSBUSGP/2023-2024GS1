using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Code;

public class Testing : MonoBehaviour {

    [SerializeField] private UI_StatsRadarChart uiStatsRadarChart;
    [SerializeField] private UI_TestStatsRadarChart uiTestStatsRadarChart;

    private void Start() {
        Stats stats = new Stats(10, 2, 5, 10, 15);

        uiStatsRadarChart.SetStats(stats);
        uiTestStatsRadarChart.SetStats(stats);

    }

}
