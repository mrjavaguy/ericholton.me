#tool "nuget:https://api.nuget.org/v3/index.json?package=Wyam&version=1.4.1"
#addin "nuget:https://api.nuget.org/v3/index.json?package=Cake.Wyam&version=1.4.1"

DirectoryPath   outputPath = MakeAbsolute(Directory("./output"));
string          target     = Argument("target", "Deploy");

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

Task("Deploy")
    .IsDependentOn("Generate-Blog")
    .Does(() =>
    {
        // Add NETLIFY_TOKEN to your enviornment variables
        string token = EnvironmentVariable("NETLIFY_TOKEN");
        if(string.IsNullOrEmpty(token))
        {
            throw new Exception("Could not get NETLIFY_TOKEN environment variable");
        }

        // zip the output directory and upload using curl
        Zip("./output", "output.zip", "./output/**/*");
        StartProcess("curl", 
            "--header \"Content-Type: application/zip\" "
            + "--header \"Authorization: Bearer " + token + "\" "
            + "--data-binary \"@output.zip\" "
            + "--url https://api.netlify.com/api/v1/sites/peaceful-keller-9f51f6.netlify.com/deploys");
    });

Task("Preview")
    .Does(() =>
    {
        Information("Previewing");
        Wyam(new WyamSettings
        {
            Preview = true,
            Watch = true,
            Recipe="blog"
        });        
    });    

RunTarget(target);
