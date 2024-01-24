using System;

namespace InventoryLibrary
{
    /// <synaps> BaseClass </synaps>
    public class BaseClass
    {
        /// <synaps> Id of BaseClass </synaps>
        public string id { get;}

        /// <synaps> date created </synaps>
        public DateTime date_created { get;}

        /// <synaps> date updated </synaps>
        public DateTime date_updated { get;}
    
        /// <synaps> BaseClass Builder </synaps>
        public BaseClass()
        {
            this.id = Guid.NewGuid().ToString();
            DateTime curent = DateTime.Now;
            this.date_created = curent;
            this.date_updated = curent;
        }
            
    }
}
