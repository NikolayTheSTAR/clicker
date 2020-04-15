using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;

namespace Utility
{
    public class EnumOperations
    {
        public static T ParseEnum<T>(string value) // Convert string to enum
        {
            return (T)Enum.Parse(typeof(T), value, true);
        }
    }

    public class LoadFromFile
    {
        public static Texture2D LoadAsTexture2D(string filePath) // Load file as Texture2D
        {
            Texture2D tex = null;
            byte[] fileData;

            if (File.Exists(filePath))
            {
                fileData = File.ReadAllBytes(filePath);
                tex = new Texture2D(2, 2);
                tex.LoadImage(fileData);
            }
            return tex;
        }
    }

    public class ListOperations
    {
        public static void DestroyListObjects(List<GameObject> list) // Destroy objects contained in the list
        {
            foreach (GameObject obj in list)
            {
                MonoBehaviour.Destroy(obj);
            }
        }
    }

    public class Serialization
    {
        private static string DataPath
        {
            get
            {
                return Path.Combine(Application.persistentDataPath, "ErrorLog.mrd");
            }
        }

        public static bool SaveToBinnary<T>(T SerializableObject) // Save universal type object into file
        {
            try
            {
                using (FileStream fs = File.Create(DataPath))
                {
                    BinaryFormatter formatter = new BinaryFormatter();
                    try
                    {
                        formatter.Serialize(fs, SerializableObject);
                        return true;
                    }
                    catch
                    {
                        Debug.LogError("Failed serialization");
                        return false;
                    }
                }
            }
            catch
            {
                Debug.LogWarning("File is already opened");
                return false;
            }
        }

        public static bool SaveToBinnary<T>(String FilePath, T SerializableObject) // Save universal type object into file
        {
            try
            {
                using (FileStream fs = File.Create(FilePath))
                {
                    BinaryFormatter formatter = new BinaryFormatter();
                    try
                    {
                        formatter.Serialize(fs, SerializableObject);
                        return true;
                    }
                    catch
                    {
                        Debug.LogError("Failed serialization");
                        return false;
                    }
                }
            }
            catch
            {
                Debug.LogWarning("File is already opened");
                return false;
            }
        }

        public static bool LoadFromBinnary<T>(out T result) // Load universal type object from file
        {
            try
            {
                using (FileStream fs = File.Open(DataPath, FileMode.Open))
                {
                    BinaryFormatter formatter = new BinaryFormatter();
                    try
                    {
                        result = (T)formatter.Deserialize(fs);
                        return true;
                    }
                    catch (Exception e)
                    {
                        //Debug.LogError("Failed deserialization");
                        Debug.LogError(e.Message);
                        result = default(T);
                        return false;
                    }
                }
            }
            catch
            {
                result = default(T);
                return false;
            }
        }

        public static bool LoadFromBinnary<T>(String FilePath, out T result) // Load universal type object from file
        {
            try
            {
                using (FileStream fs = File.Open(FilePath, FileMode.Open))
                {
                    BinaryFormatter formatter = new BinaryFormatter();
                    try
                    {
                        result = (T)formatter.Deserialize(fs);
                        return true;
                    }
                    catch (Exception e)
                    {
                        //Debug.LogError("Failed deserialization");
                        Debug.LogError(e.Message);
                        result = default(T);
                        return false;
                    }
                }
            }
            catch
            {
                result = default(T);
                return false;
            }
        }
        
        public static bool LoadFromTextAsset<T>(TextAsset textAsset, out T result) // Load universal type object from TextAsset
        {
            try
            {
                MemoryStream memStream = new MemoryStream();
                memStream.Write(textAsset.bytes, 0, textAsset.bytes.Length);
                memStream.Seek(0, SeekOrigin.Begin);
                BinaryFormatter formatter = new BinaryFormatter();
                try
                {
                    result = (T)formatter.Deserialize(memStream);
                    Debug.Log("Deserialization success");
                    return true;
                }
                catch (SerializationException e)
                {
                    Debug.LogError("Failed deserialization");
                    result = default(T);
                    return false;
                }
            }
            catch (Exception e)
            {
                Debug.LogWarning(e.Message);
                result = default(T);
                return false;
            }
        }
    }
}