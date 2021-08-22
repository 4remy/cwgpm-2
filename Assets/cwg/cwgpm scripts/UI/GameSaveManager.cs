using System.Collections.Generic;
using System.IO;
using UnityEngine;
using Schwer.ItemSystem;
using Schwer.IO;

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
        BinaryIO.WriteFile(playerInventory.value.Serialize(), $"{Application.persistentDataPath}/player.inv");
        BinaryIO.WriteFile(discoveredRecipes.ints, $"{Application.persistentDataPath}/recipes.dat");

        for (int i = 0; i < objects.Count; i++)
        {
            var json = JsonUtility.ToJson(objects[i]);
            BinaryIO.WriteFile(json, Application.persistentDataPath + string.Format("/{0}.json", i));
        }
    }

    public void LoadScriptables()
    {
        LoadInventory();
        LoadRecipes();

        for (int i = 0; i < objects.Count; i++)
        {
            var objPath = Application.persistentDataPath + string.Format("/{0}.json", i);
            if (File.Exists(objPath))
            {
                var json = BinaryIO.ReadFile<string>(objPath);
                JsonUtility.FromJsonOverwrite(json, objects[i]);
            }
        }
    }

    private void LoadInventory() {
        var path = $"{Application.persistentDataPath}/player.inv";
        if (File.Exists(path))
        {
            playerInventory.value = BinaryIO.ReadFile<SerializableInventory>(path).Deserialize(itemDB);
        }
    }

    private void LoadRecipes() {
        var path = $"{Application.persistentDataPath}/recipes.dat";
        if (File.Exists(path)) {
            discoveredRecipes.ints = BinaryIO.ReadFile<List<int>>(path);
        }
    }
}
