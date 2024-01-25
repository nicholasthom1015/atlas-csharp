using System;
using System.Collections.Generic;
using InventoryLibrary;

namespace InventoryManager
{
    static class InventoryManager
    {

        delegate void MyCallback(string[] a = null);
        static Dictionary<string, MyCallback> commands = new Dictionary<string, MyCallback>();

        static void Main(string[] args)
        {
            string[] LastIn;
            Init();
            while (true)
            {
                Console.WriteLine(GetMenuPrompt());
                LastIn = GetInput();
                string command = LastIn[0];
                List<string> temp = new List<string>(LastIn);
                temp.RemoveAt(0);
                string[] args2 = temp.ToArray();
                if (commands.ContainsKey(command))
                {
                    MyCallback callback = (MyCallback)commands[command];
                    callback.Invoke(args2);
                }
              
                else 
                {
                    Console.WriteLine("Error: Command not found.");
                }
            }
        }

        // <synaps> ValidateInput </synaps>
        public static bool ValidateInput(string[] args, int argc)
        {
            return args.Length >= argc;
        }

        // <synaps> Init </synaps>
        static void Init()
        {
            JSONStorage.instance.Load();

            commands.Add("exit", Exit);
            commands.Add("all", All);
            commands.Add("create",Create);
            commands.Add("show", Show);
            commands.Add("update", Update);
            commands.Add("delete", Delete);
            commands.Add("classnames", ClassNames);
        }

        // <synaps> GetInput </synaps>
        public static string[] GetInput()
        {
            Console.Write("Enter a command: ");
            string[] Input = Console.ReadLine().ToLower().Split(" ");
            if(Input.Length == 1)
                Input = new string[] {Input[0], null};
            return Input;
        }

        // <synaps> GetMenuPrompt </synaps>
        public static string GetMenuPrompt()
        {
            string Prompt = @"
            Inventory Manager
            <ClassNames> show all ClassNames of objects
            <All> show all objects
            <All [ClassName]> show all objects of a ClassName
            <Create [ClassName]> a new object
            <Show [ClassName object_id]> an object
            <Update [ClassName object_id]> an object
            <Delete [ClassName object_id]> an object
            <Exit> the program
            ";
            return Prompt;
        }

        private static void Exit(string[] a = null)
        {
            Console.WriteLine("Exiting...");
            Environment.Exit(0);
        }

        private static void All(string[] ClassName = null)
        {
            Console.WriteLine("All:");
            foreach (KeyValuePair<string, BaseClass> entry in JSONStorage.instance.All())
            {
                if (ClassName == null || ClassName[0] == null)
                    Console.WriteLine(entry.Key + ": " + entry.Value);
                else
                    {
                        if (entry.Key.Split(".")[0].ToLower() == ClassName[0])
                            Console.WriteLine(entry.Key + ": " + entry.Value);
                    }
            }
        }

        private static void Create(string[] ClassName = null)
        {
            if (ClassName == null || ClassName[0] == null)
            {
                Console.WriteLine("Error: ClassName required.");
                return;
            }
          
            BaseClass NewObject = CreateNew(ClassName);
            if ( NewObject == null)
                return;
            JSONStorage.instance.New(NewObject);
            JSONStorage.instance.Save();
            Console.WriteLine("Created: {0} - {1}", NewObject.GetType().Name, NewObject.id);

        }

        private static BaseClass CreateNew(string[] args)
        {
            string ClassName = args[0];
            switch (ClassName)
            {
                case "item":
                    if(args.Length < 2)
                    {
                        Console.WriteLine("Error: Item requires 1 argument[Name].");
                        return null;
                    }
                
                    return new Item(args[1]);
                case "user":
                    if(args.Length < 2)
                    {
                        Console.WriteLine("Error: User requires 1 argument[Name].");
                        return null;
                    }
                
                    return new User(args[1]);
                case "inventory":
                    if(args.Length < 3)
                    {
                        Console.WriteLine("Error: Inventory requires 2 arguments[UserID, ItemID].");
                        return null;
                    }
                
                    Item inv_item = JSONStorage.instance.GetItem("item", args[2]) as Item;
                    User inv_user = JSONStorage.instance.GetItem("user", args[1]) as User;
                    if (inv_item == null || inv_user == null)
                    {
                        return new Inventory(args[1], args[2]);
                    }
                
                    return new Inventory(inv_user, inv_item);
                default:
                    Console.WriteLine("Error: ClassName not found.");
                    return null;
            }
        }

        // <synaps> Show Method </synaps>
        public static void Show(string[] args = null)
        {
            if (args == null || args[0] == null)
            {
                Console.WriteLine("Error: ClassName required.");
                return;
            }
            if (args.Length < 2 || args[1] == null)
            {
                Console.WriteLine("Error: id required.");
                return;
            }
            BaseClass obj = JSONStorage.instance.GetItem(args[0], args[1]);
            if (obj == null)
            {
                Console.WriteLine("Error: ClassName not found.");
                return;
            }
            Console.WriteLine(obj);
        }

        // <synaps> Update </synaps>
        public static void Update(string[] args)
        {
            Console.WriteLine("Not implemented!");
        }

        // <synaps> Delete </synaps>
        public static void Delete(string[] args)
        {
            JSONStorage.instance.DeleteItem(args[0], args[1]);
        }

        // <synaps> Class Names </synaps>
        public static void ClassNames(string[] args = null)
        {
            Console.WriteLine("Class Names:");
            foreach (string ClassName in JSONStorage.classes)
            {
                Console.WriteLine(ClassName);
            }
        }

    }
}
