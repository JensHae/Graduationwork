using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Parabox.CSG;
using UnityEngine;
using System.IO;
using System.Xml.Linq;
using UnityEditor;

public class BasisScript3: MonoBehaviour
{

    List<GameObject> WallList;
    List<GameObject> WindowList;
    private GameObject Building;
    void Start()
    {
        WallList = new List<GameObject>();
        WindowList = new List<GameObject>();

        var xml = File.ReadAllText("Assets/SVG/Groundplan01.SVG");
        var XD = XDocument.Parse(xml);

        XNamespace n = @"http://www.w3.org/2000/svg";
        var plan = XD.Root.Descendants(n + "svg");

        foreach (var layer in XD.Root.Descendants(n + "g"))
        {
            if (layer.Attribute("id").Value == "Walls" || layer.Attribute("id").Value == "walls")
            {
                int i = 0;
                foreach (var wall in layer.Elements(n + "rect"))
                {
                    var wallObject = new GameObject();
                    wallObject.AddComponent<MeshFilter>();
                    wallObject.AddComponent<MeshRenderer>();
                    wallObject.AddComponent<CreateBox>();
                    var box = wallObject.GetComponent<CreateBox>();

                    Vector3 pos = new Vector3(float.Parse(wall.Attribute("x").Value),
                                              0,
                                              float.Parse(wall.Attribute("y").Value));

                    box.position = pos;
                    box.width = float.Parse(wall.Attribute("height").Value);
                    box.length = float.Parse(wall.Attribute("width").Value);
                    box.height = 40;
                    box.Create();

                    wallObject.name = "wall" + i;

                    WallList.Add(wallObject);
                    i++;
                }
            }
            else if (layer.Attribute("id").Value == "Windows" || layer.Attribute("id").Value == "windows")
            {
                int i = 0;
                foreach (var window in layer.Elements(n + "rect"))
                {
                    var windowObject = new GameObject();
                    windowObject.AddComponent<MeshFilter>();
                    windowObject.AddComponent<MeshRenderer>();
                    windowObject.AddComponent<CreateBox>();
                    var box = windowObject.GetComponent<CreateBox>();

                    Vector3 pos = new Vector3(float.Parse(window.Attribute("x").Value),
                                              10,
                                              float.Parse(window.Attribute("y").Value));

                    box.position = pos;
                    box.width = float.Parse(window.Attribute("height").Value);
                    box.length = float.Parse(window.Attribute("width").Value);
                    box.height = 20;
                    box.Create();

                    windowObject.name = "window" + i;

                    WindowList.Add(windowObject);
                    i++;
                }
            }
            else
            {
                if (EditorUtility.DisplayDialog("Wrong layer name", "Wrong layer name, please reimport", "OK"))
                {
                    Application.Quit();
                }
            }
        }

        //creating all the walls
        Building = Instantiate(WallList.ElementAt(0));
        Building.name = "Building";
        WallList.ElementAt(0).SetActive(false);

        if (WallList.Count > 1)
        {
            for (int i = 0; i < WallList.Count - 1; i++)
            {
                Mesh m = CSG.Union(Building, WallList.ElementAt(i + 1));
                m.Optimize();
                Building.GetComponent<MeshFilter>().mesh = m;
                WallList.ElementAt(i + 1).SetActive(false);
            }
        }

        //getting the windows in the walls
        foreach (var window in WindowList)
        {
            Mesh m = CSG.Subtract(Building, window);
            m.Optimize();
            Building.GetComponent<MeshFilter>().mesh = m;
            window.SetActive(false);
        }
    }
    void Update()
    {
        /*foreach(var wall in WallList) wall.GetComponent<CreateBox>().Create();
		foreach(var window in WindowList) window.GetComponent<CreateBox>().Create();
		
		
		foreach (var window in WindowList)
		{
			int id = window.GetComponent<CreateBox>().id;
			GameObject wall = null;
			int wallCount = WallList.Count;
			int i = 0;
			while(wall == null)
			{
				if (WallList.ElementAt(i).GetComponent<CreateBox>().id == window.GetComponent<CreateBox>().id)
				{
					wall = WallList.ElementAt(i);
				}
				i++;
				if (i == wallCount) break;
			}
			if(wall != null)
			{
				Mesh m = CSG.Subtract(wall, window);
				m.Optimize();
				wall.GetComponent<MeshFilter>().mesh = m;
			}
		}*/

    }
}