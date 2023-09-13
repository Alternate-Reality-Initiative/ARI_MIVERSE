using UnityEngine;
using System.IO;
using System.Collections.Generic;
using UnityEngine.Diagnostics;
using System.Linq;
using ProtoTurtle.BitmapDrawing;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine.InputSystem;

public class RemoveBackground : MonoBehaviour
{
    private string filePath;
    //string imageFolderPath = @"C:\Users\sonia\Documents\Miverse-images";
    string imageFolderPath = @"/Users/emileemeng/Desktop/miverse-imagetesting";

    [System.Serializable]
    public class SerializableVector3
    {
        public float x;
        public float y;
        public float z;

        public SerializableVector3(Vector3 vector)
        {
            x = vector.x;
            y = vector.y;
            z = vector.z;
        }

        public Vector3 ToVector3()
        {
            return new Vector3(x, y, z);
        }
    }
    [System.Serializable]
    public class GraffitiPositions
    {
        public List<SerializableVector3> positions = new List<SerializableVector3>();
    }

    public List<SerializableVector3> graffitiPositions = new List<SerializableVector3>();

    public GameObject plane;
    private List<string> _loadedImagePaths = new List<string>();

    public LayerMask playerLayer;

    int photoCount = 0;

    public Transform player;
    public Camera currentCamera;
    public bool graffitiMode = false;

    private InputAction mouseClickAction;

    public static RemoveBackground Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }
        filePath = Application.persistentDataPath + "/playerInfo.dat";

        mouseClickAction = new InputAction(binding: "<Mouse>/leftButton");
        mouseClickAction.performed += OnMouseClick;
        mouseClickAction.Enable();
    }
    private void Start()
    {
        LoadPlayerData();
    }
    private void Update()
    {
        //if (Input.GetMouseButtonDown(0) && graffitiMode)
        //{
        //    Ray ray = currentCamera.ScreenPointToRay(Input.mousePosition);
        //    RaycastHit hit;

        //    LayerMask layerMask = playerLayer;

        //    if (Physics.Raycast(ray, out hit, Mathf.Infinity, layerMask))
        //    {
        //        Debug.Log("Hit object: " + hit.collider.gameObject.name);
        //        if (hit.collider.tag == "graffitiWall")
        //        {
        //            LoadPhotos(hit.point);

        //            //player.gameObject.SetActive(true);
        //            //graffitiMode = false;
        //        }
        //    }
        //    else
        //    {
        //        Debug.Log("No object hit.");
        //    }
        //}
    }
    private void OnEnable()
    {
        // Enable the mouse click action when the script is enabled
        mouseClickAction.Enable();
    }

    private void OnDisable()
    {
        // Disable the mouse click action when the script is disabled
        mouseClickAction.Disable();
    }
    private void OnMouseClick(InputAction.CallbackContext context)
    {
        if (graffitiMode)
        {
            Ray ray = currentCamera.ScreenPointToRay(Mouse.current.position.ReadValue());
            RaycastHit hit;

            LayerMask layerMask = playerLayer;

            if (Physics.Raycast(ray, out hit, Mathf.Infinity, layerMask))
            {
                Debug.Log("Hit object: " + hit.collider.gameObject.name);
                if (hit.collider.CompareTag("graffitiWall"))
                {
                    LoadPhotos(hit.point);
                }
            }
            else
            {
                Debug.Log("No object hit.");
            }
        }
    }

    private void LoadPhotos(Vector3 graffitiPosition)
    {
        //string imageFolderPath = @"C:\Users\sonia\Documents\Miverse-images"; // Update this path to your image folder
        //List<string> jpgFilePaths = new List<string>(Directory.GetFiles(imageFolderPath, "*.jpg"));
        //List<string> pngFilePaths = new List<string>(Directory.GetFiles(imageFolderPath, "*.png"));
        //List<string> filePaths = new List<string>(jpgFilePaths);
        //filePaths.AddRange(pngFilePaths);

        DirectoryInfo directory = new DirectoryInfo(imageFolderPath);
        FileInfo[] files = directory.GetFiles();
        List<FileInfo> sortedFiles = new List<FileInfo>(files);
        sortedFiles.Sort((file1, file2) => file1.CreationTime.CompareTo(file2.CreationTime));

        if (sortedFiles.Count > 0)
        {
            string fileName = sortedFiles[sortedFiles.Count - 1].Name;
            Debug.Log(fileName);
        //}
        //foreach (string filePath in filePaths)
        //{
            if (!_loadedImagePaths.Contains(fileName))
            {
                _loadedImagePaths.Add(fileName);
                Debug.Log($"Found new image at {fileName}");

                byte[] imageBytes = File.ReadAllBytes(imageFolderPath + "/" + fileName);
                Texture2D tex = new Texture2D(2, 2);

                tex.LoadImage(imageBytes);

                Texture2D newTexture = new Texture2D(tex.width, tex.height, TextureFormat.RGBA32, false);

                for (int y = 0; y < tex.height; y++)
                {
                    for (int x = 0; x < tex.width; x++)
                    {
                        Color pixelColor = tex.GetPixel(x, y);
                        Color newPixelColor = new Color(pixelColor.r, pixelColor.g, pixelColor.b, 1);

                        newTexture.SetPixel(x, y, newPixelColor);
                    }
                }

                newTexture.Apply();

                newTexture.FloodClear(5, 5, new Color(0, 0, 0, 0));
                newTexture.Apply();


                graffitiPositions.Add(new SerializableVector3(graffitiPosition));
                SavePlayerData();

                GameObject obj = Instantiate(plane, graffitiPosition, Quaternion.Euler(90,-90,0));
                //obj.transform.position = Vector3.zero;
                MeshRenderer plane_mr = obj.GetComponent<MeshRenderer>();

                Material newMat = new Material(Shader.Find("Unlit/Transparent Cutout"));

                //UnityEditor.AssetDatabase.CreateAsset(newTexture, "Assets/Resources/" + "matpng_" + photoCount + ".asset");

                newMat.SetTexture("_MainTex", newTexture);

                //UnityEditor.AssetDatabase.CreateAsset(newMat, "Assets/Resources/" + "mat_" + photoCount + ".mat");
                //UnityEditor.AssetDatabase.Refresh();

                plane_mr.material = newMat;

                photoCount += 1;
            }
        }
    }
    public void SavePlayerData()
    {
        GraffitiPositions saveData = new GraffitiPositions();
        saveData.positions = graffitiPositions;

        BinaryFormatter formatter = new BinaryFormatter();
        using (FileStream fileStream = new FileStream(filePath, FileMode.Create))
        {
            formatter.Serialize(fileStream, saveData);
        }
    }

    public void LoadPlayerData()
    {
        if (File.Exists(filePath))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            using (FileStream fileStream = new FileStream(filePath, FileMode.Open))
            {
                GraffitiPositions saveData = (GraffitiPositions)formatter.Deserialize(fileStream);
                graffitiPositions = saveData.positions;
            }

            
            //List<string> jpgFilePaths = new List<string>(Directory.GetFiles(imageFolderPath, "*.jpg"));
            //List<string> pngFilePaths = new List<string>(Directory.GetFiles(imageFolderPath, "*.png"));
            //List<string> filePaths = new List<string>(jpgFilePaths);
            //filePaths.AddRange(pngFilePaths);

            DirectoryInfo directory = new DirectoryInfo(imageFolderPath);
            FileInfo[] files = directory.GetFiles();
            List<FileInfo> sortedFiles = new List<FileInfo>(files);
            sortedFiles.Sort((file1, file2) => file1.CreationTime.CompareTo(file2.CreationTime));

            for (int i = 0; i < graffitiPositions.Count; ++i)
            {
                _loadedImagePaths.Add(sortedFiles[i].Name);
                Debug.Log("load data: " + sortedFiles[i]);

                byte[] imageBytes = File.ReadAllBytes(imageFolderPath + "/" + sortedFiles[i].Name);
                Texture2D tex = new Texture2D(2, 2);
                tex.LoadImage(imageBytes);

                Texture2D newTexture = new Texture2D(tex.width, tex.height, TextureFormat.RGBA32, false);

                for (int y = 0; y < tex.height; y++)
                {
                    for (int x = 0; x < tex.width; x++)
                    {
                        Color pixelColor = tex.GetPixel(x, y);
                        Color newPixelColor = new Color(pixelColor.r, pixelColor.g, pixelColor.b, 1);

                        newTexture.SetPixel(x, y, newPixelColor);
                    }
                }

                newTexture.Apply();

                newTexture.FloodClear(5, 5, new Color(0, 0, 0, 0));
                newTexture.Apply();

                GameObject obj = Instantiate(plane, graffitiPositions[photoCount].ToVector3(), Quaternion.Euler(90,-90,0));
                MeshRenderer plane_mr = obj.GetComponent<MeshRenderer>();

                Material newMat = new Material(Shader.Find("Unlit/Transparent Cutout"));

                //UnityEditor.AssetDatabase.CreateAsset(newTexture, "Assets/Resources/" + "matpng_" + photoCount + ".asset");

                newMat.SetTexture("_MainTex", newTexture);

                //UnityEditor.AssetDatabase.CreateAsset(newMat, "Assets/Resources/" + "mat_" + photoCount + ".mat");
                //UnityEditor.AssetDatabase.Refresh();

                plane_mr.material = newMat;

                photoCount += 1;
            }
            
        }
    }
}
