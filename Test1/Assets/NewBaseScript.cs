using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Parabox.CSG;
using UnityEngine;
using System.IO;
using System.Xml.Linq;

public class NewBaseScript : MonoBehaviour
{
    List<GameObject> WallList;
    List<GameObject> WindowList;
    void Start()
    {
		WallList = new List<GameObject>();
		WindowList = new List<GameObject>();
            
        var xml = File.ReadAllText("Assets/WallTest2.XML");
        var plan = XElement.Parse(xml);

        foreach (var wall in plan.Element("walls").Elements("wall"))
        {
            var wallObject = new GameObject();
            wallObject.AddComponent<MeshFilter>();
            wallObject.AddComponent<MeshRenderer>();
            wallObject.AddComponent<CreateBox>();
			var box = wallObject.GetComponent<CreateBox>();

            int wallId = int.Parse(wall.Attribute("id").Value);

            Vector3 pos = new Vector3(float.Parse(wall.Element("position").Element("z").Value),
                float.Parse(wall.Element("position").Element("y").Value),
                float.Parse(wall.Element("position").Element("x").Value));

            box.id = wallId;
			box.position = pos;
			box.width = float.Parse(wall.Element("width").Value);
			box.length = float.Parse(wall.Element("length").Value);
			box.height = float.Parse(wall.Element("heigth").Value);
			box.Create();

			wallObject.name = "wall"+wallId;

            WallList.Add(wallObject);
        }

        foreach (var window in plan.Element("windows").Elements("window"))
        {
            var windowObject = new GameObject();
            windowObject.AddComponent<MeshFilter>();
            windowObject.AddComponent<CreateBox>();

            int windowId = int.Parse(window.Attribute("wallId").Value);

            Vector3 pos = new Vector3(float.Parse(window.Element("position").Element("z").Value),
                float.Parse(window.Element("position").Element("y").Value),
                float.Parse(window.Element("position").Element("x").Value));

            windowObject.GetComponent<CreateBox>().id = windowId;
            windowObject.GetComponent<CreateBox>().position = pos;
            windowObject.GetComponent<CreateBox>().width = float.Parse(window.Element("width").Value);
			windowObject.GetComponent<CreateBox>().length = float.Parse(window.Element("length").Value);
            windowObject.GetComponent<CreateBox>().height = float.Parse(window.Element("heigth").Value);
			windowObject.GetComponent<CreateBox>().Create();
			
			windowObject.name = "window"+windowId;

            WindowList.Add(windowObject);
        }

        //creating all the walls
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
        }
    }
	void Update()
	{
		foreach(var wall in WallList) wall.GetComponent<CreateBox>().Create();
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
		}

	}
}

