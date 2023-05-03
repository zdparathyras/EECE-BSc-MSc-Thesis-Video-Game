using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenCloseDrawer : MonoBehaviour
{
    [SerializeField] private GameObject Drawer;
    [SerializeField] private GameObject openButton;
    [SerializeField] private GameObject closeButton;

    public void openDrawer()
	{
        Drawer.SetActive(true);
        openButton.SetActive(false);
        closeButton.SetActive(true);
	}

    public void closeDrawer()
    {
        Drawer.SetActive(false);
        openButton.SetActive(true);
        closeButton.SetActive(false);
    }
}
