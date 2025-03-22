using System.Text.Json;

public static class SetsAndMaps
{

    /// <summary>
    /// The words parameter contains a list of two character 
    /// words (lower case, no duplicates). Using sets, find an O(n) 
    /// solution for returning all symmetric pairs of words.  
    ///
    /// For example, if words was: [am, at, ma, if, fi], we would return :
    ///
    /// ["am & ma", "if & fi"]
    ///
    /// The order of the array does not matter, nor does the order of the specific words in each string in the array.
    /// at would not be returned because ta is not in the list of words.
    ///
    /// As a special case, if the letters are the same (example: 'aa') then
    /// it would not match anything else (remember the assumption above
    /// that there were no duplicates) and therefore should not be returned.
    /// </summary>
    /// <param name="words">An array of 2-character words (lowercase, no duplicates)</param>
    public static string[] FindPairs(string[] words)
    {
        // TODO Problem 1 - ADD YOUR CODE HERE

        // hash unique words from the input
        var uniqueSet = new HashSet<string>(words);
        // hash pairs to ensure they are unique
        var pairs = new HashSet<string>();
        foreach (var u in uniqueSet)
        {
            var reverseWords = u.Reverse(); // reverse the word
            var reversed = new string(reverseWords.ToArray()); // convert the reversed char back into a string

            // checks if the reversed word exists in the original set
            // & ensures the larger word is stored first to avoid duplicate pairs
            if (uniqueSet.Contains(reversed) && u.CompareTo(reversed) > 0){
                // adds the formatted pair to the hashset
                pairs.Add($"{u} & {reversed}");
            }
        }

        // converts the hashet ot an array and returns the final list of symetric pairs
        return pairs.ToArray();
    }

    /// <summary>
    /// Read a census file and summarize the degrees (education)
    /// earned by those contained in the file.  The summary
    /// should be stored in a dictionary where the key is the
    /// degree earned and the value is the number of people that 
    /// have earned that degree.  The degree information is in
    /// the 4th column of the file.  There is no header row in the
    /// file.
    /// </summary>
    /// <param name="filename">The name of the file to read</param>
    /// <returns>fixed array of divisors</returns>
    public static Dictionary<string, int> SummarizeDegrees(string filename)
    {
        var degrees = new Dictionary<string, int>();
        foreach (var line in File.ReadLines(filename))
        {
            // TODO Problem 2 - ADD YOUR CODE HERE
            var fields = line.Split(",");

            //4th column of the file
            var degreeName = fields[3];

            if (degrees.ContainsKey(degreeName)){
                degrees[degreeName] += 1; // if the degree exists in the dictionary, increment its count by 1
            }
            else{
                degrees[degreeName] = 1; // if not, just add it to the dictionary with an initial count of 1
            }
        }

        return degrees;
    }

    /// <summary>
    /// Determine if 'word1' and 'word2' are anagrams.  An anagram
    /// is when the same letters in a word are re-organized into a 
    /// new word.  A dictionary is used to solve the problem.
    /// 
    /// Examples:
    /// is_anagram("CAT","ACT") would return true
    /// is_anagram("DOG","GOOD") would return false because GOOD has 2 O's
    /// 
    /// Important Note: When determining if two words are anagrams, you
    /// should ignore any spaces.  You should also ignore cases.  For 
    /// example, 'Ab' and 'Ba' should be considered anagrams
    /// 
    /// Reminder: You can access a letter by index in a string by 
    /// using the [] notation.
    /// </summary>
    public static bool IsAnagram(string word1, string word2)
    {
        // TODO Problem 3 - ADD YOUR CODE HERE

        // dictionaries to store the count of each character in both words
        var lettersCount1 = new Dictionary<char, int>();
        var lettersCount2 = new Dictionary<char, int>();

        // iterate through each letter in the first word and remove extra spaces
        foreach (var letter in word1.ToLower().Trim().Replace(" ", ""))
        {
            if (lettersCount1.ContainsKey(letter)){
                // if the letter already exists in the dictionary, increment its count
                lettersCount1[letter] += 1;
            }
            else{
                // if not, initialize the count
                lettersCount1[letter] = 1;
            }
        }

        // same process for the second word
        foreach (var letter in word2.ToLower().Trim().Replace(" ", ""))
        {
            if (lettersCount2.ContainsKey(letter)){
                lettersCount2[letter] += 1;
            }
            else{
                lettersCount2[letter] = 1;
            }
        }

        // compare the letters and their counts in both dictionaries
        foreach (var key in lettersCount1.Keys)
        {
            if (!lettersCount2.ContainsKey(key)){
                return false; // letter is missing in word2
            }

            if (lettersCount1[key] != lettersCount2[key]){
                return false; // letter count is different
            }
        }

        // looks for any extra letters in word2 that are not in word1
        foreach (var key in lettersCount2.Keys)
        {
            if (!lettersCount1.ContainsKey(key)){
                return false; // extra letter in word2
            }
        }

        // if all checks passed, the words are anagrams 
        return true;
    }

    /// <summary>
    /// This function will read JSON (Javascript Object Notation) data from the 
    /// United States Geological Service (USGS) consisting of earthquake data.
    /// The data will include all earthquakes in the current day.
    /// 
    /// JSON data is organized into a dictionary. After reading the data using
    /// the built-in HTTP client library, this function will return a list of all
    /// earthquake locations ('place' attribute) and magnitudes ('mag' attribute).
    /// Additional information about the format of the JSON data can be found 
    /// at this website:  
    /// 
    /// https://earthquake.usgs.gov/earthquakes/feed/v1.0/geojson.php
    /// 
    /// </summary>
    public static string[] EarthquakeDailySummary()
    {
        const string uri = "https://earthquake.usgs.gov/earthquakes/feed/v1.0/summary/all_day.geojson";
        using var client = new HttpClient();
        using var getRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);
        using var jsonStream = client.Send(getRequestMessage).Content.ReadAsStream();
        using var reader = new StreamReader(jsonStream);
        var json = reader.ReadToEnd();
        var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };

        var featureCollection = JsonSerializer.Deserialize<FeatureCollection>(json, options);

        // TODO Problem 5:
        // 1. Add code in FeatureCollection.cs to describe the JSON using classes and properties 
        // on those classes so that the call to Deserialize above works properly.
        // 2. Add code below to create a string out each place a earthquake has happened today and its magitude.

        var earthquakeDetails = new Dictionary<string, double>();

        foreach(var feature in featureCollection.Features){
            var location = feature.Properties.Place;
            var magnitude = feature.Properties.Magnitude;
            earthquakeDetails[location] = magnitude;
        }

       earthquakeDetails.ToArray();

        var result = earthquakeDetails
        .Select(entry => $"{entry.Key} - Mag {entry.Value}")
        .ToArray();

        // 3. Return an array of these string descriptions.
        return result;
    }
}