using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortionCraftTable : MonoBehaviour
{
    [SerializeField] private GameObject recipe;
    [SerializeField] private GameObject items;
    [SerializeField] private GameObject portion;

    private void Start()
    {
        EnableRecipe();
    }

    public void EnableRecipe()
    {
        recipe.SetActive(true);
        items.SetActive(false);
        portion.SetActive(false);
    }
    public void EnableItems()
    {
        recipe.SetActive(false);
        items.SetActive(true);
        portion.SetActive(false);
    }
    public void EnablePortion()
    {
        recipe.SetActive(false);
        items.SetActive(false);
        portion.SetActive(true);
    }
}
