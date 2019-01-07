using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CodeKatas;
using NUnit.Framework;

namespace Tests
{
    [TestFixture]
    public class ImmutableStackTests
    {
        ImmutableStack<string> emptyStack = null;

        [Test]
        public void InitializeStackShouldHaveZeroElements()
        {
            emptyStack = new ImmutableStack<string>();
            Assert.IsTrue(emptyStack.Count == 0);
        }

        [Test]
        public void TakeFromStackShouldReturnNullWhenZeroElements()
        {
            emptyStack = new ImmutableStack<string>();
            Assert.IsNull(emptyStack.Take());
        }

        [Test]
        public void AddToStackShouldIncreaseCount()
        {
            emptyStack = new ImmutableStack<string>();
            var stackWithOneElement = emptyStack.Add("test");

            Assert.IsTrue(emptyStack.Count == 0);
            Assert.IsTrue(stackWithOneElement.Count == 1);
            Assert.IsTrue(stackWithOneElement.Head == "test");
        }

        [Test]
        public void AddToStackTwiceShouldSetTailAndIncreaseCountTwice()
        {
            emptyStack = new ImmutableStack<string>();
            var stackWithOneElement = emptyStack.Add("test1");
            var stackWithTwoElements = stackWithOneElement.Add("test2");

            Assert.IsTrue(emptyStack.Count == 0);
            Assert.IsTrue(stackWithOneElement.Count == 1);
            Assert.IsTrue(stackWithOneElement.Head == "test1");
            Assert.IsTrue(stackWithTwoElements.Count == 2);
            Assert.IsTrue(stackWithTwoElements.Head == "test2");
        }

        [Test]
        public void TakeFromStackShouldReturnLastElementAndDecreaseCount()
        {
            emptyStack = new ImmutableStack<string>();
            var stackWithOneElement = emptyStack.Add("test1");
            var stackWithNoElements = stackWithOneElement.Take();

            Assert.IsTrue(emptyStack.Count == 0);
            Assert.IsTrue(stackWithOneElement.Count == 1);
            Assert.IsTrue(stackWithOneElement.Head == "test1");
            Assert.IsTrue(stackWithNoElements.Count == 0);
            Assert.IsTrue(stackWithNoElements.Head == null);
        }

        [Test]
        public void TakeFromStackTwiceShouldReturnLastElementAndDecreaseCountTwice()
        {
            emptyStack = new ImmutableStack<string>();
            var stackWithOneElement = emptyStack.Add("test1");
            var stackWithTwoElements = stackWithOneElement.Add("test2");
            var stackWithOneElementNew = stackWithTwoElements.Take();
            var stackWithNoElementsNew = stackWithOneElementNew.Take();
            var stackWithNull = stackWithNoElementsNew.Take();

            Assert.IsTrue(stackWithNoElementsNew.Count == 0);
            Assert.IsNull(stackWithNoElementsNew.Tail);
            Assert.IsNull(stackWithNull);
        }

        [Test]
        public void PeekStackShouldReturnCurrentElementWithoutDecreasingCount()
        {
            emptyStack = new ImmutableStack<string>();
            var stackWithOneElement = emptyStack.Add("test1");
            var stackWithTwoElements = stackWithOneElement.Add("test2");
            var peek = stackWithTwoElements.Peek();
            var peek2 = stackWithTwoElements.Peek();

            Assert.IsTrue(stackWithOneElement.Count == 1);
            Assert.IsTrue(stackWithTwoElements.Count == 2);
            Assert.AreEqual(peek, peek2);
            Assert.AreEqual(peek, "test2");
        }
    }
}
