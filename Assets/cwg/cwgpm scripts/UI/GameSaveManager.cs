using System.Collections.Generic;
using System.IO;
using UnityEngine;
using Schwer.ItemSystem;
using Schwer.IO;
using Schwer;

public class GameSaveManager : DDOLSingleton<GameSaveManager>
{
    [SerializeField] private ItemDatabase itemDB = default;
    [SerializeField] private List<ScriptableObject> objects = new List<ScriptableObject>();
    [SerializeField] private IntListSO discoveredRecipes = default;
    [SerializeField] private InventorySO[] inventories = default;

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
        BinaryIO.WriteFile(discoveredRecipes.ints, $"{Application.persistentDataPath}/recipes.dat");

        for (int i = 0; i < inventories.Length; i++)
        {
            BinaryIO.WriteFile(inventories[i].value.Serialize(), $"{Application.persistentDataPath}/{i}.inv");
        }

        for (int i = 0; i < objects.Count; i++)
        {
            var json = JsonUtility.ToJson(objects[i]);
            BinaryIO.WriteFile(json, Application.persistentDataPath + string.Format("/{0}.json", i));
        }
    }

    public void LoadScriptables()
    {
        LoadRecipes();
        LoadInventories();

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

    private void LoadInventories()
    {
        for (int i = 0; i < inventories.Length; i++)
        {
            var path = $"{Application.persistentDataPath}/{i}.inv";
            if (File.Exists(path))
            {
                inventories[i].value = BinaryIO.ReadFile<SerializableInventory>(path).Deserialize(itemDB);
            }
        }
    }

    private void LoadRecipes()
    {
        var path = $"{Application.persistentDataPath}/recipes.dat";
        if (File.Exists(path))
        {
            discoveredRecipes.ints = BinaryIO.ReadFile<List<int>>(path);
        }
    }
}
