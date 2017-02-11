#tool nuget:?package=Wyam
#addin nuget:?package=Cake.Wyam

var target = Argument("target", "Build");

Task("Build")
    .Does(() =>
    {
        Wyam();        
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
