#load nuget:https://www.myget.org/F/cake-contrib/api/v2?package=Cake.Recipe&prerelease

Environment.SetVariableNames();


BuildParameters.SetParameters(context: Context,
                            buildSystem: BuildSystem,
                            sourceDirectoryPath: "./",
                            title: "Autotask.Operations",
                            repositoryOwner: "tjackadams",
                            repositoryName: "autotaskoperations",
                            appVeyorAccountName: "tjackadams");

BuildParameters.PrintParameters(Context);

ToolSettings.SetToolSettings(context: Context,
                            dupFinderExcludePattern: new string[] { BuildParameters.RootDirectoryPath + "/src/Autotask/**/*.AssemblyInfo.cs"  },
                            testCoverageFilter: "+[*]* -[xunit.*]* -[Autotask.Tests]* -[*.Tests]* ",
                            testCoverageExcludeByAttribute: "*.ExcludeFromCodeCoverage*",
                            testCoverageExcludeByFile: "*/*Designer.cs;*/*.g.cs;*/*.g.i.cs");

Build.RunDotNetCore();