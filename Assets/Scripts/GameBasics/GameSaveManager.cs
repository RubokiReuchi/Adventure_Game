using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class GameSaveManager : MonoBehaviour
{
    [Header("List of Objects")]
    public List<ScriptableObject> objects = new List<ScriptableObject>();
    public Inventory inventory;
    public List<BoolValue> bools = new List<BoolValue>();
    public List<FloatValue> floats = new List<FloatValue>();
    public List<VectorValue> vectors = new List<VectorValue>();
    public List<EstusValue> estus = new List<EstusValue>();

    public void ResetScriptables()
    {
        for (int i = 0; i < objects.Count; i++)
        {
            if (File.Exists(Application.persistentDataPath +
                string.Format("/{0}.dat", i)))
            {
                File.Delete(Application.persistentDataPath +
                string.Format("/{0}.dat", i));
            }
        }
        ResetToInitialValues();
    }

    public void SaveScriptables()
    {
        for (int i = 0; i < objects.Count; i++)
        {
            FileStream file = File.Create(Application.persistentDataPath +
                string.Format("/{0}.dat", i));
            BinaryFormatter binary = new BinaryFormatter();
            var json = JsonUtility.ToJson(objects[i]);
            binary.Serialize(file, json);
            file.Close();
        }
    }

    public void LoadScriptables()
    {
        for (int i = 0; i < objects.Count; i++)
        {
            if (File.Exists(Application.persistentDataPath +
                string.Format("/{0}.dat", i)))
            {
                FileStream file = File.Open(Application.persistentDataPath +
                string.Format("/{0}.dat", i), FileMode.Open);
                BinaryFormatter binary = new BinaryFormatter();
                JsonUtility.FromJsonOverwrite((string)binary.Deserialize(file), objects[i]);
                file.Close();
            }
        }
    }

    private void ResetToInitialValues()
    {
        // inventory
        inventory.Reset();
        // bools
        for (int i = 0; i < bools.Count; i++)
        {
            bools[i].Reset();
        }
        // floats
        for (int i = 0; i < floats.Count; i++)
        {
            floats[i].Reset();
        }
        // vectors
        for (int i = 0; i < vectors.Count; i++)
        {
            vectors[i].Reset();
        }
        // estus
        for (int i = 0; i < estus.Count; i++)
        {
            estus[i].Reset();
        }
    }

    public void All_Items()
    {
        for (int i = 0; i < bools.Count; i++)
        {
            bools[i].currentValue = true;
        }
    }

    public void No_Items()
    {
        for (int i = 0; i < bools.Count; i++)
        {
            bools[i].currentValue = false;
        }
    }
}