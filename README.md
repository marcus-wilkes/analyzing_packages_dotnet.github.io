# Analyzing Packages

**Analyzing Packages** is a comprehensive .NET tool designed to analyze solution dependencies, generate detailed package reports, and streamline the migration of legacy microservices to a unified monolith architecture.

## 📖 Project Overview

Analyzing Packages was created to address the complexities associated with managing dependencies within large .NET Framework solutions. Originally developed to facilitate Roamler's migration from legacy microservices to a monolithic architecture, it is now an open-source tool designed to assist teams in navigating the challenges of dependency management, package versioning, and modernization. By providing deep insights into package usage, this tool makes it easier to identify version conflicts and dependencies that may hinder a successful migration.

## 🚀 Key Features

- **Solution-Wide Analysis**: Scans all `packages.config` files within a solution to assess package usage.
- **Dependency Mapping**: Creates a comprehensive map of both direct and transitive package dependencies.
- **Detailed Reports**: Generates in-depth reports on project dependencies, helping to highlight potential issues.
- **Anomaly Detection**: Identifies discrepancies in package versions across projects, pinpointing areas for resolution.

## 🛠️ How It Works

PackageAnalyzer integrates with the NuGet API to retrieve detailed package information directly from NuGet servers. Here's how the tool operates:

- **Dependency Graph Generation**: Produces a full dependency graph that includes both direct and transitive dependencies for each project.
- **Caching Mechanism**: To improve performance and minimize redundant API calls, PackageAnalyzer caches project data in the "AnalysisResult" directory located alongside the executable.
- **Fresh Data Handling**: If you need up-to-date information, simply delete the "AnalysisResult" directory before rerunning the analysis to ensure that cached data is not used.

This architecture enables fast, efficient results without excessive network or API usage.

## 📊 Available Reports

PackageAnalyzer offers three primary types of reports to provide clear insights into your solution’s dependency landscape:

1. **Packages by Project**
   - Lists all projects within the solution along with the packages they use and their versions.
   - Helps understand the dependency structure of individual projects.
   - Useful for identifying projects that may require updates or dependency consolidation.

2. **Projects by Package**
   - Displays all packages used across the solution, along with the projects that use each package and the corresponding versions.
   - Highlights version discrepancies, making it easier to address potential conflicts.
   - Assists in aligning package versions across projects for a smooth migration.

3. **Anomalies Report**
   - Focuses on detecting inconsistencies in package versions across projects.
   - Lists projects where package versions differ from the majority, allowing quick identification of problematic areas.
   - Essential for planning version alignment during monolith migrations.
---

## Author

Developed by the Marcus Wilkes and his team from the open-source community.
