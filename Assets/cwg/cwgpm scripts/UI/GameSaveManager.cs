using System.Collections.Generic;
using System.IO;
using UnityEngine;
using Schwer.ItemSystem;
using Schwer.IO;
using Schwer;

public class GameSaveManager : DDOLSingleton<GameSaveManager>
{
    private static string recipePath => $"{Application.persistentDataPath}/recipes.dat";

    [SerializeField] private ItemDatabase itemDB = default;
    [SerializeField] private List<ScriptableObject> objects = new List<ScriptableObject>();
    [SerializeField] private IntListSO discoveredRecipes = default;
    [SerializeField] private InventorySO[] inventories = default;

    private void OnEnable()
    {
        LoadScriptables();
    }

    private void OnDisable()
    {
        SaveScriptables();
    }

    public void ResetScriptables()
    {
        ResetDiscoveredRecipes();
        ResetObjects();
        ResetInventories();
        Debug.Log("Reset scriptables");
    }

    private void ResetDiscoveredRecipes()
    {
        discoveredRecipes.ints.Clear();

        if (File.Exists(recipePath))
        {
            File.Delete(recipePath);
        }
    }

    private void ResetObjects()
    {
        for (int i = 0; i < objects.Count; i++)
        {
            // Not ideal, but other methods would require restructuring some Scriptable Objects
            switch (objects[i])
            {
                default:
                    break;
                case BoolValue boolValue:
                    boolValue.RuntimeValue = boolValue.initialValue;
                    break;
                case FloatValue floatValue:
                    floatValue.RuntimeValue = floatValue.initialValue;
                    break;
                case CoinCounter coins:
                    coins.coins = 0;
                    break;
            }

            var path = Application.persistentDataPath + string.Format("/{0}.json", i);
            if (File.Exists(path))
            {
                File.Delete(path);
            }
        }
    }

    private void ResetInventories()
    {
        for (int i = 0; i < inventories.Length; i++)
        {
            inventories[i].value.Clear();

            var path = $"{Application.persistentDataPath}/{i}.inv";
            if (File.Exists(path))
            {
                File.Delete(path);
            }
        }
    }

    public void SaveScriptables()
    {
        BinaryIO.WriteFile(discoveredRecipes.ints, recipePath);

        for (int i = 0; i < inventories.Length; i++)
        {
            BinaryIO.WriteFile(inventories[i].value.Serialize(), $"{Application.persistentDataPath}/{i}.inv");
        }

        for (int i = 0; i < objects.Count; i++)
        {
            var json = JsonUtility.ToJson(objects[i]);
            BinaryIO.WriteFile(json, Application.persistentDataPath + string.Format("/{0}.json", i));
        }

        Debug.Log("Saved scriptables");
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
        if (File.Exists(recipePath))
        {
            discoveredRecipes.ints = BinaryIO.ReadFile<List<int>>(recipePath);
        }
    }
}
