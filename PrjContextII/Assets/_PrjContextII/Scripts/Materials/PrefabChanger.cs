using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class PrefabChanger : MonoBehaviour
{
    public GameObject PrefabToChangeTo;
    private Transform parentTransform;
    public Transform Child;
    // Start is called before the first frame update
    void Start()
    {
        if (parentTransform == null)
        {
            parentTransform = transform;
        }
        Child = GetComponentInChildren<MaterialChanger>().transform;
    }

    public void ChangeGameObject()
    {
        if (Child != null)
        {
            // Vind de positie en rotatie van het oude object voor gebruik bij het instantiëren van het nieuwe object
            Vector3 scale = Child.transform.localScale;
            Vector3 position = Child.position;
            Quaternion rotation = Child.rotation;

            // Verwijder of deactiveer het oude GameObject
            Destroy(Child.gameObject);


            // Instantieer het nieuwe GameObject op de plaats van het oude
            GameObject newBuilding = Instantiate(PrefabToChangeTo, position, rotation, parentTransform);
            
            //verander de scale
            newBuilding.transform.localScale = scale;
            // Als je de ouder wilt instellen, waarbij parentTransform de nieuwe ouder is
            newBuilding.transform.SetParent(parentTransform);
        }
    }
}
