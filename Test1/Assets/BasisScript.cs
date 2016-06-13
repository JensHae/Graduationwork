using UnityEngine;
using System.Collections;
using System.Xml.Linq;
using System.IO;

public class BasisScript : MonoBehaviour {

    GameObject[] Objects;

	// Use this for initialization
	void Start () {
        Objects = new GameObject[2];
        var xml = File.ReadAllText("Assets/GroundPlanOld.XML");
        var plan = XElement.Parse(xml);
        int i = 0;
        foreach (var wall in plan.Elements("wall"))
        {
            Objects[i] = new GameObject();
            Objects[i].AddComponent<CreateBoxOld>();
            Vector3 pos = new Vector3(float.Parse(wall.Element("position").Element("x").Value),
                float.Parse(wall.Element("position").Element("y").Value),
                float.Parse(wall.Element("position").Element("z").Value));
            Objects[i].GetComponent<CreateBoxOld>().position = pos;
            Objects[i].GetComponent<CreateBoxOld>().width = float.Parse(wall.Element("width").Value);
            Objects[i].GetComponent<CreateBoxOld>().length = float.Parse(wall.Element("length").Value);
            Objects[i].GetComponent<CreateBoxOld>().height = float.Parse(wall.Element("heigth").Value);

            Objects[i].GetComponent<CreateBoxOld>().hole.isSet = bool.Parse(wall.Element("window").Attribute("bool").Value);
            if(bool.Parse(wall.Element("window").Attribute("bool").Value))
            {
                Vector3 holePos = new Vector3(float.Parse(wall.Element("window").Element("position").Element("x").Value),
                float.Parse(wall.Element("window").Element("position").Element("y").Value),
                float.Parse(wall.Element("window").Element("position").Element("z").Value));
                Objects[i].GetComponent<CreateBoxOld>().hole.pos = holePos;
                Objects[i].GetComponent<CreateBoxOld>().hole.height = float.Parse(wall.Element("window").Element("heigth").Value);
                Objects[i].GetComponent<CreateBoxOld>().hole.length = float.Parse(wall.Element("window").Element("length").Value);
            }
        }
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
