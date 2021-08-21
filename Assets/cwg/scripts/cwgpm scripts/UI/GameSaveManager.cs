using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using Schwer.ItemSystem;

public class GameSaveManager : MonoBehaviour
{
    public static GameSaveManager gameSave;
    [SerializeField] private ItemDatabase itemDB = default;
    public List<ScriptableObject> objects = new List<ScriptableObject>();
    [SerializeField] private InventorySO playerInventory = default;
    [SerializeField] private IntListSO discoveredRecipes = default;

    /*
    private void Awake()
    {
        if(gameSave == null)
        {
            gameSave = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
        DontDestroyOnLoad(this);
    }
    */
    public void ResetScriptables()
    {
        // this needs to reset the boolvalues to false
        //or do i just do it by hand
        for (int i = 0; i < objects.Count; i++)
        {
            var path = Application.persistentDataPath + string.Format("/{0}.json", i);
            if (File.Exists(path))
            {
                File.Delete(path);
            }
        }
    }

    private void OnEnable()
    {
        LoadScriptables();
    }

    private void OnDisable()
     {
         SaveScriptables();
     }
    

    public void SaveScriptables()
    {
        FileStream invFile = File.Create(Application.persistentDataPath + "/player.inv");
        BinaryFormatter bf = new BinaryFormatter();
        var invData = playerInventory.value.Serialize();
        bf.Serialize(invFile, invData);
        invFile.Close();

        Schwer.IO.BinaryIO.WriteFile(discoveredRecipes.ints, $"{Application.persistentDataPath}/recipes.dat");

        for (int i = 0; i < objects.Count; i++)
        {
            FileStream file = File.Create(Application.persistentDataPath + string.Format("/{0}.json", i));
            var json = JsonUtility.ToJson(objects[i]);
            bf.Serialize(file, json);
            file.Close();
        }
    }

    public void LoadScriptables()
    {
        var invPath = Application.persistentDataPath + "/player.inv";
        if (File.Exists(invPath))
        {
            FileStream invFile = File.Open(invPath, FileMode.Open);
            BinaryFormatter bf = new BinaryFormatter();
            var serializedInvData = (SerializableInventory)bf.Deserialize(invFile);
            playerInventory.value = serializedInvData.Deserialize(itemDB);
            invFile.Close();
        }
        for (int i = 0; i < objects.Count; i++)
        {
            var objPath = Application.persistentDataPath + string.Format("/{0}.json", i);
            if (File.Exists(objPath))
            {
                FileStream file = File.Open(objPath, FileMode.Open);
                BinaryFormatter binary = new BinaryFormatter();
                JsonUtility.FromJsonOverwrite((string)binary.Deserialize(file), objects[i]);
                file.Close();
            }
        }

        discoveredRecipes.ints = Schwer.IO.BinaryIO.ReadFile<List<int>>($"{Application.persistentDataPath}/recipes.dat");
    }
}
