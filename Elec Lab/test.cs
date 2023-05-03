using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class test : MonoBehaviour
{
	private struct connection
	{
		public GameObject component;
		public List<GameObject> seSeira;
		public List<GameObject> parallila;
		public bool check;
	}

	private struct occupiedArea
	{
		public List<GameObject> dropAreas;
		public Vector3 center;
	}

	private struct Alignement
	{
		public GameObject objToAlign;
		public occupiedArea area;
		public GameObject starting_row;
		public GameObject ending_row;
	}

	public float current;
	public GameObject ObjectToAlign;
	public List<GameObject> dropAreasCollided;
	public List<GameObject> objectssss;
	public List<GameObject> dropAreasOccupied;
	private List<Alignement> Alignements;
	private bool rotationHorizontal;
	private int pinWidth_x;
	private int pinWidth_z;

	//every name of drop area has a number and character in the end
	private static int myComparison(string str1, string str2)
	{
		int result = 0;
		int n1 = -1, n2 = -1;
		char c1 = 'z', c2 = 'z';

		c1 = str1[str1.Length - 1];
		str1=str1.Substring(0, str1.Length - 1);
		n1 = Int32.Parse(str1);
		//Debug.Log(str1);

		c2 = str2[str2.Length - 1];
		str2 =str2.Substring(0, str2.Length - 1);
		n2 = Int32.Parse(str2);
		//Debug.Log(str2);

		if (c1 == c2)
		{
			if (n1 < n2) { result = -1; }
			else if (n1 > n2) { result = 1; }
			else { result = 0; }
		}
		else if (n1==n2)
		{
			if(c1 < c2) { result = -1; }
			else if (c1 > c2) { result = 1; }
			else { result = 0; }
		}
		else
		{
			if (n1 < n2) { result = -1; }
			else if (n1 > n2) { result = 1; }
			else { result = 0; }
		}

		return result;
	}

	private void Start()
	{
		string str1 = "9b";
		string str2 = "12b";
		int result = myComparison(str1, str2);

		if (result == -2) { Debug.Log(str1 + " < " + str2); }
		else if (result == 0) { Debug.Log(str1 + " == " + str2); }
		else if (result == 2) { Debug.Log(str1 + " > " + str2); }


		objectssss = new List<GameObject>();
		dropAreasCollided = new List<GameObject>();
		dropAreasOccupied = new List<GameObject>();
		Alignements = new List<Alignement>();
	}

	private void RemoveOccupiedDropAreas()
	{
		for (int i = 0; i < dropAreasCollided.Count; i++)
		{
			if (dropAreasOccupied.Contains(dropAreasCollided[i]))
			{
				dropAreasCollided.RemoveAt(i);
				i--;
			}
		}
	}

	private List<GameObject> findRows()
	{
		List<GameObject> rows = new List<GameObject>();
		GameObject row;
		for (int i = 0; i < dropAreasCollided.Count; i++)
		{
			row = dropAreasCollided[i].transform.parent.gameObject;
			if (row != null && rows.Contains(row) == false)
			{
				rows.Add(row);
			}
		}
		return rows;
	}

	//2 drop areas in 1 row
	private List<occupiedArea> findCenter2X1Horizontal(List<GameObject> closestDropAreas)
	{
		Renderer this_renderer;
		ObjectToAlign.TryGetComponent<Renderer>(out this_renderer);

		Vector3 newVector;
		List<occupiedArea> possibleOccupiedAreas = new List<occupiedArea>();
		occupiedArea newArea;
		float distanceBetweenTwo = 0.0f;
		float dist2 = 0.0f;
		if (closestDropAreas.Count > 1)
		{
			for (int i = 0; i < closestDropAreas.Count; i++)
			{
				if (i != closestDropAreas.Count - 1)
				{
					distanceBetweenTwo = closestDropAreas[i + 1].transform.position.x - closestDropAreas[i].transform.position.x;
					dist2 = distanceBetweenTwo / 2;
					newVector = closestDropAreas[i].transform.position + new Vector3(dist2, this_renderer.bounds.size.y / 2 - 0.05f, 0f);
					//newVector = closestDropAreas[i].transform.position + new Vector3(dist2, 0f, 0f);
					newArea = new occupiedArea();
					newArea.dropAreas = new List<GameObject>();
					newArea.dropAreas.Add(closestDropAreas[i]);
					newArea.dropAreas.Add(closestDropAreas[i + 1]);
					newArea.center = newVector;
					possibleOccupiedAreas.Add(newArea);
				}
			}
		}

		return possibleOccupiedAreas;
	}

	//3 drop areas in 1 row
	private List<occupiedArea> findCenter3X1Horizontal(List<GameObject> closestDropAreas)
	{
		Vector3 newVector;
		List<occupiedArea> possibleOccupiedAreas = new List<occupiedArea>();
		occupiedArea newArea;
		float distanceBetweenTwo = 0.0f;
		float dist2 = 0.0f;

		if (closestDropAreas.Count > 2)
		{
			for (int i = 0; i < closestDropAreas.Count - 2; i++)
			{
				//Debug.Log("i= " + i);
				//Debug.Log("i+2= " + (i + 2));
				newArea = new occupiedArea();
				newArea.center = new Vector3();
				newArea.dropAreas = new List<GameObject>();

				Debug.Log("finding center between " + closestDropAreas[i].name + " and " + closestDropAreas[i + 2]);
				distanceBetweenTwo = closestDropAreas[i + 2].transform.position.x - closestDropAreas[i].transform.position.x;
				dist2 = distanceBetweenTwo / 2;
				newVector = closestDropAreas[i].transform.position + new Vector3(dist2, 0f, 0f);

				newArea.dropAreas.Add(closestDropAreas[i]);
				newArea.dropAreas.Add(closestDropAreas[i + 1]);
				newArea.dropAreas.Add(closestDropAreas[i + 2]);
				newArea.center = newVector;

				possibleOccupiedAreas.Add(newArea);
			}
		}

		return possibleOccupiedAreas;
	}

	private List<occupiedArea> findCenter4X1Horizontal(List<GameObject> closestDropAreas)
	{
		Vector3 newVector;
		List<occupiedArea> possibleOccupiedAreas = new List<occupiedArea>();
		occupiedArea newArea;
		float distanceBetweenTwo = 0.0f;
		float dist2 = 0.0f;

		if (closestDropAreas.Count > 3)
		{
			for (int i = 0; i < closestDropAreas.Count - 3; i++)
			{
				//Debug.Log("i= " + i);
				//Debug.Log("i+2= " + (i + 2));
				newArea = new occupiedArea();
				newArea.center = new Vector3();
				newArea.dropAreas = new List<GameObject>();

				Debug.Log("finding center between " + closestDropAreas[i].name + " and " + closestDropAreas[i + 3]);
				distanceBetweenTwo = closestDropAreas[i + 3].transform.position.x - closestDropAreas[i].transform.position.x;
				dist2 = distanceBetweenTwo / 2;
				newVector = closestDropAreas[i].transform.position + new Vector3(dist2, 0f, 0f);

				newArea.dropAreas.Add(closestDropAreas[i]);
				newArea.dropAreas.Add(closestDropAreas[i + 1]);
				newArea.dropAreas.Add(closestDropAreas[i + 2]);
				newArea.dropAreas.Add(closestDropAreas[i + 3]);
				newArea.center = newVector;

				possibleOccupiedAreas.Add(newArea);
			}
		}

		return possibleOccupiedAreas;
	}

	private void alignHorizontal(int width_x, int width_z)
	{
		//Remove all occupied drop areas from the list
		if (dropAreasCollided.Count > 1 && dropAreasOccupied.Count > 0)
		{
			RemoveOccupiedDropAreas();
		}
		else if (dropAreasCollided.Count == 0 || dropAreasCollided.Count == 1)
		{
			Destroy(ObjectToAlign);
			dropAreasCollided.Clear();
			Debug.Log("Drop areas collided are 0 or 1");
			return;
		}

		if (dropAreasCollided.Count < width_x)
		{
			Destroy(ObjectToAlign);
			dropAreasCollided.Clear();
			Debug.Log("Drop areas collided < needed");
			return;
		}

		//Find rows
		List<GameObject> rows = new List<GameObject>();
		GameObject row;
		for (int i = 0; i < dropAreasCollided.Count; i++)
		{
			row = dropAreasCollided[i].transform.parent.gameObject;
			if (row != null && rows.Contains(row) == false)
			{
				rows.Add(row);
			}
		}
		//Debug.Log("found rows");

		if (rows.Count < 1)
		{
			Destroy(ObjectToAlign);
			dropAreasCollided.Clear();
			Debug.Log("rows<1");
			return;
		}

		//find the distance between each row and the object that collided
		List<float> distances = new List<float>();
		float dist = 0.0f;
		for (int j = 0; j < rows.Count; j++)
		{
			//dist = Vector3.Distance(rows[j].transform.position, ObjectToAlign.transform.position);
			dist = rows[j].transform.position.z - ObjectToAlign.transform.position.z;
			distances.Add(dist);
		}
		//Debug.Log("found distances");

		//find the row with minimum distance
		float minDist = 10000.0f;
		GameObject closestRow = null;
		for (int z = 0; z < distances.Count; z++)
		{
			if (distances[z] < minDist)
			{
				minDist = distances[z];
				closestRow = rows[z];
			}
		}
		//Debug.Log("found closest row -> "+closestRow.name);

		//find the dropAreas that belong to the closest row
		List<GameObject> closestDropAreas = new List<GameObject>();
		for (int i = 0; i < dropAreasCollided.Count; i++)
		{
			if (dropAreasCollided[i].transform.parent.gameObject == closestRow)
			{
				if (closestDropAreas.Contains(dropAreasCollided[i]) == false)
				{
					closestDropAreas.Add(dropAreasCollided[i]);
				}
			}
		}
		//Debug.Log("found closest drop areas");

		if (closestDropAreas.Count <= 1)
		{
			Destroy(ObjectToAlign);
			dropAreasCollided.Clear();
			Debug.Log("Closest drop areas<=1");
			return;
		}

		if (closestDropAreas.Count< width_x)
		{
			Destroy(ObjectToAlign);
			dropAreasCollided.Clear();
			Debug.Log("Closest drop areas< needed");
			return;
		}

		//SORT LIST BY NAME
		closestDropAreas.Sort((a, b) => a.name.CompareTo(b.name));
		//Debug.Log("sorted closest drop areas");

		//find the center for each possible area
		List<occupiedArea> possibleOccupiedAreas = new List<occupiedArea>();

		if (width_z == 1 && width_x == 2 && closestDropAreas.Count >= 2)
		{
			possibleOccupiedAreas = findCenter2X1Horizontal(closestDropAreas);
		}
		else if (width_z == 1 && width_x == 3 && closestDropAreas.Count >= 3)
		{
			possibleOccupiedAreas = findCenter3X1Horizontal(closestDropAreas);
		}
		else if (width_z == 1 && width_x == 4 && closestDropAreas.Count >= 4)
		{
			possibleOccupiedAreas = findCenter4X1Horizontal(closestDropAreas);
		}
		else
		{
			Destroy(ObjectToAlign);
			dropAreasCollided.Clear();
			Debug.Log("error in finding possible positions");
			return;
		}

		if (possibleOccupiedAreas.Count < 1)
		{
			Destroy(ObjectToAlign);
			dropAreasCollided.Clear();
			Debug.Log("possible positions<1");
			return;
		}

		//Debug.Log("found center points");

		//find the closest center to the object that collided 
		int index = 0;
		float minCenterDist = Vector3.Distance(possibleOccupiedAreas[0].center, ObjectToAlign.transform.position);

		for (int i = 1; i < possibleOccupiedAreas.Count; i++)
		{
			if (minCenterDist > Vector3.Distance(possibleOccupiedAreas[i].center, ObjectToAlign.transform.position))
			{
				minCenterDist = Vector3.Distance(possibleOccupiedAreas[i].center, ObjectToAlign.transform.position);
				index = i;
			}
		}
		//Debug.Log("found closest center point");

		//move the object there
		Rigidbody rb;
		ObjectToAlign.TryGetComponent<Rigidbody>(out rb);
		if (rb != null)
		{
			rb.transform.position = possibleOccupiedAreas[index].center;

			ObjectToAlign.tag = "objectInDropArea";
			rb.freezeRotation = true;

			for (int i = 0; i < possibleOccupiedAreas[index].dropAreas.Count; i++)
			{
				//Debug.Log(possibleOccupiedAreas[index].dropAreas[i].name);
				dropAreasOccupied.Add(possibleOccupiedAreas[index].dropAreas[i]);
				possibleOccupiedAreas[index].dropAreas[i].tag = "DropAreaOccupied";
			}

			Alignement newAlign = new Alignement();
			newAlign.objToAlign = ObjectToAlign;
			objectssss.Add(ObjectToAlign);
			newAlign.area = possibleOccupiedAreas[index];
			newAlign.starting_row = newAlign.area.dropAreas[0].transform.parent.gameObject;
			newAlign.ending_row = newAlign.area.dropAreas.Last().transform.parent.gameObject;
			Alignements.Add(newAlign);

			ObjectToAlign = null;
			dropAreasCollided.Clear();
		}
		else
		{
			Debug.Log("rigibody=null");
		}

	}

	private void removeUnwantedRows(List<GameObject> rows, int rowsWantedCount)
	{
		//sort rows based on distance from the object we want to align
		if (rowsWantedCount < rows.Count)
		{
			Debug.Log("removing unwanted rows");
			rows.Sort((a, b) => Vector3.Distance(a.transform.position, ObjectToAlign.transform.position).CompareTo(Vector3.Distance(b.transform.position, ObjectToAlign.transform.position)));
			rows.RemoveRange(rowsWantedCount, rows.Count - rowsWantedCount);
		}
	}

	//we must have pinWidth_x number of drop areas for every column
	private List<occupiedArea> findPossibleAreasVertical(int pinWidth_x)
	{
		List<occupiedArea> possibleOccupiedAreas = new List<occupiedArea>();
		List<char> columns = new List<char>();
		DropArea da_script;

		for (int i = 0; i < dropAreasCollided.Count; i++)
		{
			da_script = dropAreasCollided[i].GetComponent<DropArea>();
			if (da_script != null)
			{
				if (columns.Contains(da_script.column) == false)
				{
					columns.Add(da_script.column);
				}
			}
			da_script = null;
		}

		for (int i = 0; i < columns.Count; i++)
		{
			occupiedArea newArea = new occupiedArea();
			newArea.dropAreas = new List<GameObject>();
			newArea.center = new Vector3(0f, 0f, 0f);
			possibleOccupiedAreas.Add(newArea);
		}

		for (int i = 0; i < dropAreasCollided.Count; i++)
		{
			da_script = dropAreasCollided[i].GetComponent<DropArea>();
			if (da_script != null)
			{
				for (int j = 0; j < columns.Count; j++)
				{
					if (da_script.column == columns[j])
					{
						possibleOccupiedAreas[j].dropAreas.Add(dropAreasCollided[i]);
					}
				}
			}
			da_script = null;
		}

		//rows.Sort((a, b) => a.name.CompareTo(b.name));
		for (int i = 0; i < possibleOccupiedAreas.Count; i++)
		{
			possibleOccupiedAreas[i].dropAreas.Sort((a, b) => myComparison(a.name, b.name));
		}

		for (int i = 0; i < possibleOccupiedAreas.Count; i++)
		{
			if (possibleOccupiedAreas[i].dropAreas.Count != pinWidth_x)
			{
				possibleOccupiedAreas.RemoveAt(i);
				i--;
			}
		}



		return possibleOccupiedAreas;
	}

	//2 rows with 1 drop area each
	private List<occupiedArea> findCenter2X1Vertical()
	{
		//find the center for each individual pair
		float distanceBetweenTwo = 0.0f;
		float dist2 = 0.0f;
		List<occupiedArea> possibleOccupiedAreas = new List<occupiedArea>();
		Vector3 newVector;
		occupiedArea newArea;

		for (int i = 0; i < dropAreasCollided.Count - 1; i++)
		{
			newArea = new occupiedArea();
			newArea.center = new Vector3();
			newArea.dropAreas = new List<GameObject>();

			distanceBetweenTwo = dropAreasCollided[i + 1].transform.position.z - dropAreasCollided[i].transform.position.z;
			dist2 = distanceBetweenTwo / 2;
			newVector = dropAreasCollided[i].transform.position + new Vector3(0f, 0f, dist2);

			newArea.center = newVector;
			newArea.dropAreas.Add(dropAreasCollided[i]);
			newArea.dropAreas.Add(dropAreasCollided[i + 1]);

			possibleOccupiedAreas.Add(newArea);
			i++;
		}
		return possibleOccupiedAreas;
	}

	//3 rows with 1 drop area each
	private List<occupiedArea> findCenter3X1Vertical()
	{
		//find the center for each individual pair
		float distanceBetweenTwo = 0.0f;
		float dist2 = 0.0f;
		List<occupiedArea> possibleOccupiedAreas = new List<occupiedArea>();
		Vector3 newVector;
		occupiedArea newArea;

		for (int i = 0; i < dropAreasCollided.Count - 2; i++)
		{
			newArea = new occupiedArea();
			newArea.center = new Vector3();
			newArea.dropAreas = new List<GameObject>();

			distanceBetweenTwo = dropAreasCollided[i + 2].transform.position.z - dropAreasCollided[i].transform.position.z;
			dist2 = distanceBetweenTwo / 2;
			newVector = dropAreasCollided[i].transform.position + new Vector3(0f, 0f, dist2);

			newArea.center = newVector;
			newArea.dropAreas.Add(dropAreasCollided[i]);
			newArea.dropAreas.Add(dropAreasCollided[i + 1]);
			newArea.dropAreas.Add(dropAreasCollided[i + 2]);

			possibleOccupiedAreas.Add(newArea);
			i=i+2;
		}
		return possibleOccupiedAreas;
	}

	//4 rows with 1 drop area each
	private List<occupiedArea> findCenter4X1Vertical()
	{
		//find the center for each individual pair
		float distanceBetweenTwo = 0.0f;
		float dist2 = 0.0f;
		List<occupiedArea> possibleOccupiedAreas = new List<occupiedArea>();
		Vector3 newVector;
		occupiedArea newArea;

		for (int i = 0; i < dropAreasCollided.Count - 3; i++)
		{
			newArea = new occupiedArea();
			newArea.center = new Vector3();
			newArea.dropAreas = new List<GameObject>();

			distanceBetweenTwo = dropAreasCollided[i + 3].transform.position.z - dropAreasCollided[i].transform.position.z;
			Debug.Log("finding center between " + dropAreasCollided[i + 3].name + " and " + dropAreasCollided[i].name);
			dist2 = distanceBetweenTwo / 2;
			newVector = dropAreasCollided[i].transform.position + new Vector3(0f, 0f, dist2);

			newArea.center = newVector;
			newArea.dropAreas.Add(dropAreasCollided[i]);
			newArea.dropAreas.Add(dropAreasCollided[i + 1]);
			newArea.dropAreas.Add(dropAreasCollided[i + 2]);
			newArea.dropAreas.Add(dropAreasCollided[i + 3]);

			possibleOccupiedAreas.Add(newArea);
			i = i + 3;
		}
		return possibleOccupiedAreas;
	}

	private void findCenterVertical(List<occupiedArea> possibleOccupiedAreas, int width)
	{
		float distanceBetweenTwo = 0.0f;
		float dist2 = 0.0f;
		GameObject dropArea1, dropArea2;
		Vector3 newVector;
		occupiedArea newArea;

		for (int i = 0; i < possibleOccupiedAreas.Count; i++)
		{
			int lastDA = possibleOccupiedAreas[i].dropAreas.Count - 1;
			dropArea1 = possibleOccupiedAreas[i].dropAreas[0];
			dropArea2 = possibleOccupiedAreas[i].dropAreas[lastDA];

			distanceBetweenTwo = dropArea2.transform.position.z - dropArea1.transform.position.z;
			dist2 = distanceBetweenTwo / 2;
			newVector = dropArea1.transform.position + new Vector3(0f, 0f, dist2);

			newArea = new occupiedArea();
			newArea.dropAreas = new List<GameObject>();
			newArea.center = new Vector3();

			newArea.dropAreas = possibleOccupiedAreas[i].dropAreas;
			newArea.center = newVector;

			possibleOccupiedAreas[i] = newArea;

			
			
		}

	}

	private void alignVertical(int width_x, int width_z)
	{
		//Remove all occupied drop areas from the list
		if (dropAreasCollided.Count > 1 && dropAreasOccupied.Count > 0)
		{
			RemoveOccupiedDropAreas();
		}

		if (dropAreasCollided.Count == 0 || dropAreasCollided.Count < width_x)
		{
			Destroy(ObjectToAlign);
			dropAreasCollided.Clear();
			Debug.Log("dropAreas collided = 0 or dropAreas collided < necessary");
			return;
		}


		List<occupiedArea> possibleOccupiedAreas = new List<occupiedArea>();
		possibleOccupiedAreas = findPossibleAreasVertical(width_x);

		findCenterVertical(possibleOccupiedAreas, width_x);
		Debug.Log("possible areas");
		string straaa = "";
		for (int i = 0; i < possibleOccupiedAreas.Count; i++)
		{
			straaa = "area " + i + " = ";
			for (int j = 0; j < possibleOccupiedAreas[i].dropAreas.Count; j++)
			{
				straaa += possibleOccupiedAreas[i].dropAreas[j].name + ", ";
			}
			straaa += " with center: "+possibleOccupiedAreas[i].center;
			Debug.Log(straaa);
			straaa = "";
		}

		if (possibleOccupiedAreas.Count < 1)
		{
			Destroy(ObjectToAlign);
			dropAreasCollided.Clear();
			Debug.Log("no possible area found");
			return;
		}

		//find the closest center to the object that collided 
		int index = 0;
		float minCenterDist = Vector3.Distance(possibleOccupiedAreas[0].center, ObjectToAlign.transform.position);

		for (int i = 1; i < possibleOccupiedAreas.Count; i++)
		{
			if (minCenterDist > Vector3.Distance(possibleOccupiedAreas[i].center, ObjectToAlign.transform.position))
			{
				minCenterDist = Vector3.Distance(possibleOccupiedAreas[i].center, ObjectToAlign.transform.position);
				index = i;
			}
		}

		Rigidbody rb;
		ObjectToAlign.TryGetComponent<Rigidbody>(out rb);
		if (rb != null && index < possibleOccupiedAreas.Count)
		{
			rb.freezeRotation = true;
			rb.position = possibleOccupiedAreas[index].center;
			ObjectToAlign.tag = "objectInDropArea";

			for (int i = 0; i < possibleOccupiedAreas[index].dropAreas.Count; i++)
			{
				dropAreasOccupied.Add(possibleOccupiedAreas[index].dropAreas[i]);
			}

			Alignement newAlign = new Alignement();
			newAlign.objToAlign = ObjectToAlign;
			objectssss.Add(ObjectToAlign);
			newAlign.area = possibleOccupiedAreas[index];
			newAlign.starting_row = newAlign.area.dropAreas[0].transform.parent.gameObject;
			newAlign.ending_row = newAlign.area.dropAreas.Last().transform.parent.gameObject;
			Alignements.Add(newAlign);

			ObjectToAlign = null;
			dropAreasCollided.Clear();
		}

	}

	public void OnChildTriggerEnter(GameObject dropArea, GameObject objCollided)
	{
		//Debug.Log("Drop area " + dropArea.name + " collided with " + objCollided.name);
		if (dropAreasCollided.Contains(dropArea) == false)
		{
			dropAreasCollided.Add(dropArea);
		}
	}

	private void OnCollisionEnter(Collision collision)
	{
		ObjectToAlign = collision.gameObject;
		//Debug.Log("collided with " + collision.gameObject.name);
		if (ObjectToAlign.tag == "Draggable")
		{
			RotateOBJ rotate_script;
			ObjectToAlign.TryGetComponent<RotateOBJ>(out rotate_script);
			if (rotate_script != null)
			{
				pinWidth_x = rotate_script.pinWidth_x;
				pinWidth_z = rotate_script.pinWidth_z;
				rotationHorizontal = rotate_script.RotationHorizontal;
				if (rotationHorizontal == true)
				{
					alignHorizontal(pinWidth_x, pinWidth_z);
					dropAreasOccupied.Sort((a, b) => myComparison(a.name, b.name));
				}
				else
				{
					alignVertical(pinWidth_x, pinWidth_z);
					dropAreasOccupied.Sort((a, b) => myComparison(a.name, b.name));
				}
			}
		}
		//CurrentCalculator();
	}

	private void OnCollisionExit(Collision collision)
	{
		if (collision.gameObject.tag == "objectInDropArea")
		{
			ComponentManager component_script = collision.gameObject.GetComponent<ComponentManager>();
			if (component_script != null)
			{
				GameObject controlPanel = component_script.controlPanel;
				if (controlPanel != null && controlPanel.activeSelf == true)
				{
					controlPanel.SetActive(false);
				}
			}

			int index = -1;
			int index2 = -1;
			index = Alignements.FindIndex(x => x.objToAlign == collision.gameObject);
			index2 = objectssss.FindIndex(x => x == collision.gameObject);
			if (index != -1)
			{
				for (int i=0; i<Alignements[index].area.dropAreas.Count; i++)
				{
					dropAreasOccupied.Remove(Alignements[index].area.dropAreas[i]);
				}
				dropAreasOccupied.Sort((a, b) => myComparison(a.name, b.name));
				objectssss.RemoveAt(index2);
				Alignements.RemoveAt(index);
				dropAreasCollided.Clear();
				collision.gameObject.tag = "Draggable";
				//other.gameObject = null;
				ObjectToAlign = null;
			}
		}
		//CurrentCalculator();
	}

	private void CurrentCalculator()
	{

		current = 0.0f;
		float R_seira = 0.0f;
		float R_parallila = 0.0f;
		float V = 0.0f;

		List<connection> connections = new List<connection>();
		List<GameObject> seSeira = new List<GameObject>();
		List<GameObject> parallila = new List<GameObject>();
		GameObject arxi_parallila;
		GameObject telos_parallila;

		for (int i = 0; i < Alignements.Count; i++)
		{
			connection newone = new connection();
			newone.component = Alignements[i].objToAlign;
			newone.seSeira = new List<GameObject>();
			newone.check = new bool();
			newone.check = false;
			newone.parallila = new List<GameObject>();
			connections.Add(newone);
		}

		for (int i = 0; i < Alignements.Count; i++)
		{
			for (int j = i + 1; j < Alignements.Count; j++)
			{
				if (Alignements[i].starting_row == Alignements[j].starting_row)
				{
					connections[i].parallila.Add(Alignements[j].objToAlign);
				}
				else if (Alignements[i].starting_row == Alignements[j].ending_row)
				{
					connections[i].seSeira.Add(Alignements[j].objToAlign);
				}
			}
		}


		string str = "parallila: ";
		for (int i = 0; i < parallila.Count; i++)
		{
			str += parallila[i].name+", ";
		}
		Debug.Log(str);

		Debug.Log(current);
	}
}
