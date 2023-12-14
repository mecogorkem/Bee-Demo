using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LevelSelectPage : MonoBehaviour
{
    [SerializeField] private Transform content;
    [SerializeField] private GameObject levelSelectListObj;
    [SerializeField] private float speed =1;
    private RectTransform _rectTransform;
    private GridLayoutGroup _gridLayoutGroup;

    private void Awake()
    {
        _gridLayoutGroup = content.GetComponent<GridLayoutGroup>();
        _rectTransform = content.GetComponent<RectTransform>();
        for (int i = 0; i < Client.Instance.player.MaxLevel; i++)
        {
            var obj = Instantiate(levelSelectListObj, content);
            var level = i + 1;
            obj.name = level.ToString();
            obj.GetComponent<Button>().onClick.AddListener(() =>
            {
                Client.Instance.StartLevel(int.Parse(obj.name));
            });
            obj.GetComponent<Button>().interactable = level <= Client.Instance.player.reachedLevel;
            obj.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = $"Level {level}";
        }

    }

    private void OnEnable()
    {
        StartCoroutine(Anim());
    }

    void RectUpdate()
    {
        LayoutRebuilder.ForceRebuildLayoutImmediate(_rectTransform);
        _rectTransform.sizeDelta = new Vector2(_rectTransform.sizeDelta.x, _gridLayoutGroup.preferredHeight);
    }

    IEnumerator Anim()
    {
        var startValue = 0.2f;
        this.transform.localScale = new Vector3(startValue, startValue, startValue);
        yield return new WaitForFixedUpdate();
        RectUpdate();
        while (startValue<1)
        {
            yield return new WaitForFixedUpdate();
            startValue += speed * Time.deltaTime;
            this.transform.localScale = new Vector3(startValue, startValue, startValue);
        }
        this.transform.localScale = new Vector3(1, 1, 1);

    }
}
