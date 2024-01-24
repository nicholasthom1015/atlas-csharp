using System;
using System.Collections.Generic;

namespace InventoryLibrary
{
    // <synaps> Item class </synaps>
    public class Item : BaseClass
    {
        string _name = "";
        string _description = "";
        float _price = 0.0f;
        HashSet<string> _tags = new HashSet<string>();

        // <synaps> Name of Item </synaps>
        public string name
        {
            get { return _name; }
            set { _name = value; }
        }

        // <synaps> Item Descrip </synaps>
        public string description
        {
            get { return _description; }
            set { _description = value; }
        }

        // <synaps> Item tags </synaps>
        public string[] tags
        {
            get {
                if (_tags.Count == 0)
                    return new string[0]; 
                return new List<string>(_tags).ToArray(); 
                }
            set { _tags = new HashSet<string>(value); }
        }
        
        // <synaps> Price of Item </synaps>
        public float price
        {
            get { return (float)Math.Round(_price, 2);}
            set { _price = value; }
        }

        // <synaps> Item Builder </synaps>
        public Item(string name, string description = "", float price = 0.0f, string[] tags = null)
        {
            this.name = name;
            this.description = description;
            this.price = price;
            if (tags != null)
                foreach (string tag in tags)
                    this._tags.Add(tag);
        }
    }
}
