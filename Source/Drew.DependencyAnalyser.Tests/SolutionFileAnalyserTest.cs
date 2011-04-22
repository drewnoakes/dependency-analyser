using NUnit.Framework;

namespace Drew.DependencyAnalyser.Tests
{
    [TestFixture]
    public sealed class SolutionFileAnalyserTest
    {
        [Test]
        public void ExtractDependenciesFromProject()
        {
            var analyser = new SolutionFileAnalyser();
            Assertion.AssertEquals(1, analyser.ExtractDependenciesFromProject(SingleDependencyProjectFileContent).Length);
            Assertion.AssertEquals("SolutionDependencyAnalyser", analyser.ExtractDependenciesFromProject(SingleDependencyProjectFileContent)[0]);
            Assertion.AssertEquals(2, analyser.ExtractDependenciesFromProject(DoubleDependencyProjectFileContent).Length);
        }
        
        [Test]
        public void ExtractProjectInfoFromSolution()
        {
            var analyser = new SolutionFileAnalyser();
            Assertion.AssertEquals(1, analyser.ExtractProjectInfoFromSolution(SingleProjectSolutionContent).Length);
            Assertion.AssertEquals("SolutionDependencyPlotter.csproj", analyser.ExtractProjectInfoFromSolution(SingleProjectSolutionContent)[0].RelativePath);
            Assertion.AssertEquals("SolutionDependencyPlotter", analyser.ExtractProjectInfoFromSolution(SingleProjectSolutionContent)[0].Name);
            Assertion.AssertEquals(2, analyser.ExtractProjectInfoFromSolution(DoubleProjectSolutionContent).Length);
        }
        
        #region Test data - Solution files

        const string SingleProjectSolutionContent = @"Microsoft Visual Studio Solution File, Format Version 7.00
Project(""{FAE04EC0-301F-11D3-BF4B-00C04F79EFBC}"") = ""SolutionDependencyPlotter"", ""SolutionDependencyPlotter.csproj"", ""{C5BAF76A-B49F-40D3-A24B-26880A1352DD}""
EndProject
Global
    GlobalSection(SolutionConfiguration) = preSolution
        ConfigName.0 = Debug
        ConfigName.1 = Release
    EndGlobalSection
    GlobalSection(ProjectDependencies) = postSolution
    EndGlobalSection
    GlobalSection(ProjectConfiguration) = postSolution
        {C5BAF76A-B49F-40D3-A24B-26880A1352DD}.Debug.ActiveCfg = Debug|.NET
        {C5BAF76A-B49F-40D3-A24B-26880A1352DD}.Debug.Build.0 = Debug|.NET
        {C5BAF76A-B49F-40D3-A24B-26880A1352DD}.Release.ActiveCfg = Release|.NET
        {C5BAF76A-B49F-40D3-A24B-26880A1352DD}.Release.Build.0 = Release|.NET
    EndGlobalSection
    GlobalSection(ExtensibilityGlobals) = postSolution
    EndGlobalSection
    GlobalSection(ExtensibilityAddIns) = postSolution
    EndGlobalSection
EndGlobal
";

        const string MultipleProjectSolutionContent = @"Microsoft Visual Studio Solution File, Format Version 7.00
Project(""{FAE04EC0-301F-11D3-BF4B-00C04F79EFBC}"") = ""TelexConnector"", ""TelexConnector\TelexConnector.csproj"", ""{C78C08D5-E786-424C-A1D4-A3C7053DFBB9}""
EndProject
Project(""{FAE04EC0-301F-11D3-BF4B-00C04F79EFBC}"") = ""SampleProject.DataAccess"", ""SampleProject.DataAccess\SampleProject.DataAccess.csproj"", ""{871B9787-1242-423B-9571-586030E68ECB}""
EndProject
Project(""{FAE04EC0-301F-11D3-BF4B-00C04F79EFBC}"") = ""SampleProject.Types"", ""SampleProject.Types\SampleProject.Types.csproj"", ""{CB5892A9-8F5C-4CC3-8D01-FED30501DD35}""
EndProject
Project(""{FAE04EC0-301F-11D3-BF4B-00C04F79EFBC}"") = ""SampleProject.DataAccess.Console"", ""SampleProject.DataAccess.Console\SampleProject.DataAccess.Console.csproj"", ""{FB000617-A95A-472D-931E-8D5D091F6538}""
EndProject
Project(""{FAE04EC0-301F-11D3-BF4B-00C04F79EFBC}"") = ""SampleProject.InvoiceEvents"", ""SampleProject.InvoiceEvents\SampleProject.InvoiceEvents.csproj"", ""{C2684F02-0911-45E0-8097-3324A6B03B6B}""
EndProject
Project(""{FAE04EC0-301F-11D3-BF4B-00C04F79EFBC}"") = ""SampleProject.InvoiceEventHandler"", ""SampleProject.InvoiceEventHandler\SampleProject.InvoiceEventHandler.csproj"", ""{3E72DECC-C530-4518-BB31-FB5C79CD8DBC}""
EndProject
Project(""{FAE04EC0-301F-11D3-BF4B-00C04F79EFBC}"") = ""SampleProject.DataAccess.Client"", ""SampleProject.DataAccess.Client\SampleProject.DataAccess.Client.csproj"", ""{631E56D0-715E-4DD1-9B03-27413143CFE5}""
EndProject
Project(""{FAE04EC0-301F-11D3-BF4B-00C04F79EFBC}"") = ""SampleProject.DataAccess.Server"", ""SampleProject.DataAccess.Server\SampleProject.DataAccess.Server.csproj"", ""{71801CC7-6EC3-41CD-AD38-4C66A4BAE6B2}""
EndProject
Project(""{FAE04EC0-301F-11D3-BF4B-00C04F79EFBC}"") = ""SampleProject.Messages"", ""SampleProject.Messages\SampleProject.Messages.csproj"", ""{24491D42-C951-4DF8-ADB5-4D8BF80A14C3}""
EndProject
Project(""{FAE04EC0-301F-11D3-BF4B-00C04F79EFBC}"") = ""SampleProject.Web"", ""http://localhost/openbooks/SampleProject.Web.csproj"", ""{5CDC0CBF-DB7B-4C47-B3B0-2297359367F3}""
EndProject
Project(""{4F174C21-8C12-11D0-8340-0000F80270F8}"") = ""Database"", ""Database\Database.dbp"", ""{78AC0D3C-6206-4401-BD53-A1F9F4190948}""
EndProject
Project(""{FAE04EC0-301F-11D3-BF4B-00C04F79EFBC}"") = ""SampleProject.MessageRouter.Server"", ""SampleProject.MessageRouter.Server\SampleProject.MessageRouter.Server.csproj"", ""{6B53D20B-F63A-48FF-94F2-F6410AC64C0C}""
EndProject
Project(""{FAE04EC0-301F-11D3-BF4B-00C04F79EFBC}"") = ""SampleProject.MessageRouter"", ""SampleProject.MessageRouter\SampleProject.MessageRouter.csproj"", ""{1E161A65-FD97-410E-AB19-726772DBB60D}""
EndProject
Project(""{FAE04EC0-301F-11D3-BF4B-00C04F79EFBC}"") = ""SampleProject.IntegrationTest"", ""SampleProject.IntegrationTest\SampleProject.IntegrationTest.csproj"", ""{DFAF5701-6AE7-4D97-A4FE-A6EE8698562C}""
EndProject
Project(""{4F174C21-8C12-11D0-8340-0000F80270F8}"") = ""SampleProject.Routing"", ""SampleProject.Routing\SampleProject.Routing.dbp"", ""{918E7826-1018-422B-BA91-A8F1E9C7EF6A}""
EndProject
Project(""{FAE04EC0-301F-11D3-BF4B-00C04F79EFBC}"") = ""SampleProject.Alerts"", ""SampleProject.Alerts\SampleProject.Alerts.csproj"", ""{4344626F-B3E6-4579-99BB-4BFAD2B2B346}""
EndProject
Project(""{FAE04EC0-301F-11D3-BF4B-00C04F79EFBC}"") = ""SampleProject.Credentials"", ""SampleProject.Credentials\SampleProject.Credentials.csproj"", ""{0B03605B-2FB7-4386-AFCC-E0152CD6E511}""
EndProject
Project(""{FAE04EC0-301F-11D3-BF4B-00C04F79EFBC}"") = ""SampleProject.WebControls"", ""SampleProject.WebControls\SampleProject.WebControls.csproj"", ""{78EE3C61-66AA-42D9-8ADA-60DC22323BDF}""
EndProject
Project(""{FAE04EC0-301F-11D3-BF4B-00C04F79EFBC}"") = ""SampleProject.SecurityTest"", ""SampleProject.SecurityTest\SampleProject.SecurityTest.csproj"", ""{687C6098-B7E8-4DE4-85C5-F22410A7E615}""
EndProject
Project(""{FAE04EC0-301F-11D3-BF4B-00C04F79EFBC}"") = ""SampleProject.ARImage.Server"", ""SampleProject.ARImage.Server\SampleProject.ARImage.Server.csproj"", ""{EF3D6DD2-5B1B-4472-9940-C53249917BFC}""
EndProject
Project(""{FAE04EC0-301F-11D3-BF4B-00C04F79EFBC}"") = ""SampleProject.Reports.Server"", ""SampleProject.Reports.Server\SampleProject.Reports.Server.csproj"", ""{C87FE99F-0025-4D0F-B452-C806EABC9B2B}""
EndProject
Project(""{FAE04EC0-301F-11D3-BF4B-00C04F79EFBC}"") = ""SampleProject.DataAccess.Service"", ""SampleProject.DataAccess.Service\SampleProject.DataAccess.Service.csproj"", ""{09D01F76-71A6-4E9B-B049-7377857000ED}""
EndProject
Project(""{FAE04EC0-301F-11D3-BF4B-00C04F79EFBC}"") = ""MapiService"", ""SampleProject.TelexConnector\MapiService.csproj"", ""{5D0E3E40-9E24-4F46-9DCD-3AB2BD95B409}""
EndProject
Project(""{54435603-DBB4-11D2-8724-00A0C9A8B90C}"") = ""SampleProject.Installation.AppServer"", ""SampleProject.Installation.AppServer\SampleProject.Installation.AppServer.vdproj"", ""{433AB615-34AC-46D0-B50B-5087D7A297F4}""
EndProject
Project(""{54435603-DBB4-11D2-8724-00A0C9A8B90C}"") = ""SampleProject.Installation.Database"", ""SampleProject.Installation.Database\SampleProject.Installation.Database.vdproj"", ""{F6736631-2818-4DA3-9801-BB8254F56C05}""
EndProject
Project(""{54435603-DBB4-11D2-8724-00A0C9A8B90C}"") = ""SampleProject.Installation.Web"", ""SampleProject.Installation.Web\SampleProject.Installation.Web.vdproj"", ""{B5AE8E99-1352-4178-B19D-817CE052F87D}""
EndProject
Project(""{54435603-DBB4-11D2-8724-00A0C9A8B90C}"") = ""SampleProject.Installation.MailCheckingService"", ""SampleProject.Installation.MailCheckingService\SampleProject.Installation.MailCheckingService.vdproj"", ""{1BD47D4A-3426-49A9-9014-77FE19889FE1}""
EndProject
Project(""{FAE04EC0-301F-11D3-BF4B-00C04F79EFBC}"") = ""Bp.OpenBooks.TiffPrinter"", ""Bp.OpenBooks.TiffPrinter\Bp.OpenBooks.TiffPrinter.csproj"", ""{EFD13718-A216-4C8B-8331-01A09D751823}""
EndProject
Global
";

        const string DoubleProjectSolutionContent = @"Microsoft Visual Studio Solution File, Format Version 7.00
Project(""{FAE04EC0-301F-11D3-BF4B-00C04F79EFBC}"") = ""SolutionDependencyPlotter"", ""SolutionDependencyPlotter.csproj"", ""{C5BAF76A-B49F-40D3-A24B-26880A1352DD}""
EndProject
Project(""{FAE04EC0-301F-11D3-BF4B-00C04F79EFBC}"") = ""SolutionDependencyAnalyser"", ""..\SolutionDependencyAnalyser\SolutionDependencyAnalyser.csproj"", ""{95F5377E-3ED2-4B5D-BF01-0F0EED12CC35}""
EndProject
Global
    GlobalSection(SolutionConfiguration) = preSolution
        ConfigName.0 = Debug
        ConfigName.1 = Release
    EndGlobalSection
    GlobalSection(ProjectDependencies) = postSolution
    EndGlobalSection
    GlobalSection(ProjectConfiguration) = postSolution
        {C5BAF76A-B49F-40D3-A24B-26880A1352DD}.Debug.ActiveCfg = Debug|.NET
        {C5BAF76A-B49F-40D3-A24B-26880A1352DD}.Debug.Build.0 = Debug|.NET
        {C5BAF76A-B49F-40D3-A24B-26880A1352DD}.Release.ActiveCfg = Release|.NET
        {C5BAF76A-B49F-40D3-A24B-26880A1352DD}.Release.Build.0 = Release|.NET
        {95F5377E-3ED2-4B5D-BF01-0F0EED12CC35}.Debug.ActiveCfg = Debug|.NET
        {95F5377E-3ED2-4B5D-BF01-0F0EED12CC35}.Debug.Build.0 = Debug|.NET
        {95F5377E-3ED2-4B5D-BF01-0F0EED12CC35}.Release.ActiveCfg = Release|.NET
        {95F5377E-3ED2-4B5D-BF01-0F0EED12CC35}.Release.Build.0 = Release|.NET
    EndGlobalSection
    GlobalSection(ExtensibilityGlobals) = postSolution
    EndGlobalSection
    GlobalSection(ExtensibilityAddIns) = postSolution
    EndGlobalSection
EndGlobal
";

        #endregion

        #region Test data - Project files

        const string SingleDependencyProjectFileContent = @"<VisualStudioProject>
    <CSHARP
        ProjectType = ""Local""
        ProductVersion = ""7.0.9466""
        SchemaVersion = ""1.0""
        ProjectGuid = ""{C5BAF76A-B49F-40D3-A24B-26880A1352DD}""
    >
        <Build>
            <Settings
                ApplicationIcon = ""App.ico""
                AssemblyKeyContainerName = """"
                AssemblyName = ""SolutionDependencyPlotter""
                AssemblyOriginatorKeyFile = """"
                DefaultClientScript = ""JScript""
                DefaultHTMLPageLayout = ""Grid""
                DefaultTargetSchema = ""IE50""
                DelaySign = ""false""
                OutputType = ""WinExe""
                RootNamespace = ""SolutionDependencyPlotter""
                StartupObject = """"
            >
                <Config
                    Name = ""Debug""
                    AllowUnsafeBlocks = ""false""
                    BaseAddress = ""285212672""
                    CheckForOverflowUnderflow = ""false""
                    ConfigurationOverrideFile = """"
                    DefineConstants = ""DEBUG;TRACE""
                    DocumentationFile = """"
                    DebugSymbols = ""true""
                    FileAlignment = ""4096""
                    IncrementalBuild = ""true""
                    Optimize = ""false""
                    OutputPath = ""bin\Debug\""
                    RegisterForComInterop = ""false""
                    RemoveIntegerChecks = ""false""
                    TreatWarningsAsErrors = ""false""
                    WarningLevel = ""4""
                />
                <Config
                    Name = ""Release""
                    AllowUnsafeBlocks = ""false""
                    BaseAddress = ""285212672""
                    CheckForOverflowUnderflow = ""false""
                    ConfigurationOverrideFile = """"
                    DefineConstants = ""TRACE""
                    DocumentationFile = """"
                    DebugSymbols = ""false""
                    FileAlignment = ""4096""
                    IncrementalBuild = ""false""
                    Optimize = ""true""
                    OutputPath = ""bin\Release\""
                    RegisterForComInterop = ""false""
                    RemoveIntegerChecks = ""false""
                    TreatWarningsAsErrors = ""false""
                    WarningLevel = ""4""
                />
            </Settings>
            <References>
                <Reference
                    Name = ""System""
                    AssemblyName = ""System""
                    HintPath = ""..\..\..\..\..\WINNT\Microsoft.NET\Framework\v1.0.3705\System.dll""
                />
                <Reference
                    Name = ""System.Data""
                    AssemblyName = ""System.Data""
                    HintPath = ""..\..\..\..\..\WINNT\Microsoft.NET\Framework\v1.0.3705\System.Data.dll""
                />
                <Reference
                    Name = ""System.Drawing""
                    AssemblyName = ""System.Drawing""
                    HintPath = ""..\..\..\..\..\WINNT\Microsoft.NET\Framework\v1.0.3705\System.Drawing.dll""
                />
                <Reference
                    Name = ""System.Windows.Forms""
                    AssemblyName = ""System.Windows.Forms""
                    HintPath = ""..\..\..\..\..\WINNT\Microsoft.NET\Framework\v1.0.3705\System.Windows.Forms.dll""
                />
                <Reference
                    Name = ""System.XML""
                    AssemblyName = ""System.Xml""
                    HintPath = ""..\..\..\..\..\WINNT\Microsoft.NET\Framework\v1.0.3705\System.XML.dll""
                />
                <Reference
                    Name = ""nunit.framework""
                    AssemblyName = ""nunit.framework""
                    HintPath = ""..\..\..\..\..\Program Files\NUnit V2.0\bin\nunit.framework.dll""
                />
                <Reference
                    Name = ""SolutionDependencyAnalyser""
                    Project = ""{95F5377E-3ED2-4B5D-BF01-0F0EED12CC35}""
                    Package = ""{FAE04EC0-301F-11D3-BF4B-00C04F79EFBC}""
                />
            </References>
        </Build>
        <Files>
            <Include>
                <File
                    RelPath = ""App.ico""
                    BuildAction = ""Content""
                />
                <File
                    RelPath = ""AssemblyInfo.cs""
                    BuildAction = ""Compile""
                />
                <File
                    RelPath = ""Form1.cs""
                    SubType = ""Form""
                    BuildAction = ""Compile""
                />
                <File
                    RelPath = ""Form1.resx""
                    DependentUpon = ""Form1.cs""
                    BuildAction = ""EmbeddedResource""
                />
            </Include>
        </Files>
    </CSHARP>
</VisualStudioProject>
";

        const string DoubleDependencyProjectFileContent = @"<VisualStudioProject>
    <CSHARP
        ProjectType = ""Local""
        ProductVersion = ""7.0.9466""
        SchemaVersion = ""1.0""
        ProjectGuid = ""{0B03605B-2FB7-4386-AFCC-E0152CD6E511}""
        SccProjectName = ""OpenBooks""
        SccLocalPath = ""..""
        SccProvider = ""MSSCCI:Perforce SCM""
    >
        <Build>
            <Settings
                ApplicationIcon = """"
                AssemblyKeyContainerName = """"
                AssemblyName = ""SampleProject.Credentials""
                AssemblyOriginatorKeyFile = """"
                DefaultClientScript = ""JScript""
                DefaultHTMLPageLayout = ""Grid""
                DefaultTargetSchema = ""IE50""
                DelaySign = ""false""
                OutputType = ""Library""
                RootNamespace = ""SampleProject.Credentials""
                StartupObject = """"
            >
                <Config
                    Name = ""Debug""
                    AllowUnsafeBlocks = ""false""
                    BaseAddress = ""285212672""
                    CheckForOverflowUnderflow = ""false""
                    ConfigurationOverrideFile = """"
                    DefineConstants = ""DEBUG;TRACE""
                    DocumentationFile = """"
                    DebugSymbols = ""true""
                    FileAlignment = ""4096""
                    IncrementalBuild = ""true""
                    Optimize = ""false""
                    OutputPath = ""bin\Debug\""
                    RegisterForComInterop = ""false""
                    RemoveIntegerChecks = ""false""
                    TreatWarningsAsErrors = ""false""
                    WarningLevel = ""4""
                />
                <Config
                    Name = ""Release""
                    AllowUnsafeBlocks = ""false""
                    BaseAddress = ""285212672""
                    CheckForOverflowUnderflow = ""false""
                    ConfigurationOverrideFile = """"
                    DefineConstants = ""TRACE""
                    DocumentationFile = """"
                    DebugSymbols = ""false""
                    FileAlignment = ""4096""
                    IncrementalBuild = ""false""
                    Optimize = ""true""
                    OutputPath = ""bin\Release\""
                    RegisterForComInterop = ""false""
                    RemoveIntegerChecks = ""false""
                    TreatWarningsAsErrors = ""false""
                    WarningLevel = ""4""
                />
            </Settings>
            <References>
                <Reference
                    Name = ""System""
                    AssemblyName = ""System""
                    HintPath = ""..\..\..\..\WINNT\Microsoft.NET\Framework\v1.0.3705\System.dll""
                />
                <Reference
                    Name = ""System.Data""
                    AssemblyName = ""System.Data""
                    HintPath = ""..\..\..\..\WINNT\Microsoft.NET\Framework\v1.0.3705\System.Data.dll""
                />
                <Reference
                    Name = ""System.XML""
                    AssemblyName = ""System.Xml""
                    HintPath = ""..\..\..\..\WINNT\Microsoft.NET\Framework\v1.0.3705\System.XML.dll""
                />
                <Reference
                    Name = ""nunit.framework""
                    AssemblyName = ""nunit.framework""
                    HintPath = ""..\..\..\..\Program Files\NUnit V2.0\bin\nunit.framework.dll""
                />
                <Reference
                    Name = ""SampleProject.Types""
                    Project = ""{CB5892A9-8F5C-4CC3-8D01-FED30501DD35}""
                    Package = ""{FAE04EC0-301F-11D3-BF4B-00C04F79EFBC}""
                />
                <Reference
                    Name = ""System.Web""
                    AssemblyName = ""System.Web""
                    HintPath = ""..\..\..\..\WINNT\Microsoft.NET\Framework\v1.0.3705\System.Web.dll""
                />
                <Reference
                    Name = ""SampleProject.DataAccess""
                    Project = ""{871B9787-1242-423B-9571-586030E68ECB}""
                    Package = ""{FAE04EC0-301F-11D3-BF4B-00C04F79EFBC}""
                />
                <Reference
                    Name = ""Microsoft.ApplicationBlocks.ExceptionManagement.Interfaces""
                    AssemblyName = ""Microsoft.ApplicationBlocks.ExceptionManagement.Interfaces""
                    HintPath = ""..\lib\Microsoft.ApplicationBlocks.ExceptionManagement.Interfaces.dll""
                />
                <Reference
                    Name = ""Microsoft.ApplicationBlocks.ExceptionManagement""
                    AssemblyName = ""Microsoft.ApplicationBlocks.ExceptionManagement""
                    HintPath = ""..\lib\Microsoft.ApplicationBlocks.ExceptionManagement.dll""
                />
                <Reference
                    Name = ""Util.Logging""
                    AssemblyName = ""Util.Logging""
                    HintPath = ""..\lib\Util.Logging.dll""
                />
            </References>
        </Build>
        <Files>
            <Include>
                <File
                    RelPath = ""AssemblyInfo.cs""
                    SubType = ""Code""
                    BuildAction = ""Compile""
                />
                <File
                    RelPath = ""CredentialsComponent.cs""
                    SubType = ""Code""
                    BuildAction = ""Compile""
                />
                <File
                    RelPath = ""CredentialsComponent.resx""
                    DependentUpon = ""CredentialsComponent.cs""
                    BuildAction = ""EmbeddedResource""
                />
                <File
                    RelPath = ""CredentialsComponentTest.cs""
                    SubType = ""Code""
                    BuildAction = ""Compile""
                />
                <File
                    RelPath = ""OpenBooksPrincipal.cs""
                    SubType = ""Code""
                    BuildAction = ""Compile""
                />
                <File
                    RelPath = ""bin\Debug\bp.openbooks.credentials.dll.config""
                    BuildAction = ""None""
                />
            </Include>
        </Files>
    </CSHARP>
</VisualStudioProject>
";

        #endregion
    }
}
