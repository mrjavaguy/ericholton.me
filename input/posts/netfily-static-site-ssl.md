Title: Moving this site from Azure to Netlify
Published: 07/28/2018
Tags: 
  - Blog
  - Netlify
---
# Moving the hosting of this site and HTTPS
While doing some research on adding HTTPS to my blog on Azure, I stumbled on [Netlify](https://www.netlify.com/). Netlify offers free hosting of static sites with HTTPS, powered by [Let's Encrypt](https://letsencrypt.org/).

## Change to the build script

I add the following code to the cake build script

```csharp
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
```

Then I updated my CI server to call the `Deploy` task.

## Benefits

* Site supports HTTPS
* Save money every month
* Simplified deployment