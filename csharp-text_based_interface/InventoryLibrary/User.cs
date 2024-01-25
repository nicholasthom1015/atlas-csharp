using System;
using System.Collections.Generic;

namespace InventoryLibrary
{
    // <synaps> User class </synaps>
    public class User : BaseClass
    {
        string _name = "user";
        
        //<synaps> Name of User </synaps>
        public string name
        {
            get { return _name; }
            set { _name = value; }
        }

        //<synaps> User Builder </synaps>
        public User(string name)
        {
            if(name != null)
                this.name = name;
        }
    }
}
