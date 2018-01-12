using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class Recipe {

    public string resultname;
    public string[] ingredients;
    public HashSet<string> set;

    public Recipe(string resultname, string[] ingredients) {
        this.resultname = resultname;
        this.ingredients = ingredients;
        this.set = new HashSet<string>(ingredients);
    }

    public Recipe() {

    }
}
