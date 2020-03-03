# Bet.Extensions.Wkhtmltopdf Benchmark


To run benchmark

1. Run in release mode
```
    dotnet run -c Release

```

2. Copy `WkhtmlWrapper` to `Release\netcoreapp3.1`


## Sample results

[BENCHMARKING C# CODE WITH BENCHMARK .NET](https://www.stevejgordon.co.uk/introduction-to-benchmarking-csharp-code-with-benchmark-dot-net)

|      Method |    Mean |    Error |   StdDev | Gen 0 | Gen 1 | Gen 2 |  Allocated |
|------------ |--------:|---------:|---------:|------:|------:|------:|-----------:|
| GetSmallPdf | 1.507 s | 0.2960 s | 0.2768 s |     - |     - |     - |   424.3 KB |
| GetLargePdf | 3.996 s | 0.8330 s | 0.6956 s |     - |     - |     - | 3158.72 KB |
