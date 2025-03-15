using Microsoft.VisualStudio.TestTools.UnitTesting;

// TODO Problem 2 - Write and run test cases and fix the code to match requirements.

[TestClass]
public class PriorityQueueTests
{
    [TestMethod]
    // Scenario: Create a queue with different priorities and ensure the correct dequeue order.
    // Expected Result: Emily, Tim, Bob, Sue
    // Defect(s) Found: Tim was dequeued first, which means Dequeue() is not correctly identifying the highest-priority player
    public void TestPriorityQueue_HighestPriorityFirst()
    {
        var bob = new PriorityItem("Bob", 2);
        var tim = new PriorityItem("Tim", 3);
        var sue = new PriorityItem("Sue", 1);
        var emily = new PriorityItem("Emily", 6);

        PriorityItem[] expectedResults = { emily, tim, bob, sue };

        var players = new PriorityQueue();
        players.Enqueue(bob.Value, bob.Priority);
        players.Enqueue(tim.Value, tim.Priority);
        players.Enqueue(sue.Value, sue.Priority);
        players.Enqueue(emily.Value, emily.Priority);

        int i = 0;
        while (players.Length > 0)
        {
            if (i >= expectedResults.Length)
            {
                Assert.Fail("Queue should have run out of items by now.");
            }

            var person = players.Dequeue();
            Assert.AreEqual(expectedResults[i].Value, person);
            i++;
        }
    }

    [TestMethod]
    // Scenario: Create a queue where multiple players have the same priority. The queue should remove the item with the highest priority first. If multiple items have the same highest priority, the earliest item added should be dequeued first.
    // Expected Result: Tim, Emily, Sue, Bob
    // Defect(s) Found: Emily is dequeued before Tim, meaning that Enqueue() doesn't follow the order of insertion for items with the same priority.
    public void TestPriorityQueue_SamePriority()
    {
        var bob = new PriorityItem("Bob", 2);
        var tim = new PriorityItem("Tim",5);
        var sue = new PriorityItem("Sue", 3);
        var emily = new PriorityItem("Emily", 5);

        PriorityItem[] expectedResults = {tim, emily, sue, bob};

        var players = new PriorityQueue();
        players.Enqueue(bob.Value, bob.Priority);
        players.Enqueue(tim.Value, tim.Priority);
        players.Enqueue(sue.Value, sue.Priority);
        players.Enqueue(emily.Value, emily.Priority);

        int i = 0;
        while (players.Length > 0)
        {
            if (i >= expectedResults.Length)
            {
                Assert.Fail("Queue should have run out of items by now.");
            }

            var person = players.Dequeue();
            Assert.AreEqual(expectedResults[i].Value, person);
            i++;
        }

        if (i < expectedResults.Length)
        {
            Assert.Fail($"Queue ran out of items too early.");
        }

    }

    [TestMethod]
    // Scenario: Enqueue a single item and then dequeue it
    // Expected Result: The same item should be returned and the queue should become empty
    // Defect(s) Found: the lenght of the queue is 1 even afeter using Dequeue(), which means that it is not properly removing the item after returning it
    public void TestPriorityQueue_SingleElement()
    {
        var players = new PriorityQueue();
        players.Enqueue("Alice", 10);

        Assert.AreEqual("Alice", players.Dequeue(), "Dequeue should return the only element in the queue.");
        Assert.AreEqual(0, players.Length, "Queue should be empty after dequeuing the only element.");
    }

    [TestMethod]
    // Scenario: Tries to qequeue from an empty queue
    // Expected Result: Exception should be thrown with appropriate error message.
    // Defect(s) Found: None
    public void TestPriorityQueue_EmptyDequeue()
    {
        var players = new PriorityQueue();

        try
        {
            players.Dequeue();
            Assert.Fail("Exception should have been thrown.");
        }
        catch (InvalidOperationException e)
        {
            Assert.AreEqual("The queue is empty.", e.Message);
        }
        catch (AssertFailedException)
        {
            throw;
        }
        catch (Exception e)
        {
            Assert.Fail(
                string.Format("Unexpected exception of type {0} caught: {1}",
                               e.GetType(), e.Message)
            );
        }
    }

    [TestMethod]
    // Scenario: Creates and test aq queue with  moren than 1000 elements
    // Expected Result: The queue should handle large inputs correctly by dequeuing highest priority items first
    // Defect(s) Found: Doesn't dequeue the highest-priority item first Returns elements in the wrong order
    public void TestPriorityQueue_LargeNumOfElements()
    {
        var players = new PriorityQueue();
        for (int i = 0; i < 1000; i++)
        {
            players.Enqueue($"Player{i}", i);
        }

        for (int i = 999; i >= 0; i--)
        {
            Assert.AreEqual($"Player{i}", players.Dequeue(), $"Expected Player{i} to be dequeued.");
        }

        Assert.AreEqual(0, players.Length, "Queue should be empty after all elements are dequeued.");
    }
}