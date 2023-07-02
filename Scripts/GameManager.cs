using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public bool IsPlayMode
    {
        get { return isPlayMode; }
        set { isPlayMode = value; }
    }
    bool isPlayMode;

    public bool IsTitleBack
    {
        get { return isTitleBack; }
    }
    bool isTitleBack;


    [SerializeField] GameObject startPanel;
    [SerializeField] Text saunaText;
    [SerializeField] GameObject titleBackPanel;
    AudioSource audioBGM;
    [SerializeField] AudioClip defaultClip;
    [SerializeField] AudioClip lastClip;
    [SerializeField] AudioClip fireClip;
    private void Awake()
    {
        audioBGM = GetComponent<AudioSource>();
        isPlayMode = false;
        isTitleBack = false;
        startPanel.SetActive(true);
    }
    void Start()
    {
        isPlayMode = false;
        audioBGM.Play();
    }


    public void HideStartPanel()
    {
        startPanel.SetActive(false);
        isPlayMode = true;
    }

    public void FireBGM()
    {
        audioBGM.clip = fireClip;
        audioBGM.Play();
    }
    public void DefaultBGM()
    {
        audioBGM.clip = defaultClip;
        audioBGM.Play();
    }
    public void LastBGM()
    {
        audioBGM.clip = lastClip;
        audioBGM.Play();
    }




    //example
    //一列１６文字までです。
    //3列まで記入できます。

    List<string> words = new List<string>()
    {
        "なんだか物騒な場所だな。\n敵がたくさんいる。\n気を付けないと。",
        "サウナの発祥の地はフィンランド\nらしい。日本独自の文化\nじゃないんだね。",
        "フィンランド語で蒸し風呂を意味する\nSaunaが語源らしいね。",
        "サウナには乾式と温式があるよ\nでも、一般的に乾式をサウナと\n呼ぶみたい。",
        "ベンチの上に行くほど熱いよね。\n僕は熱いの苦手だから\n下の席でいいや。",
        "サウナってヤケドしないのかな？\n椅子に接している部分は\n熱くなってくるよね。",
        "水風呂は慣れるまで苦手だったな。\nこどものころはプールと\n勘違いしてたよ。",
        "お腹いっぱいの時のサウナは\nあまり健康に良くないよ。",
        "じめじめとした空気だな。\nもしかしたらこの先のサウナは\nミストサウナかもしれない。",
        "ここらへんの草や木を\nサウナに入れたら効果あるかな？\nいや、やめておこう。",
        "ここらへんの石を\nサウナに入れたらロウリュみたいに\n活用できるかなぁ。",
        "これだけ剣を振った後のサウナは\nさぞ効くんだろうなぁ。",
        "水風呂がなくたって\n整えるはずだ！\nだってこんなにも\n外は寒いんだから！",
        "サウナは血流を促進するから\n健康にとってもいいんだ！",
        "風邪を引いていたり\n飲酒しているときのサウナは\n行かないほうがいいかも。",
        "サウナに入れば脂肪が燃焼される。\nつまり痩せれるということだ！",
        "そういえば敵から攻撃されないな\nサウナの周りに居る敵に\n悪いやつはいないってことか。",
        "ここまで遊んでくれてありがとう！\n作:HiroGames66\n今後の活動も応援お願いします。",
    };


    public void ShowWords()
    {
        saunaText.text = words[Random.Range(0, words.Count-1)];
        AudioManager.instance.WordsSE();
    }
    public void LastWords()
    {
        saunaText.text = words[words.Count -1];
        AudioManager.instance.WordsSE();
    }

    public void End()
    {
        titleBackPanel.SetActive(true);
        isTitleBack = true;
    }

    public void TitleBack()
    {
        SceneManager.LoadScene(0);
    }
    
}
