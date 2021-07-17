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
            if (File.Exists(Application.persistentDataPath + string.Format("/{0}.json", i)))
            {
                File.Delete(Application.persistentDataPath + string.Format("/{0}.json", i));
            }
        }
    }

    private void OnEnable()
    {
        LoadScriptables();
    }

    /* private void OnDisable()
     {
         SaveScriptables();
     }
    */

    public void SaveScriptables()
    {
        FileStream invFile = File.Create(Application.persistentDataPath + "/player.inv");
        BinaryFormatter bf = new BinaryFormatter();
        var invData = playerInventory.value.Serialize();
        bf.Serialize(invFile, invData);

        for (int i = 0; i < objects.Count; i++)
        {
            FileStream file = File.Create(Application.persistentDataPath + string.Format("/{0}.json", i));
            BinaryFormatter binary = new BinaryFormatter();
            var json = JsonUtility.ToJson(objects[i]);
            binary.Serialize(file, json);
            file.Close();
        }

    }

    public void LoadScriptables()
    {
        if (File.Exists(Application.persistentDataPath + "/player.inv"))
        {
            FileStream invFile = File.Open(Application.persistentDataPath + "/player.inv", FileMode.Open);
            BinaryFormatter bf = new BinaryFormatter();
            var serializedInvData = (SerializableInventory)bf.Deserialize(invFile);
            playerInventory.value = serializedInvData.Deserialize(itemDB);
        }
        for (int i = 0; i < objects.Count; i++)
        {
            if (File.Exists(Application.persistentDataPath + string.Format("/{0}.json", i)))
            {
                FileStream file = File.Open(Application.persistentDataPath + string.Format("/{0}.json", i), FileMode.Open);
                BinaryFormatter binary = new BinaryFormatter();
                JsonUtility.FromJsonOverwrite((string)binary.Deserialize(file), objects[i]);
                file.Close();
            }
        }
    }
}
