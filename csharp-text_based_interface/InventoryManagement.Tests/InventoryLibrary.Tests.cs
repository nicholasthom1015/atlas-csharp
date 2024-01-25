using System;
using NUnit.Framework;
using InventoryLibrary;

namespace InventoryManagement.Tests
{
    public class Tests
    {
        BaseClass testCase;
        Item testItem;
        User testUser;

        [SetUp]
        public void Setup()
        {
            testCase = new BaseClass();
            testItem = new Item("testItem");
            testUser = new User("testUser");
        }

        [Test]
        public void PropertyTypeTests()
        {
            Assert.IsInstanceOf<string>(testCase.id);
            Assert.IsInstanceOf<DateTime>(testCase.date_created);
            Assert.IsInstanceOf<DateTime>(testCase.date_updated);

            Assert.IsNotNull(testCase.id);
            Assert.IsNotNull(testCase.date_created);
            Assert.IsNotNull(testCase.date_updated);
        }

        [Test]
        public void ItemTest()
        {
            Assert.IsInstanceOf<string>(testItem.name);
            Assert.IsInstanceOf<string>(testItem.description);
            Assert.IsInstanceOf<float>(testItem.price);
            Assert.IsInstanceOf<string[]>(testItem.tags);

            Assert.IsNotNull(testItem.name);
        }

        [TestCase( new object[] {"dogs"}, ExpectedResult =  1)]
        [TestCase( new object[] {"dogs", "cats"}, ExpectedResult =  2)]
        [TestCase( new object[] {"dogs", "cats", "dogs"}, ExpectedResult =  2)]
        [TestCase( new object[] {"dogs", "cats", "dogs", "cats"}, ExpectedResult =  2)]
        [TestCase( new object[] {}, ExpectedResult =  0)]
        public int TagCountTest(params string[] value)
        {
            Item LocalItem;
            try
            {
                LocalItem = new Item("testItem", tags: value);
                return LocalItem.tags.Length;
            }
            catch
            {
                Assert.Fail();
            }
            
            return -1;
        }

        [Test]
        public void UserTest()
        {
            Assert.IsInstanceOf<string>(testUser.name);
            Assert.IsNotNull(testUser.name);
        }

        [TestCase("testUser",ExpectedResult = "testUser")]
        [TestCase("testUser2",ExpectedResult = "testUser2")]
        [TestCase(null, ExpectedResult = "user")]
        public string UserNameTest(string value)
        {
            User LocalUser;
            try
            {
                LocalUser = new User(value);
                Assert.IsNotNull(LocalUser.name);
                return LocalUser.name;
            }
            catch
            {
                Assert.Fail();
            }
            
            return null;
        }

        [TestCase(1, ExpectedResult = 1)]
        [TestCase(2, ExpectedResult = 2)]
        [TestCase(0, ExpectedResult = 0)]
        [TestCase(-1, ExpectedResult = 0)]
        public int InventoryCreationTests(int value)
        {
            Inventory LocalInventory;
            try
            {
                LocalInventory = new Inventory(testUser, testItem, value);
                Assert.IsNotNull(LocalInventory.user_id);
                Assert.IsNotNull(LocalInventory.item_id);
                Assert.AreEqual(LocalInventory.user_id, testUser.id);
                Assert.AreEqual(LocalInventory.item_id, testItem.id);
                return LocalInventory.quantity;
            }
                catch
            {
                Assert.Fail();
                return -1;
            }
        }

        [TestCase(1, ExpectedResult = 1)]
        [TestCase(2, ExpectedResult = 2)]
        [TestCase(0, ExpectedResult = 0)]
        [TestCase(-1, ExpectedResult = 0)]
        public int InventorySetQuantityTests(int value)
        {
            try
            {
                Inventory LocalInventory = new Inventory(testUser, testItem);
                LocalInventory.quantity = value;
                return LocalInventory.quantity;
            }
            catch
            {
                Assert.Fail();
                return -1;
            }
        }

    }
}
