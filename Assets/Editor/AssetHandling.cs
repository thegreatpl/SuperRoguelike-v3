using System;
using System.IO;
using System.Linq;
using Unity.Collections;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class AssetHandling : MonoBehaviour
{
    [MenuItem("AssetHandling/ImportUnicode")]
    static void ImportUnicode()
    {
        var folder = Directory.GetCurrentDirectory() + "/Assets/Resources/Unicode/";
        var file = folder + "ImageAssignment.txt";

        var lines = File.ReadAllLines(file);

        string unicodeDirectory = Directory.GetCurrentDirectory() + @"\..\..\Unicode images\blackbackground\";

        foreach (var line in lines)
        {
            var split = line.Split(':');
            if (split.Length < 2 || split[0][0] == '-')
                continue;
            
            try
            {
                var path = $"{folder}{split[1]}.png";

                if (File.Exists(path))
                    continue; 

                FileUtil.ReplaceFile($"{unicodeDirectory}{split[1]}.png", path);
                AssetDatabase.ImportAsset(path);



                //TextureImporter ti = AssetImporter.GetAtPath($"Assets/Resources/Unicode/{split[1]}.png") as TextureImporter;
                //ti.isReadable = true;
                //ti.spritePixelsPerUnit = 16;
                //ti.filterMode = FilterMode.Point;
                //ti.spriteImportMode = SpriteImportMode.Single;

                //AssetDatabase.ImportAsset(path, ImportAssetOptions.ForceUpdate);


            }
            catch (Exception e) 
            {
                Debug.LogError($"Exception caught by Unicode Importer: {e.Message}");
            }
        }
        AssetDatabase.Refresh();
        foreach (var image in Directory.GetFiles("Assets/Resources/Unicode").Where(x => Path.GetExtension(x) == ".png"))
        {
            TextureImporter ti = AssetImporter.GetAtPath(image) as TextureImporter;
            ti.isReadable = true;
            ti.spritePixelsPerUnit = 16;
            ti.filterMode = FilterMode.Point;
            ti.spriteImportMode = SpriteImportMode.Single;

            AssetDatabase.ImportAsset(image, ImportAssetOptions.ForceUpdate);
        }


    }

}
