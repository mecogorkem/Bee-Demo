using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

public class GameManager : MonoBehaviour
{
    private readonly LevelData _level1 = new LevelData {
        waspSpeed = 6,
        beeCount = 12,
        endMob = new List<MobType>{ MobType.Bee },
        flowerCount = 6,
        mobCount = 6
    };
    private LevelData Level2 = new LevelData {
        waspSpeed = 6,
        beeCount = 10,
        endMob = new List<MobType>{ MobType.Bee,MobType.Bee},
        flowerCount = 10,
        mobCount = 8
    };
    private LevelData Level3 = new LevelData {
        waspSpeed = 6,
        beeCount = 12,
        endMob = new List<MobType>{ MobType.Bee,MobType.Bee,MobType.Bee},
        flowerCount = 6,
        mobCount = 10
    };
    private LevelData Level4 = new LevelData {
        waspSpeed = 6,
        beeCount = 6,
        endMob = new List<MobType>{ MobType.Bee,MobType.Bee,MobType.Bee,MobType.Bee},
        flowerCount = 6,
        mobCount = 12
    };
    private LevelData Level5 = new LevelData {
        waspSpeed = 6,
        beeCount = 10,
        endMob = new List<MobType>{ MobType.Bumble,MobType.Bumble,MobType.Bumble},
        flowerCount = 6,
        mobCount = 14
    };
    private LevelData Level6 = new LevelData {
        waspSpeed = 6,
        beeCount = 10,
        endMob = new List<MobType>{ MobType.Bumble,MobType.Bumble,MobType.Bumble},
        flowerCount = 6,
        mobCount = 15
    };
    private LevelData Level7 = new LevelData {
        waspSpeed = 6,
        beeCount = 10,
        endMob = new List<MobType>{ MobType.Bumble,MobType.Bumble,MobType.Bumble},
        flowerCount = 6,
        mobCount = 16
    };
    private LevelData Level8 = new LevelData {
        waspSpeed = 8,
        beeCount = 12,
        endMob = new List<MobType>{ MobType.Bumble,MobType.Bumble,MobType.Bumble,MobType.Bumble},
        flowerCount = 6,
        mobCount = 16
    };
    private LevelData Level9 = new LevelData {
        waspSpeed = 8,
        beeCount = 12,
        endMob = new List<MobType>{ MobType.Bumble,MobType.Bumble,MobType.Bumble,MobType.Bumble},
        flowerCount = 6,
        mobCount = 16
    };
    private LevelData Level10 = new LevelData {
        waspSpeed = 10,
        beeCount = 16,
        endMob = new List<MobType>{ MobType.Sting,MobType.Sting,MobType.Sting},
        flowerCount = 6,
        mobCount = 16
    };
    
    private List<LevelData> _levelDatas;
    public static GameManager Instance;
    [SerializeField] private TextMeshPro collectedBeeText;
    [SerializeField] private TextMeshProUGUI collectedFlowerTmp;
    [SerializeField] private MapGenerator mapGenerator;
    [SerializeField] private Transform finalMobList;
    [SerializeField] private GameObject mainWasp;
    [SerializeField] private GameObject winScreen;
    [SerializeField] private GameObject loseScreen;
    [SerializeField] private TextMeshProUGUI collectedFlowerTmp2;
    [SerializeField] private TextMeshProUGUI bonusFlowerText;

    [SerializeField] private AudioClip flowerCollect;
    [SerializeField] private AudioClip beeCollect;
    [SerializeField] private AudioClip takeDamage;
    [SerializeField] private AudioClip endGame;
    [SerializeField] private AudioClip windGame;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioSource ambianceSource;
    [SerializeField] private Transform childWaspList;
    [SerializeField] private GameObject childWasp;
    [SerializeField] private GameObject beeParticle;
    [SerializeField] private GameObject flowerParticle;
    [SerializeField] private GameObject mobParticle;
    private int _collectedBee;
    private int _collectedFlower;

    private void Awake()
    {
        Instance = this;
        _levelDatas = new List<LevelData>
        {
            _level1, Level2, Level3, Level4, Level5, Level6, Level7, Level8,
            Level9, Level10
        };
    }

    private void Start()
    {
        mapGenerator.Initialize(_levelDatas[Client.Instance.currentLevel-1]);
        audioSource.mute = !Client.Instance.player.sound;
        ambianceSource.mute = !Client.Instance.player.sound;
        mainWasp.GetComponent<ControlableWasp>().waspSpeed = _levelDatas[Client.Instance.currentLevel - 1].waspSpeed;
    }

    public void CollectBee(int value)
    {
        _collectedBee += value;
        collectedBeeText.text = _collectedBee.ToString();
        audioSource.PlayOneShot(beeCollect);
        var childCount = childWaspList.childCount;
        for (int i = 0; i < value; i++)
        {
            var currentChildValue = childCount + i;
            int zIndex = ((currentChildValue - (currentChildValue % 3))/3)+1;
            int xIndex = (currentChildValue % 3)-1;
            var obj = Instantiate(childWasp, childWaspList);
            obj.transform.localPosition = new Vector3(xIndex*0.5f, 0.4f, zIndex*0.5f);
        }

        DropParticle(beeParticle);
    }

    public void LoseBee(int value)
    {
        _collectedBee -= value;
        collectedBeeText.text = _collectedBee.ToString();
        audioSource.PlayOneShot(takeDamage);
        var currentValue = value;

        int i = 0;
        int max = childWaspList.childCount - 1;
        while (max-i >= 0 && currentValue>0)
        {
            GameObject.Destroy(childWaspList.GetChild(max-i).gameObject);
            currentValue--;
            i++;

        }

        DropParticle(mobParticle);
        if (_collectedBee<0)
        {
            Gameover();
        }
    }

    public void CollectFlower(int value)
    {
        _collectedFlower += value;
        collectedFlowerTmp.text = _collectedFlower.ToString();
        audioSource.PlayOneShot(flowerCollect);
        DropParticle(flowerParticle);
    }

    private void Gameover()
    {
        audioSource.PlayOneShot(endGame);
        loseScreen.SetActive(true);
        mainWasp.GetComponent<ControlableWasp>().waspSpeed = 0;
    }

    public void StartFight()
    {

        StartCoroutine(AttackMob(finalMobList, mainWasp.transform));

    }

    private IEnumerator AttackMob(Transform mobTransform, Transform wasp)
    {
        while (mobTransform.childCount!=0 || _collectedBee<0)
        {
            var child = mobTransform.GetChild(0);
            Vector3 direction = wasp.position - child.position;
            direction.y = 0f; 
            float lerpValue = 0;
            var startRotation = child.rotation;
            Quaternion lookRotation = Quaternion.LookRotation(direction);
            while (lerpValue<1)
            {
                child.rotation = Quaternion.Lerp(startRotation, lookRotation, lerpValue);
                lerpValue += Time.deltaTime;
                yield return new WaitForFixedUpdate();
            }

            var startPosition = child.position;
            lerpValue = 0;
            while (lerpValue<1)
            {
                if (child == null)
                {
                    break;
                }
                child.position = Vector3.Lerp(startPosition,wasp.position,lerpValue);
                lerpValue += Time.deltaTime;
                yield return new WaitForFixedUpdate();
            }
            yield return new WaitForFixedUpdate();
          

        }

        if (!loseScreen.activeSelf)
        {
            FinishLevel();
        }
        
    }

    public void SoundChange()
    {
        audioSource.mute = !Client.Instance.player.sound;
        ambianceSource.mute = !Client.Instance.player.sound;
    }

    private void FinishLevel()
    {
        winScreen.SetActive(true);
        audioSource.PlayOneShot(windGame);
        Client.Instance.player.LevelUp(Client.Instance.currentLevel+1);
        collectedFlowerTmp2.text = _collectedFlower.ToString();
        bonusFlowerText.text = (_collectedFlower*2).ToString();
        Client.Instance.player.AddFlower(_collectedFlower);
    }

    private void DropParticle(GameObject particle)
    {
        var part = Instantiate(particle);
        part.transform.position = new Vector3(0,0.65f,0)+mainWasp.transform.position;
    }
}
