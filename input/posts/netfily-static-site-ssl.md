Title: Moving this site from Azure to Netlify
Published: 07/28/2018
Tags:
  - Blog
  - Netlify
---

# Moving the Hosting of This Site and Adding HTTPS

In my quest to enhance my blog's security and reduce costs, I discovered [Netlify](https://www.netlify.com/) while researching ways to add HTTPS to my site on Azure. Netlify offers complimentary hosting for static sites and includes HTTPS support via [Let's Encrypt](https://letsencrypt.org/), which was exactly what I needed.

## Changes to the Build Script

To integrate Netlify into my deployment process, I modified the build script in my Cake build system. Here’s the crucial snippet that I added:

```csharp
Task("Deploy")
    .IsDependentOn("Generate-Blog")
    .Does(() =>
    {
        // Add NETLIFY_TOKEN to your environment variables
        string token = EnvironmentVariable("NETLIFY_TOKEN");
        if(string.IsNullOrEmpty(token))
        {
            throw new Exception("Could not get NETLIFY_TOKEN environment variable");
        }

        // Zip the output directory and upload using curl
        Zip("./output", "output.zip", "./output/**/*");
        StartProcess("curl", 
            "--header \"Content-Type: application/zip\" "
            + "--header \"Authorization: Bearer " + token + "\" "
            + "--data-binary \"@output.zip\" "
            + "--url https://api.netlify.com/api/v1/sites/peaceful-keller-9f51f6.netlify.com/deploys");
    });
```

This script ensures that after the site is generated, it zips the output directory and uses `curl` to upload the zip file to Netlify using the provided API token. I also updated my Continuous Integration (CI) server to call this `Deploy` task as part of the automated process.

## Benefits of Moving to Netlify

The transition to Netlify from Azure brought several advantages:

- **HTTPS Support:** By using Let's Encrypt, my site now supports HTTPS out of the box, enhancing security and trust.
- **Cost Efficiency:** Shifting to Netlify eliminated my hosting costs, as Netlify offers free hosting for static sites.
- **Simplified Deployment:** The integration of Netlify’s deployment API streamlined the process, making deployments faster and more reliable.

Overall, the move to Netlify not only saved costs but also simplified the deployment process and improved the site's security. I highly recommend Netlify for anyone looking to host static sites efficiently and securely.
