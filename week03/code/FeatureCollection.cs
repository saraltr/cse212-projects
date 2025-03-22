public class FeatureCollection
{
    // TODO Problem 5 - ADD YOUR CODE HERE
    public List<Feature> Features { get; set;}
}

public class Feature
{
    public Properties Properties { get; set; }
}

public class Properties
{
    public string Place { get; set; }
    public double Magnitude { get; set; }
}