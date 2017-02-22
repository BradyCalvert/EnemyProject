using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class EnemyTool : EditorWindow {

    public List<Enemies> enemyList = new List<Enemies>();
    string newString;
    bool nameFlag;
    bool spriteFlag;
    bool isMagic;
    string[] enemyNames;
    int newHealth;
    int lastChoise = 0;
    Sprite newSprite;
    int newAttack;
    int newDefense;
    int newAgility;
    int mana;

    float attackTime;

    List<string> enemyNameList = new List<string>();
    int currentChoice = 0;


    [MenuItem("Custom Tool/ EnemyTool %g")]
    private static void blahblah()
    {
        EditorWindow.GetWindow<EnemyTool>();
    }

    void Awake()
    {
        getEnemies();
    }


    private void OnGUI()
    {
        currentChoice = EditorGUILayout.Popup(currentChoice, enemyNames);
        if(GUILayout.Button("Button"))
        {
            getEnemies();
        }
        newString = EditorGUILayout.TextField("Name: ", newString);
        newHealth = EditorGUILayout.IntSlider("Health: ",newHealth, 1,300);
        newAttack = EditorGUILayout.IntSlider("Attack: ",newAttack, 1, 100);
        newDefense = EditorGUILayout.IntSlider("Defense: ",newDefense, 1, 100);
        newAgility = EditorGUILayout.IntSlider("Agility: ",newAgility, 1, 100);
        attackTime = EditorGUILayout.Slider("Attack Time: ",attackTime,1.0f, 20.0f);
        newSprite = (Sprite)EditorGUILayout.ObjectField(newSprite,typeof(Sprite),newSprite);
        isMagic = EditorGUILayout.Toggle("Check if a magic user: ",isMagic);
        if(isMagic==true)
        {
           mana = EditorGUILayout.IntSlider("Mana: ", mana, 1, 100);
        }



    if (newString==null)
        {
            EditorGUILayout.HelpBox("Name cannot be blank", MessageType.Error);
        }
    if(newSprite==null)
        {
           EditorGUILayout.HelpBox("Sprite cannot be empty", MessageType.Error);
        }
        if (currentChoice == 0)
        {
            if (GUILayout.Button("Create New"))
            {
                CreateEnemy();
            }

        }
        else
        {
            if (GUILayout.Button("Save"))
            {
                alterEnemy();
            }
        }
        if(currentChoice!=lastChoise)
        {
            if(currentChoice==0)
            {
                //blank out fields for enemy
                newString = "";
                
            }
            else
            {
                //Load fields with enemy data
                newString=enemyList[currentChoice - 1].emname;
                newHealth = enemyList[currentChoice - 1].health;
                newAttack = enemyList[currentChoice - 1].atk;
                newDefense = enemyList[currentChoice - 1].def;
                newAgility = enemyList[currentChoice - 1].agi;
                attackTime = enemyList[currentChoice - 1].atkTime;
                newSprite = enemyList[currentChoice - 1].mySprite;
                mana = enemyList[currentChoice - 1].manaPool;
        AssetDatabase.SaveAssets();
      }
            lastChoise = currentChoice;
        }

        
    }
    private void getEnemies()
    {

        enemyList.Clear();
        string[] guids = AssetDatabase.FindAssets("t:Enemies",null);
        foreach (string guid in guids)
        {
            string myString = AssetDatabase.GUIDToAssetPath(guid);

            Enemies enemyInst=AssetDatabase.LoadAssetAtPath(myString, typeof(Enemies)) as Enemies;

            enemyList.Add(enemyInst);
        }
        foreach (Enemies e in enemyList)
        {
            enemyNameList.Add(e.emname);
        }
        enemyNameList.Insert(0, "New");
        enemyNames = enemyNameList.ToArray();
    }



    private void CreateEnemy()
    {
    //Enemies myEnemy = ScriptableObject.CreateInstance<Enemies>();
    if (newString == null)
    {
      EditorGUILayout.HelpBox("Name cannot be blank", MessageType.Error);
    }
    if (newSprite == null)
    {
      EditorGUILayout.HelpBox("Sprite cannot be empty", MessageType.Error);
    }
    else
        {
            Enemies myEnemy = ScriptableObject.CreateInstance<Enemies>();
            myEnemy.emname = newString;
            myEnemy.agi = newAgility;
            myEnemy.atk = newAttack;
            myEnemy.def = newDefense;
            myEnemy.health = newHealth;
            myEnemy.atkTime = attackTime;
            myEnemy.mySprite = newSprite;
            myEnemy.isMagic = isMagic;
            myEnemy.manaPool = mana;
            //AssetDatabase.CreateAsset(myEnemy, "Assets/Resources/Data/EnemyData/" + myEnemy.emname.Replace(" ", "_") + ".asset");
      AssetDatabase.CreateAsset(myEnemy, "Assets/Resources/Data/EnemyData/" + myEnemy.emname.Replace(" ", "_") + ".asset");
      getEnemies();
      AssetDatabase.SaveAssets();
    }
    }
          
        


    private void alterEnemy()
    {
        enemyList[currentChoice-1].health = newHealth;
        enemyList[currentChoice-1].atk = newAttack;
        enemyList[currentChoice-1].def = newDefense;
        enemyList[currentChoice-1].agi = newAgility;
        enemyList[currentChoice - 1].atkTime = attackTime;
        enemyList[currentChoice - 1].mySprite = newSprite;
        enemyList[currentChoice - 1].manaPool=mana;
    AssetDatabase.SaveAssets();


  }
}

