return await Bootstrapper
  .Factory
  .CreateWeb(args)
  .DeployToNetlify(
      "47e16c00-c0cf-41db-a118-1b96a54c3d79",
      Config.FromSetting<string>("NetlifyAccessToken")
  )  
  .RunAsync();