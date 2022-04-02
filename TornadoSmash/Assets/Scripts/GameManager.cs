using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public GameObject groundShadow;
    public List<Transform> cubeList = new List<Transform>();
    [SerializeField] private Image fillImage;
    [SerializeField] private float levelEndSweepTime; //1.5f
    [SerializeField] private float levelEndSweepCollRadius; //12
    private float startCubeListCount;
    [HideInInspector] public bool isGameFinished = false;
    [HideInInspector] public bool isLevelCleared = false; 
    
    private void Awake()
    {
        instance = this;
    } 

    void Start()
    {
        startCubeListCount = cubeList.Count;
    } 

    void Update()
    {
        HandleWinning();
        UpdateProgress();
    } 

    void HandleWinning()
    {
        if (!isGameFinished && isLevelCleared)
        {
            isGameFinished = true;
            TornadoController.instance.TriggerLevelFinished();
            groundShadow.SetActive(false);
            SweepLevelFloorCubes();
        }
    } 

    public void CheckGameWinning()
    {
        if (cubeList.Count <= 0 && !isLevelCleared)
        {
            isLevelCleared = true;
        }
    } 

    void SweepLevelFloorCubes()
    {
        DOTween.To(() => TornadoController.instance.centerCollider.radius, x => TornadoController.instance.centerCollider.radius = x, levelEndSweepCollRadius, levelEndSweepTime);
    } 

    private void UpdateProgress()
    {
        float value = Mathf.InverseLerp(startCubeListCount, 0, cubeList.Count);
        fillImage.fillAmount = value;
    }
}
