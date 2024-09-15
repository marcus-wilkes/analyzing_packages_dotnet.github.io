﻿using System.Reflection;
using System.Text.Json;
using NuGet.Frameworks;
using NuGet.Packaging.Core;
using NuGet.Versioning;
using PackageAnalyzer.Models;
using PackageAnalyzer.Services;

Console.WriteLine("Enter solution path:");
string? solutionPath = Console.ReadLine();

if (string.IsNullOrEmpty(solutionPath))
{
    Console.WriteLine("Solution path is required.");
    return;
}

if (!Directory.Exists(solutionPath))
{
    Console.WriteLine("Solution path does not exist.");
    return;
}

var packageConfigFiles = Directory.GetFiles(solutionPath, "*packages.config", SearchOption.AllDirectories);

var projectsWithPackages = new Dictionary<string, List<PackageInfo>>();
foreach (var packageFile in packageConfigFiles)
{
    var projectName = Path.GetFileNameWithoutExtension(packageFile).Replace("_packages", "");
    var packageFileContent = await File.ReadAllTextAsync(packageFile);
    var packages = PackageConfigParser.GetPackages(packageFileContent);
    
    projectsWithPackages[projectName] = packages;
}

foreach (var project in projectsWithPackages)
{
    string projectName = project.Key;
    List<PackageInfo> packages = project.Value;

    Console.WriteLine($"Processing project: {projectName}");

    foreach (var package in packages)
    {
        var packageIdentity = new PackageIdentity(package.Name, NuGetVersion.Parse(package.Version));
        var framework = NuGetFramework.ParseFolder(package.TargetFramework);

        var processedPackages = new HashSet<string>(StringComparer.OrdinalIgnoreCase);

        // Assign transitive dependencies directly
        package.TransitiveDependencies = await NugetService.GetTransitiveDependencies(
            packageIdentity,
            framework,
            processedPackages);
    }

    // Store the analysis including transitive dependencies
    await StoreAnalysis(projectName, packages);

    Console.WriteLine($"Finished processing project: {projectName}");
}

static Task StoreAnalysis(string projectName, List<PackageInfo> packages)
{
    var analysisFilePath = GetAnalysisFilePath(projectName);
    var serializedPackages = JsonSerializer.Serialize(packages, new JsonSerializerOptions { WriteIndented = true });
    return File.WriteAllTextAsync(analysisFilePath, serializedPackages);
}

static string GetAnalysisFilePath(string projectName) => Path.Combine(GetAppPath(), $"{projectName}_Analysis.json");
static string GetAppPath() => Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) ?? throw new InvalidOperationException("Unable to determine application path.");