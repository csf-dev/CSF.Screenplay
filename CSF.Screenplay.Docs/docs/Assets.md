---
uid: AssetsArticle
---

# Report assets

[Screenplay reports] provide a deep record of how each [performance] has progressed. 
Sometimes, though, a performance creates our generates things.
Imagine a (fictitious) performance which exports a spreadsheet of sales figures into a PDF file with graphs and charts. 
Wouldn't it be useful to be able to retrieve/access that PDF file from the report? 

_This is what assets facilitate._

[Screenplay reports]: GettingReports.md
[performance]: xref:CSF.Screenplay.IPerformance

## Saving assets

Assets are **files** which are saved to the file system, alongside the report.
There are four steps to doing this: 

1. Grant the actor the [`GetAssetFilePaths`] ability
2. In the performable class which is to save the asset, use the ability (above) to get the file path
3. Save (or copy) the file to the path retrieved above
4. Use the [`RecordAsset`] actor method to associate the asset file with the report and the current performable

[`GetAssetFilePaths`]:xref:CSF.Screenplay.Abilities.GetAssetFilePaths
[`RecordAsset`]: xref:CSF.Screenplay.ICanPerform.RecordAsset(System.Object,System.String,System.String)

## Example

This example shows how to get an asset file and save it alongside the report. 

```csharp
TODO: Write this
```