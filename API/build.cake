#addin "nuget:?package=Cake.Coverlet"
#addin nuget:?package=Cake.Sonar
#tool nuget:?package=MSBuild.SonarQube.Runner.Tool
#tool "nuget:?package=ReportGenerator"

var target = Argument("target", "Run");

/*  Caminho relativo dos projetos de teste */
var testProjectsRelativePaths = new string[]
{
    "TOTVS.Fullstack.Challenge.AuctionHouse.Service.Test",
    "TOTVS.Fullstack.Challenge.AuctionHouse.RestApi.Test"
};

/*  Configurações do Test Runner */
ConvertableDirectoryPath parentDirectory = Directory(".");
var coverageDirectory = parentDirectory + Directory("Coverage");
var coverageFilename = "coverage_results";
var coverageFileExtension = ".opencover.xml"; // Necessário ser o formato OpenCover por restrição do Sonar
var reportTypes = "HtmlInline_AzurePipelines";
var coverageFilePath = coverageDirectory + File(coverageFilename + coverageFileExtension);
var jsonFilePath = coverageDirectory + File(coverageFilename + ".json");

Task("Build")
    .Does(() => {
        DotNetCoreBuild("TOTVS.Fullstack.Challenge.AuctionHouse.sln");
    });

Task("Test")
    .IsDependentOn("Build")
    .Does(() => {
        // DotNetCoreTestSettings testSettings = new DotNetCoreTestSettings
        // {
        //     /// Arquivos TRX são usados para publicação dos resultados dos testes no Azure DevOps
        //     ArgumentCustomization = args => args.Append($"--logger trx")
        // };

        CoverletSettings coverletSettings = new CoverletSettings
        {
            CollectCoverage = true,
            CoverletOutputDirectory = coverageDirectory,
            CoverletOutputName = coverageFilename
        };

        // Em caso de somente um teste, o resultado final já é no formato de 'OpenCover'
        if (testProjectsRelativePaths.Length == 1)
        {
            coverletSettings.CoverletOutputFormat  = CoverletOutputFormat.opencover;
            Coverlet(Directory(testProjectsRelativePaths[0]), coverletSettings);
        }
        else
        {
            // Quando existe mais de um projeto é necessário deixar que os arquivos intermediários
            // em formato JSON próprio do Coverlet para que os arquivos intermediários possam ser mesclados
            Coverlet(Directory(testProjectsRelativePaths[0]), coverletSettings);
            coverletSettings.MergeWithFile = jsonFilePath;

            for (int i = 1; i < testProjectsRelativePaths.Length; i++)
            {
                if (i == testProjectsRelativePaths.Length - 1)
                {
                    coverletSettings.CoverletOutputFormat  = CoverletOutputFormat.opencover;
                }

                Coverlet(Directory(testProjectsRelativePaths[i]), coverletSettings);
            }
        }
    });

Task("Report")
    .IsDependentOn("Test")
    .Does(() => {
        var reportSettings = new ReportGeneratorSettings
        {
            ArgumentCustomization = args => args.Append($"-reportTypes:{reportTypes}")
        };

        ReportGenerator(coverageFilePath, coverageDirectory, reportSettings);
    });

Task("Sonar")
    .IsDependentOn("SonarBegin")
    .IsDependentOn("Test")
    .IsDependentOn("SonarEnd");
 
Task("SonarBegin")
    .Does(() => {
        SonarBegin(new SonarBeginSettings
        {
            // Parâmetros suportados
            Key = "jpmoura_TotvsFullstackChalleng",
            Url = "https://sonarcloud.io",
            Login = "a5b809a2c5212e22ac1eb1dd94efc7009d85b336",
            //Password = "myPassword",
            Verbose = true,
            
            // Parâmetros customizados
            ArgumentCustomization = args => args
                .Append("/o:jpmoura-github")
                .Append($"/d:sonar.cs.opencover.reportsPaths={coverageDirectory}\\{coverageFilename}{coverageFileExtension}")
        });
    });

Task("SonarEnd")
    .Does(() => {
        SonarEnd(new SonarEndSettings
        {
            Login = "a5b809a2c5212e22ac1eb1dd94efc7009d85b336",
        });
    });

Task("Run")
    .IsDependentOn("Test")
    .Does(() => {
        DotNetCoreRun("TOTVS.Fullstack.Challenge.AuctionHouse.RestApi/TOTVS.Fullstack.Challenge.AuctionHouse.RestApi.csproj");
    });

RunTarget(target);