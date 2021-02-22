using UnityEngine;
using UnityEditor;
using Peixi;

public class IslandEditor : MonoBehaviour
{
    private IslandPresenter presenter;

    private void OnEnable()
    {
        presenter = GetComponent<IslandPresenter>();
    }

    private void OnDrawGizmos()
    {
        if (presenter != null)
        {
            Handles.Label(transform.position, "耐久度:" + presenter.Durability_current);
        }
    }
}


