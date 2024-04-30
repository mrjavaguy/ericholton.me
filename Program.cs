return await Bootstrapper
  .Factory
  .CreateWeb(args)
  .DeployToNetlify(
      "MySiteId",
      Config.FromSetting<string>("TOKEN_VAR")
  )  
  .RunAsync();