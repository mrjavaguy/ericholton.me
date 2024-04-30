return await Bootstrapper
  .Factory
  .CreateWeb(args)
  .DeployToNetlify(
      "peaceful-keller-9f51f6",
      Config.FromSetting<string>("TOKEN_VAR")
  )  
  .RunAsync();