using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class NewGameController : MonoBehaviour
{

    [SerializeField]
    private Sprite bgImage;
    public Sprite[] puzzles;
    public List<Sprite> gamePuzzles = new List<Sprite>();
    private string firstGuessPuzzle, secondGuessPuzzle;
    private int firstGuessIndex, secondGuessIndex;
    private bool firstGuess, secondGuess;

    private int countGuesses;
    private int countCorrectGuesses;
    private int gameGuesses;



    public List<Button> btns = new List<Button>();
    void Awake()
    {
        puzzles = Resources.LoadAll<Sprite>("Sprites/Images");

    }
    void Start()
    {

        GetButtons();
        AddListeners();
        AddGamePuzzles();
        Shuffle(gamePuzzles);
        gameGuesses = gamePuzzles.Count / 2;
    }

    void GetButtons()
    {
        GameObject[] objects = GameObject.FindGameObjectsWithTag("PuzzleButton");
        for (int i = 0; i < objects.Length; i++)
        {transform.Rotate(new Vector3(15, 30, 45) * Time.deltaTime);
            btns.Add(objects[i].GetComponent<Button>());
            //transform.Rotate(new Vector3(0, 10, 0));
           // objects[i].GetComponent<Transform>(new Vector3(0, 10, 0));
            btns[i].image.sprite = bgImage;
            
        }

    }
    void AddGamePuzzles()
    {
        int looper = btns.Count;
        int index = 0;
        for (int i = 0; i < looper; i++)
        {
            if (index == looper / 2)
            {
                index = 0;
            }
            gamePuzzles.Add(puzzles[index]);
            index++;

        }



    }
    void AddListeners()
    {
        foreach (Button btn in btns)
        {
            btn.onClick.AddListener(() => PickPuzzle());
        }


    }
    public void PickPuzzle()
    {
        string name = UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject.name;


        if (!firstGuess)
        {
            firstGuess = true;
            firstGuessIndex = int.Parse(name);

            firstGuessPuzzle = gamePuzzles[firstGuessIndex].name;
            btns[firstGuessIndex].image.sprite = gamePuzzles[firstGuessIndex];
        }
        else if (!secondGuess)
        {
            secondGuess = true;
            secondGuessIndex = int.Parse(name);
            secondGuessPuzzle = gamePuzzles[secondGuessIndex].name;
            btns[secondGuessIndex].image.sprite = gamePuzzles[secondGuessIndex];
            //if (firstGuessPuzzle == secondGuessPuzzle)
            //{
            //    Debug.Log("the puzzle match");
            //}
            //else { Debug.Log("the puzzle dont match"); }
            countGuesses++;
            StartCoroutine(CheckIfPuzzleMatch());
        }

    }
    IEnumerator CheckIfPuzzleMatch()
    {
        yield return new WaitForSeconds(1f);
        if (firstGuessPuzzle == secondGuessPuzzle)
        {
            yield return new WaitForSeconds(1f);
            transform.Rotate(new Vector3(0, 10, 0));
            btns[firstGuessIndex].interactable = false;
            btns[secondGuessIndex].interactable = false;

            btns[firstGuessIndex].image.color = new Color(0, 0, 0, 0);
            btns[secondGuessIndex].image.color = new Color(0, 0, 0, 0);

            CheckIfGameIsFinished();

        }
        else
        {

            btns[firstGuessIndex].image.sprite = bgImage;
            btns[secondGuessIndex].image.sprite = bgImage;

        }
        yield return new WaitForSeconds(1f);
        firstGuess = secondGuess = false;

    }
    void CheckIfGameIsFinished()
    {

        countCorrectGuesses++;
        if (countCorrectGuesses == gameGuesses)
        {

            Debug.Log("Oyun Bitti");
            Debug.Log(countGuesses + " deneme yaptın.");

            StartCoroutine(SonrakiSahne());
        }

    }
    void Shuffle(List<Sprite> list)
    {
        for (int i = 0; i < list.Count; i++)
        {
            Sprite temp = list[i];
            int randomIndex = Random.Range(i, list.Count);
            list[i] = list[randomIndex];
            list[randomIndex] = temp;


        }

    }
    IEnumerator SonrakiSahne()
    {
        yield return new WaitForSeconds(3);
        //Sahne Değiştirme
        // Application.Quit(); //çıkış
        SceneManager.LoadScene("TekEkran");
        //SceneManager.LoadScene(1);

    }
}
