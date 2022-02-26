using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Customize : MonoBehaviour
{
    public GameObject[] meshes_AR;

    public void Customization_button(int mesh_ID)
    {
        if (meshes_AR.Length != 2)
        {
            return;
        }

        meshes_AR[mesh_ID].SetActive(true);
        meshes_AR[(meshes_AR.Length - 1) - mesh_ID].SetActive(false);
    }
}
