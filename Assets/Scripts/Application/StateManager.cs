using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

namespace Grind
{
    public class StateManager : MonoBehaviour
    {
        [SerializeField]
        private Dictionary<string, float> flags = new Dictionary<string, float>();

        public static StateManager Instance { get; private set; }

        private void Awake()
        {
            if(Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
            } else if (Instance != this)
            {
                Destroy(gameObject);
            }
        }

        #region Flag Operations

        public void SetFlag(string key, float value)
        {
            if (flags.ContainsKey(key))
            {
                flags[key] = value;
            } else
            {
                flags.Add(key, value);
            }
        }

        public float GetFlag(string key)
        {
            return flags[key];
        }

        public bool TryGetFlag(string key, out float value)
        {
            return flags.TryGetValue(key, out value);
        }

        public void AddToFlag(string key, float amount)
        {
            float val = 0;
            TryGetFlag(key, out val);
            SetFlag(key, val + amount);
        }

        #endregion

        #region File Operations

        public void Save()
        {
            SaveGameData(UnityEngine.Application.persistentDataPath + "/savegame.dat");
        }

        public void Load()
        {
            LoadGameData(UnityEngine.Application.persistentDataPath + "/savegame.dat");
        }

        private void SaveGameData(string savePath)
        {
            if (String.IsNullOrEmpty(savePath))
            {
                throw new Exception("Save path was empty");
            }

            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Create(savePath);
            bf.Serialize(file, flags);
            file.Close();
            Debug.Log("File saved to: " + savePath);
        }

        private void LoadGameData(string savePath)
        {
            if (File.Exists(savePath))
            {
                Debug.Log("Loading game...");
                BinaryFormatter bf = new BinaryFormatter();
                FileStream file = File.Open(savePath, FileMode.Open);
                Dictionary<string, int> gameData = (Dictionary<string, int>)bf.Deserialize(file);
                file.Close();
            }
            else
            {
                Debug.Log("There is no save game to load");
            }
        }

        #endregion
    }
}
