# Lucene.NET Local Search Engine (1000 Records Demo)

This project demonstrates how to implement a local full-text search engine using Lucene.NET in a .NET Core application.

It shows how to:

- Create and manage a Lucene index
- Generate and index 1000 sample records
- Perform full-text searches
- Store index data on disk
- Work without Elasticsearch or external services

---

## 🚀 Technologies Used

- .NET 8 / .NET Core
- Lucene.NET 4.8
- StandardAnalyzer
- File System Directory (FSDirectory)

Lucene.NET is a .NET port of Apache Lucene, a high-performance full-text search library.

---

## 📦 NuGet Packages

```bash
dotnet add package Lucene.Net --version 4.8.0-beta00016
dotnet add package Lucene.Net.Analysis.Common --version 4.8.0-beta00016
dotnet add package Lucene.Net.QueryParser --version 4.8.0-beta00016
```

