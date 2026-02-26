using System;
using System.IO;
using Lucene.Net.Analysis.Standard;
using Lucene.Net.Documents;
using Lucene.Net.Index;
using Lucene.Net.QueryParsers.Classic;
using Lucene.Net.Search;
using Lucene.Net.Store;
using Lucene.Net.Util;

class Program
{
    private static readonly LuceneVersion AppLuceneVersion = LuceneVersion.LUCENE_48;
    private static readonly string IndexPath = "LuceneIndex";

    static void Main()
    {
        CreateIndex();
        SearchIndex("laptop");
    }

    static void CreateIndex()
    {
        var dir = FSDirectory.Open("LuceneIndex");
        var analyzer = new StandardAnalyzer(AppLuceneVersion);
        var config = new IndexWriterConfig(AppLuceneVersion, analyzer);

        using var writer = new IndexWriter(dir, config);

        var random = new Random();

        string[] categories = { "Laptop", "Mouse", "Keyboard", "Monitor", "Phone" };
        string[] adjectives = { "Gaming", "Professional", "Wireless", "Ultra", "Smart", "Portable" };

        for (int i = 1; i <= 1000; i++)
        {
            string category = categories[random.Next(categories.Length)];
            string adjective = adjectives[random.Next(adjectives.Length)];

            string title = $"{adjective} {category} {i}";
            string description = $"This is a {adjective.ToLower()} {category.ToLower()} designed for high performance and productivity.";

            var doc = new Document
        {
            new StringField("Id", i.ToString(), Field.Store.YES),
            new TextField("Title", title, Field.Store.YES),
            new TextField("Description", description, Field.Store.YES),
            new Int32Field("Price", random.Next(50, 3000), Field.Store.YES)
        };

            writer.AddDocument(doc);
        }

        writer.Commit();
        Console.WriteLine("1000 records indexed successfully.");
    }

    static void AddDocument(IndexWriter writer, int id, string title, string description)
    {
        var doc = new Document
        {
            new StringField("Id", id.ToString(), Field.Store.YES),
            new TextField("Title", title, Field.Store.YES),
            new TextField("Description", description, Field.Store.YES)
        };

        writer.AddDocument(doc);
    }

    static void SearchIndex(string searchText)
    {
        var dir = FSDirectory.Open(IndexPath);
        var analyzer = new StandardAnalyzer(AppLuceneVersion);

        using var reader = DirectoryReader.Open(dir);
        var searcher = new IndexSearcher(reader);

        var parser = new QueryParser(AppLuceneVersion, "Description", analyzer);
        var query = parser.Parse(searchText);

        var hits = searcher.Search(query, 10);

        Console.WriteLine($"\nSearch results for: {searchText}\n");

        foreach (var scoreDoc in hits.ScoreDocs)
        {
            var foundDoc = searcher.Doc(scoreDoc.Doc);

            Console.WriteLine($"Id: {foundDoc.Get("Id")}");
            Console.WriteLine($"Title: {foundDoc.Get("Title")}");
            Console.WriteLine($"Description: {foundDoc.Get("Description")}");
            Console.WriteLine($"Score: {scoreDoc.Score}");
            Console.WriteLine("-----------------------------------");
        }
    }
}