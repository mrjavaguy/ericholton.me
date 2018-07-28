#tool "nuget:https://api.nuget.org/v3/index.json?package=Wyam&version=1.4.1"
#addin "nuget:https://api.nuget.org/v3/index.json?package=Cake.Wyam&version=1.4.1"
#addin "nuget:https://api.nuget.org/v3/index.json?package=Cake.Kudu.Client&version=0.6.0"

DirectoryPath   outputPath = MakeAbsolute(Directory("./output"));
string          target     = Argument("target", "Kudu-Publish-Blog"),
                baseUri    = EnvironmentVariable("KUDU_CLIENT_BASEURI"),
                userName   = EnvironmentVariable("KUDU_CLIENT_USERNAME"),
                password   = EnvironmentVariable("KUDU_CLIENT_PASSWORD");

Task("Clean-Blog")
    .Does(() =>
{
    CleanDirectory(outputPath);
});

Task("Generate-Blog")
    .IsDependentOn("Clean-Blog")
    .Does(() =>
{
    Wyam(new WyamSettings
    {
        Recipe = "Blog",
        Theme = "CleanBlog",
        OutputPath = outputPath
    });
});

Task("Kudu-Publish-Blog")
    .IsDependentOn("Generate-Blog")
    .WithCriteria(!string.IsNullOrEmpty(baseUri)
        && !string.IsNullOrEmpty(userName)
        && !string.IsNullOrEmpty(password)
    )
    .Does(()=>
{
    IKuduClient kuduClient = KuduClient(
        baseUri,
        userName,
        password);

    kuduClient.ZipDeployDirectory(
        outputPath);
});

RunTarget(target);
